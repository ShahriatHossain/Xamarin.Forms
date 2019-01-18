//using PCLStorage;

using Pigeon.Helpers;
using Pigeon.LocalDataAccess.Model;
using Pigeon.Services.Configuration;
using Pigeon.Services.Interface;
using Pigeon.Services.Model;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using Xamarin.Forms;

namespace Pigeon.ViewModels
{
    public class NoticeCreatePageViewModel : BindableBase, INavigationAware
    {
        private MediaFile _selecteFile;
        private FileData _selectedPdf;

        private readonly INavigationService _navigationService;
        private readonly IMessageService _messageService;
        private ImageResizeHelper _imageResizeHelper;
        public DelegateCommand PickFileCommand { get; set; }
        private CommonHelper _commonHelper;
        private FileManageHelper _fileManagerHelper;
        public NoticeCreatePageViewModel(INavigationService navigationService,
            IMessageService messageService)
        {
            this._navigationService = navigationService;
            this._messageService = messageService;
            _commonHelper = new CommonHelper();
            _fileManagerHelper = new FileManageHelper();
            PostNoticeCommand = new DelegateCommand(PostNotice);
            DismissCommand = new DelegateCommand(Dismiss);
            this.VotingExpireDate = DateTime.Now;
            this.MinDate = DateTime.Now;
            this.IsPlainNotice = false;
            this.IsPDF = false;
            LogoSource = ImageSource.FromResource("Pigeon.Images.file_upload_icon.png");
            _imageResizeHelper = new ImageResizeHelper();
            PickFileCommand = new DelegateCommand(PickPhotoOrPdf);
        }

        public DelegateCommand PostNoticeCommand { get; set; }
        public DelegateCommand DismissCommand { get; set; }

        #region Properties
        private string _notice;
        public string Notice
        {
            get { return _notice; }
            set { SetProperty(ref _notice, value); }
        }

        private string _fileDisplayName;
        public string FileDisplayName
        {
            get { return _fileDisplayName; }
            set { SetProperty(ref _fileDisplayName, value); }
        }

        private DateTime _votingExpireDate;
        public DateTime VotingExpireDate
        {
            get { return _votingExpireDate; }
            set { SetProperty(ref _votingExpireDate, value); }
        }

        private DateTime _minDate;
        public DateTime MinDate
        {
            get { return _minDate; }
            set { SetProperty(ref _minDate, value); }
        }

        private bool _isVotingEnable;
        public bool IsVotingEnable
        {
            get { return _isVotingEnable; }
            set { SetProperty(ref _isVotingEnable, value); }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        private ChannelApi _channel;
        public ChannelApi Channel
        {
            get { return _channel; }
            set { SetProperty(ref _channel, value); }
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            this.Channel = (ChannelApi)parameters["channel"];
        }

        private async void Dismiss()
        {
            InstituteSession.Current.SetSelectedChannel(Convert.ToInt32(this.Channel.Id));

            await _navigationService.GoBackAsync();
        }

        private bool _isPlainNotice;
        public bool IsPlainNotice
        {
            get { return _isPlainNotice; }
            set
            {
                if (!value)
                {
                    IsPDF = false;
                    Notice = string.Empty;
                }
                SetProperty(ref _isPlainNotice, value);
            }
        }

        private bool _isPDF;
        public bool IsPDF
        {
            get { return _isPDF; }
            set
            {
                if (value)
                {
                    DeleteImage();
                    LogoSource = ImageSource.FromResource("Pigeon.Images.pdf_upload_icon.png");
                }
                else
                {
                    LogoSource = ImageSource.FromResource("Pigeon.Images.file_upload_icon.png");
                    DisplayFileName = string.Empty;
                }

                SetProperty(ref _isPDF, value);
            }
        }

        private ImageSource _logoSource;
        public ImageSource LogoSource
        {
            get { return _logoSource; }
            set { SetProperty(ref _logoSource, value); }
        }
        #endregion

        private async void PostNotice()
        {
            await _commonHelper.ShowLoaderAsync();
            var notice = new Services.Model.ChannelNotice();

            var message = new TextMessage();
            if (!this.IsPlainNotice)
            {
                message.Text = this.Notice;
            }
            if (this.IsVotingEnable)
            {
                message.VotingExpireDate = VotingExpireDate.AddHours(23).AddMinutes(59).AddSeconds(59);
            }
            message.EnableVoting = this.IsVotingEnable;


            if (!this.IsPlainNotice)
            {
                if (string.IsNullOrWhiteSpace(message.Text))
                {
                    await _commonHelper.HideLoaderAsync();

                    _commonHelper.DisplayAlertMessage(string.Empty, "Blank Notices are not allowed.");
                    return;
                }
                else
                {
                    notice = await _messageService.Save(message, this.Channel);
                }
            }
            else if (this.IsPDF)
            {
                notice = await _messageService.SaveMediaNoticeAsync(this.Channel.Id, message.EnableVoting, message.VotingExpireDate, _fileManagerHelper.ConvertByteArrayToStream(_fileManagerHelper.ConvertFileDatatoByteArray(_selectedPdf)), ".pdf");
            }
            else
            {
                notice = await _messageService.SaveMediaNoticeAsync(this.Channel.Id, message.EnableVoting, message.VotingExpireDate, _fileManagerHelper.ConvertByteArrayToStream(_fileManagerHelper.ConvertMediaFiletoByteArray(_selecteFile)), ".png");
            }

            if (notice.Id > 0)
            {
                var newNotice = new LocalDataAccess.Model.ChannelNotice()
                {
                    NoticeId = notice.Id,
                    UserId = LocalStorageSettings.Token,
                    ChannelId = this.Channel.Id.Value,
                    Notice = notice.Notice,
                    ResponseNeeded = notice.ResponseNeeded,
                    //FileType = x.FileType,
                    //FileDisplayName = x.FileDisplayName,
                    CreationTime = notice.CreationTime,
                    //VotingLastDate = x.VotingLastDate,
                    //VotingStatus = x.VotingStatus
                };
                //TODO
                //_noticeDataAccess.Save(newNotice);
                DeleteImage();

                UserSession.Current.NotificationService.Publish(this.Channel.Id, this.Channel.Name, message.Text);
                var param = new NavigationParameters();
                param.Add("hasNewNotice", true);

                InstituteSession.Current.SetSelectedChannel(Convert.ToInt32(this.Channel.Id));

                await _navigationService.GoBackAsync(param);

                await _commonHelper.HideLoaderAsync();
            }
            else
            {
                await _commonHelper.HideLoaderAsync();
            }
        }

        private async void GetPdfOrImage(TextMessage message)
        {
            if (IsPDF)
            {
                this.GetPdf();
            }
            else
            {
                this.GetImage();
            }
        }

        private async void GetPdf()
        {
            if (this._selectedPdf == null)
            {
                await _commonHelper.HideLoaderAsync();
                _commonHelper.DisplayAlertMessage("No file selected", "Please select a PDF.");
                return;
            }
        }

        private async void GetImage()
        {
            if (this._selecteFile == null)
            {
                await _commonHelper.HideLoaderAsync();
                _commonHelper.DisplayAlertMessage("No file selected", "Please select an Image.");
                return;
            }
        }

        private void DeleteImage()
        {
            try
            {
                if (_selecteFile != null && !string.IsNullOrEmpty(_selecteFile.Path))
                {
                    DeleteImageFile(_selecteFile.Path);
                    _selecteFile = null;
                }
            }
            catch (Exception)
            {

                //throw;
            }
        }

        private void DeleteImageFile(string path)
        {
            if (string.IsNullOrEmpty(path)) return;
            _fileManagerHelper.DeleteLocalFile(path);
        }

        private async void PickPhotoOrPdf()
        {
            if (IsPDF)
            {
                this.PickPdf();
            }
            else
            {
                this.PickImage();
            }
        }

        private async void PickImage()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                _commonHelper.DisplayAlertMessage("Photos Not Supported", "Permission not granted to photos.");
                return;
            }
            var previousTempImageFilePath = _selecteFile == null ? string.Empty : _selecteFile.Path;
            _selecteFile = await CrossMedia.Current.PickPhotoAsync();

            if (_selecteFile == null)
                return;
            DisplayFileName = System.IO.Path.GetFileName(_selecteFile.Path);
            DeleteImageFile(previousTempImageFilePath);
            LogoSource = ImageSource.FromStream(() =>
            {
                var stream = _selecteFile.GetStream();
                return stream;
            });
        }

        private async void PickPdf()
        {
            try
            {
                _selectedPdf = await CrossFilePicker.Current.PickFile();
                if (_selectedPdf == null)
                    return;
                if (!_selectedPdf.FileName.EndsWith(".pdf"))
                {
                    _commonHelper.DisplayAlertMessage(string.Empty, "Wrong File Type! Please Select PDF.");
                    return;
                }
                SetPdfDetail();
            }
            catch (Exception ex)
            {
                _commonHelper.DisplayAlertMessage(string.Empty, "PDF could not be uploaded! Place the PDF in local drive and try again.");
            }

        }

        private string _displayFileName;
        public string DisplayFileName
        {
            get { return _displayFileName; }
            set { SetProperty(ref _displayFileName, value); }
        }
        private void SetPdfDetail()
        {
            var pdfDataArray = _selectedPdf.DataArray;
            if (pdfDataArray.Length <= 0)
            {
                pdfDataArray = new FileManageHelper().GetFileDataArray(_selectedPdf.FileName);
                if (pdfDataArray.Length <= 0)
                {
                    _commonHelper.DisplayAlertMessage(string.Empty, "PDF could not be uploaded! Place the PDF in local drive and try again.");
                }
                else
                {
                    DisplayFileName = _selectedPdf.FileName;
                }
            }
            else
            {
                DisplayFileName = _selectedPdf.FileName;
            }

            LogoSource = ImageSource.FromResource("Pigeon.Images.pdf_icon.png");
        }
    }
}
