using MnMLilon.Helper;
using MnMLilon.Service.Configuration;
using MnMLilon.Service.Interface;
using MnMLilon.Service.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;

namespace MnMLilon.ViewModels.TroubleTicket
{
    public class TroubleTicketTransactionListContentPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly ITroubleTicketService _troubleTicketService;
        public TroubleTicketTransactionListContentPageViewModel(INavigationService navigationService, ITroubleTicketService troubleTicketService)
        {
            _navigationService = navigationService;
            _troubleTicketService = troubleTicketService;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            GetAllPendingTickets();

            TroubleTicketSession.Current.SetIsNewMessageArrived(false);

            CheckDeviceLocationEnabled();
        }

        private async void CheckDeviceLocationEnabled()
        {
            var isLocaltionEnabled = await CommonHelper.IsLocationEnabledFromDevice();
            if (!isLocaltionEnabled)
            {
                CommonHelper.DisplayAlertMessage("", "Please enable device location.");
            }
        }

        private ObservableCollection<PendingTroubleTicketModel> _pendingTicketList;
        public ObservableCollection<PendingTroubleTicketModel> PendingTicketList
        {
            get { return _pendingTicketList; }
            set { SetProperty(ref _pendingTicketList, value); }
        }
        private async void GetAllPendingTickets()
        {
            var technicianNumber = UserSession.Current.UserName;
            PendingTicketList = await _troubleTicketService.GetAllPendingTicketsAsync(technicianNumber);
        }


        DelegateCommand _navToHomeCommand;
        public DelegateCommand NavToHomeCommand => _navToHomeCommand ?? (_navToHomeCommand = new DelegateCommand(GoToHome));
        private async void GoToHome()
        {
            await _navigationService.NavigateAsync("CategoryContentPage", null, false, false);
        }


        public async void NavigateToTabListAsync(PendingTroubleTicketModel troubleTicket)
        {
            await CommonHelper.ShowLoaderAsync();

            TroubleTicketSession.Current.SetPendingTroubleTicket(troubleTicket);

            TroubleTicketSession.Current.SetTroubleTicketId(Convert.ToInt32(troubleTicket.Id));
            TroubleTicketSession.Current.SetTroubleTicketSiteId(Convert.ToInt32(troubleTicket.SiteId));

            var troubleTicketDetails = await _troubleTicketService.GetTicketDetails(Convert.ToInt32(troubleTicket.Id));
            TroubleTicketSession.Current.SetTroubleTicket(troubleTicketDetails);

            if (troubleTicketDetails.SiteDetails != null)
            {
                var Latitude = (troubleTicketDetails.SiteDetails.Latitude == null) ? 0.0 : (Double)troubleTicketDetails.SiteDetails.Latitude;
                var Longitude = (troubleTicketDetails.SiteDetails.Longitude == null) ? 0.0 : (Double)troubleTicketDetails.SiteDetails.Longitude;

                var isLocationCorrect = await CheckUserInCorrectLocationAsync(Latitude, Longitude);

                if (isLocationCorrect)
                {
                    await _navigationService.NavigateAsync("TroubleTicketTabListContentPage", null, false, false);
                }

            }
            else
            {
                await _navigationService.NavigateAsync("TroubleTicketTabListContentPage", null, false, false);
            }

            await CommonHelper.HideLoaderAsync();
        }

        private async System.Threading.Tasks.Task<bool> CheckUserInCorrectLocationAsync(double latitude, double longitude)
        {
            var isLocationEnabled = await CommonHelper.IsLocationEnabledFromDevice();

            if (!isLocationEnabled)
            {
                CommonHelper.DisplayAlertMessage("", "Please enable your device location.");
                return false;
            }
            else
            {
                //var isUserInRange = await CommonHelper.IsUserInDesiredLocation(latitude, longitude);
                //var location = await CommonHelper.GetDeviceCurrentLocation();
                //if (!isUserInRange)
                //{
                //    CommonHelper.DisplayAlertMessage("", "You are not in the desired location and your current device location is Latitude: " + location.Latitude + "\nLongitude: " + location.Longitude + " \n and server location is: \nLatitude: " + latitude + "\nLongitude: " + longitude);
                //    return false;
                //}
            }

            return true;
        }
    }
}
