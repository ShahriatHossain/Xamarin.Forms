using Pigeon.Helpers;
using Pigeon.Services.Interface;
using Pigeon.Services.Model;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Linq;

namespace Pigeon.ViewModels
{
    public class EditInstitutePageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IInstituteService _instituteService;
        private readonly IInstituteCategoryService _instituteCategoryService;
        private readonly IInstituteTypeService _instituteTypeService;
        private readonly IChannelService _channelService;
        private FileManageHelper _fileManagerHelper;
        private CommonHelper _commonHelper;
        public EditInstitutePageViewModel(INavigationService navigationService, IInstituteService instituteService,
            IInstituteCategoryService instituteCategoryService, IInstituteTypeService instituteTypeService,
            IChannelService channelService, FileManageHelper fileManagerHelper, CommonHelper commonHelper)
        {
            _navigationService = navigationService;
            _instituteService = instituteService;
            _instituteCategoryService = instituteCategoryService;
            _instituteTypeService = instituteTypeService;
            _channelService = channelService;
            _fileManagerHelper = fileManagerHelper;
            _commonHelper = commonHelper;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            int instituteId = Convert.ToInt32(parameters["InstituteId"]);
            SetDefaultFields(instituteId);
        }

        private Institute _instituteDetails;
        public Institute InstituteDetails
        {
            get { return _instituteDetails; }
            set { SetProperty(ref _instituteDetails, value); }
        }

        private ImageSource _logoSource;
        public ImageSource LogoSource
        {
            get { return _logoSource; }
            set { SetProperty(ref _logoSource, value); }
        }

        private InstituteCategory _instituteCategory;
        public InstituteCategory InstituteCategory
        {
            get { return _instituteCategory; }
            set { SetProperty(ref _instituteCategory, value); }
        }

        private ObservableCollection<InstituteCategory> _instituteCategoryList;
        public ObservableCollection<InstituteCategory> InstituteCategoryList
        {
            get { return _instituteCategoryList; }
            set { SetProperty(ref _instituteCategoryList, value); }
        }

        private InstituteType _instituteType;

        public InstituteType InstituteType
        {
            get { return _instituteType; }
            set { SetProperty(ref _instituteType, value); }
        }

        private ObservableCollection<InstituteType> _instituteTypeList;
        public ObservableCollection<InstituteType> InstituteTypeList
        {
            get { return _instituteTypeList; }
            set { SetProperty(ref _instituteTypeList, value); }
        }

        private async void SetDefaultFields(int? instituteId)
        {
            InstituteDetails = await _instituteService.GetInstituteDetailByOwnedAsync(instituteId);
            LogoSource = InstituteDetails.LogoUri != null ? ImageSource.FromUri(InstituteDetails.LogoUri) : ImageSource.FromResource("Pigeon.Images.file_upload_icon.png");

            InstituteCategoryList = await _instituteCategoryService.GetInstituteCategoriesAsync();
            InstituteCategory = InstituteCategoryList.Where(x => x.Id == InstituteDetails.InstituteCategoryId).FirstOrDefault();

            InstituteTypeList = await _instituteTypeService.GetInstituteTypesAsync();
            InstituteType = InstituteTypeList.Where(x => x.Id == InstituteDetails.InstituteTypeId).FirstOrDefault();
        }
     
        DelegateCommand _updateCommand;
        public DelegateCommand UpdateCommand => _updateCommand != null ?
            _updateCommand : (_updateCommand = new DelegateCommand(UpdateAsync));

        private async void UpdateAsync()
        {
            var institute = new Institute()
            {
                Name = InstituteDetails.Name,
                Area = InstituteDetails.Area,
                District = InstituteDetails.District,
                Division = InstituteDetails.Division,
                Description = InstituteDetails.Description,
                InstituteCategoryId = InstituteDetails.InstituteCategoryId,
                InstituteTypeId = InstituteDetails.InstituteTypeId,
                Logo = string.IsNullOrEmpty(LogoBlob) ? InstituteDetails.Logo : LogoBlob,
                Id = Convert.ToInt32(InstituteDetails.InstituteId)

            };

            await _commonHelper.ShowLoaderAsync();

            var response = await _instituteService.UpdateInstituteAsync(institute);

            await _navigationService.NavigateAsync("InstituteTabbedPage/InstituteListPage", null, false, false);

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
