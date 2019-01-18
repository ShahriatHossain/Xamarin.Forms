using MnMLilon.Service.Configuration;
using MnMLilon.Service.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MnMLilon.ViewModels.TroubleTicket
{
    public class GeneralInformationPageViewModel : BindableBase, INavigatedAware
    {
        private readonly INavigationService _navigationService;
        public GeneralInformationPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }


        DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand != null ?
            _saveCommand : (_saveCommand = new DelegateCommand(SaveAsync));

        private async void SaveAsync()
        {
            TroubleTicketSession.Current.SetIsGeneralInformationFilled(true);

            TroubleTicketSession.Current.TroubleTicket.TroubleTicketDetails.NoOfModulesAtSite = string.IsNullOrEmpty(NoOfModulesAtSite) ? (int?)null : Convert.ToInt32(NoOfModulesAtSite);
            TroubleTicketSession.Current.TroubleTicket.TroubleTicketDetails.NoOfJunctionBoxAtSite = string.IsNullOrEmpty(NoOfJunctionBoxAtSite) ? (int?)null : Convert.ToInt32(NoOfJunctionBoxAtSite);
            TroubleTicketSession.Current.TroubleTicket.TroubleTicketDetails.SiteObservation = SiteObservation;
            TroubleTicketSession.Current.TroubleTicket.TroubleTicketDetails.SolutionImparted = SolutionImparted;
            TroubleTicketSession.Current.TroubleTicket.TroubleTicketDetails.ProblemRelatedId = ProblemRelated.Id;

            await _navigationService.NavigateAsync("TroubleTicketTabListContentPage", null, false, false);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            SetDefaultFields();
        }

        private string _noOfModulesAtSite;
        public string NoOfModulesAtSite
        {
            get { return _noOfModulesAtSite; }
            set { SetProperty(ref _noOfModulesAtSite, value); }
        }

        private string _noOfJunctionBoxAtSite;
        public string NoOfJunctionBoxAtSite
        {
            get { return _noOfJunctionBoxAtSite; }
            set { SetProperty(ref _noOfJunctionBoxAtSite, value); }
        }

        private string _siteObservation;

        public string SiteObservation
        {
            get { return _siteObservation; }
            set { SetProperty(ref _siteObservation, value); }
        }

        private string _solutionImparted;
        public string SolutionImparted
        {
            get { return _solutionImparted; }
            set { SetProperty(ref _solutionImparted, value); }
        }

        private Category _problemRelated;
        public Category ProblemRelated
        {
            get { return _problemRelated; }
            set { SetProperty(ref _problemRelated, value); }
        }

        private ObservableCollection<Category> _problemRelatedList;
        public ObservableCollection<Category> ProblemRelatedList
        {
            get { return _problemRelatedList; }
            set { SetProperty(ref _problemRelatedList, value); }
        }

        private void SetDefaultFields()
        {
            var ticketModel = TroubleTicketSession.Current.TroubleTicket;

            NoOfModulesAtSite = ticketModel.TroubleTicketDetails.NoOfModulesAtSite.ToString();
            NoOfJunctionBoxAtSite = ticketModel.TroubleTicketDetails.NoOfJunctionBoxAtSite.ToString();
            SiteObservation = ticketModel.TroubleTicketDetails.SiteObservation;
            SolutionImparted = ticketModel.TroubleTicketDetails.SolutionImparted;

            ProblemRelatedList = new ObservableCollection<Category>()
            {
                new Category { Id=1, Name="Mahindra" },
                new Category { Id=2, Name="Customer" }
            };

            if (ticketModel.TroubleTicketDetails.ProblemRelatedId != null
                && ticketModel.TroubleTicketDetails.ProblemRelatedId > 0)
            {
                ProblemRelated = ProblemRelatedList
                    .FirstOrDefault(x => x.Id == ticketModel.TroubleTicketDetails.ProblemRelatedId);
            }
            else
                ProblemRelated = ProblemRelatedList.FirstOrDefault();
        }
    }
}
