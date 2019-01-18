using MnMLilon.Helper;
using MnMLilon.Service.Configuration;
using Prism.Commands;
using Prism.Navigation;

namespace MnMLilon.ViewModels.TroubleTicket
{
    public class SiteDetailsPageViewModel : BaseViewModel, INavigatedAware
    {
        private readonly INavigationService _navigationService;
        public SiteDetailsPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand != null ?
            _saveCommand : (_saveCommand = new DelegateCommand(SaveAsync));

        private async void SaveAsync()
        {
            TroubleTicketSession.Current.SetIsSiteDetailsFilled(true);

            TroubleTicketSession.Current.TroubleTicket.TroubleTicketDetails.SiteLoadInAmps = CommonHelper.GetTextboxValue(SiteLoadInAmps);
            TroubleTicketSession.Current.TroubleTicket.TroubleTicketDetails.DgAvailable = DgAvailable;
            TroubleTicketSession.Current.TroubleTicket.TroubleTicketDetails.EbAvailable = EbAvailable;

            await _navigationService.NavigateAsync("TroubleTicketTabListContentPage", null, false, false);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            SetDefaultFields();
        }

        private string _siteLoadInAmps;
        public string SiteLoadInAmps
        {
            get { return _siteLoadInAmps; }
            set { SetProperty(ref _siteLoadInAmps, value); }
        }

        private bool _dgAvailable;
        public bool DgAvailable
        {
            get { return _dgAvailable; }
            set { SetProperty(ref _dgAvailable, value); }
        }

        private bool _ebAvailable;
        public bool EbAvailable
        {
            get { return _ebAvailable; }
            set { SetProperty(ref _ebAvailable, value); }
        }
        private void SetDefaultFields()
        {
            var ticketModel = TroubleTicketSession.Current.TroubleTicket;

            SiteLoadInAmps = CommonHelper.SetTextboxValue(ticketModel.TroubleTicketDetails.SiteLoadInAmps.ToString());
            DgAvailable = ticketModel.TroubleTicketDetails.DgAvailable;
            EbAvailable = ticketModel.TroubleTicketDetails.EbAvailable;
        }
    }
}
