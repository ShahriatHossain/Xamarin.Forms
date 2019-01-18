using MnMFiber.Core.Dtos.HotoFiber;
using MnMFiber.Core.Interfaces;
using MnMFiber.Core.Repositories;
using MnMFiber.Persistence.Sessions;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace MnMFiber.ViewModels.HotoFiber
{
    public class OtherDetailPageViewModel : BindableBase, IBaseInterface
    {
        INavigationService _navigationService;
        IHotoFiberService _hotoFiberRepository;

        public OtherDetailPageViewModel(INavigationService navigationService, IHotoFiberService hotoFiberRepository)
        {
            _navigationService = navigationService;
            _hotoFiberRepository = hotoFiberRepository;

        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            InitiatePageAsync();
        }

        private TicketSpotDto _ticketSpot;
        public TicketSpotDto TicketSpot
        {
            get { return _ticketSpot; }
            set { SetProperty(ref _ticketSpot, value); }
        }

        private MediaFileFlagsDto _mediaFileFlags;
        public MediaFileFlagsDto MediaFileFlags
        {
            get { return _mediaFileFlags; }
            set { SetProperty(ref _mediaFileFlags, value); }
        }
        public void InitiatePageAsync()
        {
            TicketSpot = HotoFiberSession.Current.TicketSpot;

            MediaFileFlags = HotoFiberSession.Current.MediaFileFlags;
        }

        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand != null ?
            _saveCommand : (_saveCommand = new DelegateCommand(SaveToSession));

        public void SaveToSession()
        {
            HotoFiberSession.Current.SetTicketSpot(TicketSpot);

            HotoFiberSession.Current.SpotCategoryFlags.IsOtherDetailVisited = true;

            _navigationService.NavigateAsync("AddSpotPage", null, false, false);
        }

        private DelegateCommand _fileUploadCommand;
        public DelegateCommand FileUploadCommand => _fileUploadCommand != null ?
            _fileUploadCommand : (_fileUploadCommand = new DelegateCommand(TakePhotoAsync));

        private async void TakePhotoAsync()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "Sample",
                PhotoSize = PhotoSize.Medium,
                CompressionQuality = 92
            });

            if (file == null)
                return;

            HotoFiberSession.Current.MediaFiles.Media1 = file;
            HotoFiberSession.Current.SetMediaFiles(HotoFiberSession.Current.MediaFiles);

            MediaFileFlags.IsMedia1Uploaded = true;
        }
    }
}
