using Pigeon.Helpers;
using Pigeon.Services.Interface;
using Pigeon.Services.Model;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using Xamarin.Forms;

namespace Pigeon.ViewModels
{
    public class ChannelCreatePageViewModel : BindableBase, INavigationAware
    {
        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;
        private readonly IChannelService _channelService;
        private readonly IChannelCategoryService _channelCategoryService;

        private FileManageHelper _fileManagerHelper;
        private CommonHelper _commonHelper;

        public ChannelCreatePageViewModel(IUserService userService, IChannelService channelService,
            INavigationService navigationService, IChannelCategoryService channelCategoryService,
            FileManageHelper fileManagerHelper)
        {
            _userService = userService;
            _channelService = channelService;
            _navigationService = navigationService;
            _commonHelper = new CommonHelper();
            _channelCategoryService = channelCategoryService;
            _fileManagerHelper = fileManagerHelper;
        }

        public void OnNavigatedFrom(NavigationParameters parameters) { }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            int instituteId = Convert.ToInt32(parameters["instituteId"]);
            var instituteName = Convert.ToString(parameters["instituteName"]);
            SetDefaultFields(instituteId, instituteName);
        }

        private int _channelCategoryId;
        public int ChannelCategoryId
        {
            get { return _channelCategoryId; }
            set { SetProperty(ref _channelCategoryId, value); }
        }

        private string _channelName;
        public string ChannelName
        {
            get { return _channelName; }
            set { SetProperty(ref _channelName, value); }
        }

        private ImageSource _logoSource;
        public ImageSource LogoSource
        {
            get { return _logoSource; }
            set { SetProperty(ref _logoSource, value); }
        }

        private int _instituteId;
        public int InstituteId
        {
            get { return _instituteId; }
            set { SetProperty(ref _instituteId, value); }
        }

        private string _instituteName;
        public string InstituteName
        {
            get { return _instituteName; }
            set { SetProperty(ref _instituteName, value); }
        }

        private void SetDefaultFields(int instituteId, string instituteName)
        {
            LogoSource = ImageSource.FromResource("Pigeon.Images.file_upload_icon.png");
            InstituteId = instituteId;
            InstituteName = instituteName;
        }

        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(SaveAsync));

        private async void SaveAsync()
        {
            var channel = new ChannelApi
            {
                Name = ChannelName,
                ChannelCategoryId = ChannelCategoryId,
                InstituteId = InstituteId,
                Logo = LogoBlob
            };

            await _commonHelper.ShowLoaderAsync();

            var result = await _channelService.SaveChannelAsync(channel);

            if (result != null)
            {
                var param = new NavigationParameters();
                param.Add("instituteId", InstituteId);
                param.Add("instituteName", InstituteName);
                await _navigationService.NavigateAsync("InstituteChannelList", param, false, false);
            }

            await _commonHelper.HideLoaderAsync();
        }

        DelegateCommand _pickPhotoCommand;
        public DelegateCommand PickPhotoCommand => _pickPhotoCommand != null ?
            _pickPhotoCommand : (_pickPhotoCommand = new DelegateCommand(PickPhotoAsync));

        private MediaFile _selecteFile;

        private string _logoBlob;
        public string LogoBlob
        {
            get { return _logoBlob; }
            set { SetProperty(ref _logoBlob, value); }
        }

        private async void PickPhotoAsync()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                _commonHelper.DisplayAlertMessage("Photos Not Supported", "Permission not granted to photos.");
                return;
            }

            _selecteFile = await CrossMedia.Current.PickPhotoAsync();

            LogoSource = ImageSource.FromStream(() =>
            {
                var stream = _selecteFile.GetStream();
                return stream;
            });


            LogoBlob = await _channelService.PostAndGetBlobAsync(_fileManagerHelper.ConvertByteArrayToStream(_fileManagerHelper.ConvertMediaFiletoByteArray(_selecteFile)), ".png");
        }
    }
}
