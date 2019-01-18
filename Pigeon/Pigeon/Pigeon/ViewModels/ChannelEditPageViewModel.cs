using Pigeon.Services.Interface;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Pigeon.Helpers;
using Pigeon.Services.Model;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Navigation;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace Pigeon.ViewModels
{
    public class ChannelEditPageViewModel : BindableBase, INavigationAware
    {

        private readonly IUserService _userService;
        private readonly INavigationService _navigationService;
        private readonly IChannelService _channelService;
        private readonly IChannelCategoryService _channelCategoryService;

        private FileManageHelper _fileManagerHelper;
        private CommonHelper _commonHelper;

        public ChannelEditPageViewModel(IUserService userService, IChannelService channelService,
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

        public void OnNavigatedFrom(NavigationParameters parameters) {
           
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            int channelId = Convert.ToInt32(parameters["channelId"]);
            SetDefaultFields(channelId);
        }

        private ImageSource _logoSource;
        public ImageSource LogoSource
        {
            get { return _logoSource; }
            set { SetProperty(ref _logoSource, value); }
        }

        private ChannelApi _channelDetails;
        public ChannelApi ChannelDetails
        {
            get { return _channelDetails; }
            set { SetProperty(ref _channelDetails, value); }
        }

        private ChannelCategory _channelCategory;

        public ChannelCategory ChannelCategory
        {
            get { return _channelCategory; }
            set { SetProperty(ref _channelCategory, value); }
        }

        private ObservableCollection<ChannelCategory> _channelCategoryList;
        public ObservableCollection<ChannelCategory> ChannelCategoryList
        {
            get { return _channelCategoryList; }
            set { SetProperty(ref _channelCategoryList, value); }
        }

        private async void SetDefaultFields(int? channelId)
        {
            ChannelDetails = await _channelService.GetChannelDetailByOwnedAsync(channelId, string.Empty);
            LogoSource = ChannelDetails.LogoUri !=null? ImageSource.FromUri(ChannelDetails.LogoUri): ImageSource.FromResource("Pigeon.Images.file_upload_icon.png");
            ChannelCategoryList = await _channelCategoryService.GetChannelCategoriesAsync();
            ChannelCategory = ChannelCategoryList.Where(x => x.Id == ChannelDetails.ChannelCategoryId).FirstOrDefault();
        }

        private int _channelCategoryId;
        public int ChannelCategoryId
        {
            get { return _channelCategoryId; }
            set { SetProperty(ref _channelCategoryId, value); }
        }

        private DelegateCommand _updateCommand;
        public DelegateCommand UpdateCommand => _updateCommand ?? (_updateCommand = new DelegateCommand(UpdateAsync));

        private async void UpdateAsync()
        {
            var channel = new ChannelApi
            {
                Name = ChannelDetails.Name,
                ChannelCategoryId = ChannelCategoryId,
                InstituteId = ChannelDetails.InstituteId,
                Logo = string.IsNullOrEmpty(LogoBlob)? ChannelDetails.Logo: LogoBlob,
                Id = Convert.ToInt32(ChannelDetails.ChannelId)
            };

            await _commonHelper.ShowLoaderAsync();

            var result = await _channelService.UpdateChannelAsync(channel);

            if (result != null)
            {
                var param = new NavigationParameters();
                param.Add("instituteId", ChannelDetails.InstituteId);
                param.Add("instituteName", string.Empty);
                await _navigationService.NavigateAsync("InstituteTabbedPage/ChannelListPage", null, false, false);
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
