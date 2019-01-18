using Pigeon.Helpers;
using Pigeon.Services.Interface;
using Pigeon.Services.Model;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace Pigeon.ViewModels
{
    public class CreateInstitutePageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IInstituteService _instituteService;
        private readonly IInstituteCategoryService _instituteCategoryService;
        private readonly IInstituteTypeService _instituteTypeService;
        private readonly IChannelService _channelService;
        private FileManageHelper _fileManagerHelper;
        private CommonHelper _commonHelper;
        public CreateInstitutePageViewModel(INavigationService navigationService, IInstituteService instituteService,
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
            this.SetDefaultFieldsValue();
        }

        private ImageSource _logoSource;
        public ImageSource LogoSource
        {
            get { return _logoSource; }
            set { SetProperty(ref _logoSource, value); }
        }
        private void SetDefaultFieldsValue()
        {
            LogoSource = ImageSource.FromResource("Pigeon.Images.file_upload_icon.png");
        }

        private string _instituteName;
        public string InstituteName
        {
            get { return _instituteName; }
            set { SetProperty(ref _instituteName, value); }
        }

        private string _area;
        public string Area
        {
            get { return _area; }
            set { SetProperty(ref _area, value); }
        }

        private string _district;
        public string District
        {
            get { return _district; }
            set { SetProperty(ref _district, value); }
        }

        private string _division;
        public string Division
        {
            get { return _division; }
            set { SetProperty(ref _division, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private int _instituteCategoryId;
        public int InstituteCategoryId
        {
            get { return _instituteCategoryId; }
            set { SetProperty(ref _instituteCategoryId, value); }
        }

        private int _nstituteTypeId;
        public int InstituteTypeId
        {
            get { return _nstituteTypeId; }
            set { SetProperty(ref _nstituteTypeId, value); }
        }

        DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand != null ?
            _saveCommand : (_saveCommand = new DelegateCommand(SaveAsync));

        private async void SaveAsync()
        {
            var institute = new Institute()
            {
                Name = InstituteName,
                Area = Area,
                District = District,
                Division = Division,
                Description = Description,
                InstituteCategoryId = InstituteCategoryId,
                InstituteTypeId = InstituteTypeId,
                Logo = LogoBlob

            };

            await _commonHelper.ShowLoaderAsync();

            var response = await _instituteService.SaveInstituteAsync(institute);

            await _navigationService.NavigateAsync("InstituteTabbedPage", null, false, false);

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

            //await _commonHelper.ShowLoaderAsync();

            _selecteFile = await CrossMedia.Current.PickPhotoAsync();

            LogoSource = ImageSource.FromStream(() =>
            {
                var stream = _selecteFile.GetStream();
                return stream;
            });

            //await _commonHelper.HideLoaderAsync();

            LogoBlob = await _channelService.PostAndGetBlobAsync(_fileManagerHelper.ConvertByteArrayToStream(_fileManagerHelper.ConvertMediaFiletoByteArray(_selecteFile)), ".png");

        }
    }
}
