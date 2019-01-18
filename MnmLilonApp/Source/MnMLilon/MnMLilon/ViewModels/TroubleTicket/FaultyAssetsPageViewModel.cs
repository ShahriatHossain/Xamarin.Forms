using MnMLilon.Service.Configuration;
using MnMLilon.Service.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Linq;

namespace MnMLilon.ViewModels.TroubleTicket
{
    public class FaultyAssetsPageViewModel : BindableBase, INavigatedAware
    {
        public readonly INavigationService _navigationService;
        public FaultyAssetsPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            InitiatePageWith();
        }

        private ObservableCollection<TroubleTicketFaultyAsset> _faultyAssetList
            = new ObservableCollection<TroubleTicketFaultyAsset>();
        public ObservableCollection<TroubleTicketFaultyAsset> FaultyAssetList
        {
            get { return _faultyAssetList; }
            set { SetProperty(ref _faultyAssetList, value); }
        }
        private void InitiatePageWith()
        {
            var troubleTicketId = TroubleTicketSession.Current.TroubleTicketId;

            if (TroubleTicketSession.Current.TroubleTicket.TroubleTicketFaultyAssets != null
                && TroubleTicketSession.Current.TroubleTicket.TroubleTicketFaultyAssets.Count > 0)
            {
                foreach (var item in TroubleTicketSession.Current.TroubleTicket.TroubleTicketFaultyAssets)
                {
                    FaultyAssetList.Add(item);
                }
            }
            else
            {
                FaultyAssetList = new ObservableCollection<TroubleTicketFaultyAsset>()
                {
                    new TroubleTicketFaultyAsset()
                    {
                        Id = 1,
                        SerialNo = string.Empty,
                        TicketId = troubleTicketId
                    },
                    new TroubleTicketFaultyAsset()
                    {
                        Id = 2,
                        SerialNo = string.Empty,
                        TicketId = troubleTicketId
                    },
                    new TroubleTicketFaultyAsset()
                    {
                        Id = 3,
                        SerialNo = string.Empty,
                        TicketId = troubleTicketId
                    },
                    new TroubleTicketFaultyAsset()
                    {
                        Id = 4,
                        SerialNo = string.Empty,
                        TicketId = troubleTicketId
                    }
                };
            }

        }


        DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(SaveAsync));

        private async void SaveAsync()
        {
            TroubleTicketSession.Current.SetIsFaultyAssetsSelected(true);
            TroubleTicketSession.Current.TroubleTicket.TroubleTicketFaultyAssets
                = FaultyAssetList.ToList();

            await _navigationService.NavigateAsync("TroubleTicketTabListContentPage", null, false, false);
        }
    }
}
