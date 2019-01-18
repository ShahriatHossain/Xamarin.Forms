using MnMLilon.Helper;
using MnMLilon.Service.Configuration;
using MnMLilon.Service.Interface;
using MnMLilon.Service.Model;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MnMLilon.ViewModels.TroubleTicket
{
    public class DetailsOfWorkDonePageViewModel : BaseViewModel, INavigatedAware
    {
        private readonly INavigationService _navigationService;
        private readonly ITroubleTicketService _troubleTicketService;
        public DetailsOfWorkDonePageViewModel(INavigationService navigationService, ITroubleTicketService troubleTicketService)
        {
            _navigationService = navigationService;
            _troubleTicketService = troubleTicketService;
        }

        DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand => _saveCommand != null ?
            _saveCommand : (_saveCommand = new DelegateCommand(SaveAsync));

        private async void SaveAsync()
        {
            TroubleTicketSession.Current.SetIsDetailsofWorkDoneFilled(true);

            if (StatusComplaint == null)
            {
                CommonHelper.DisplayAlertMessage(string.Empty, "Status of Complaints field can't be empty");
                return;
            }

            SaveToSession();

            await _navigationService.NavigateAsync("TroubleTicketTabListContentPage", null, false, false);
        }

        private void SaveToSession()
        {
            TroubleTicketSession.Current.TroubleTicket.TroubleTicketDetails.AlarmsNotice = AlarmsNotice;
            TroubleTicketSession.Current.TroubleTicket.TroubleTicketDetails.ServiceEngineerRemarks = ServiceEngineerRemarks;
            TroubleTicketSession.Current.TroubleTicket.TroubleTicketDetails.StatusRemarks = StatusRemarks;
            TroubleTicketSession.Current.TroubleTicket.TroubleTicketDetails.ServiceEngineerRemarksWithRca = ServiceEngineerRemarksWithRca;
            TroubleTicketSession.Current.TroubleTicket.TroubleTicketDetails.Recommendation = Recommendation;
            TroubleTicketSession.Current.TroubleTicket.TroubleTicketDetails.StatusOfComplaints = (StatusComplaint == null) ? (int?)null : StatusComplaint.Id;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            GetMakeInputList();

            GetMaterialTypeList();

            SetDefaultFields();
        }

        private ObservableCollection<Category> _statusComplalinList;
        public ObservableCollection<Category> StatusComplalinList
        {
            get { return _statusComplalinList; }
            set { SetProperty(ref _statusComplalinList, value); }
        }
        private void GetMakeInputList()
        {
            StatusComplalinList = new ObservableCollection<Category>()
            {
                new Category { Id=1, Name="Successfully Closed" },
                new Category { Id=2, Name="Pending" }
            };

        }


        private ObservableCollection<Category> _materialTypeList;
        public ObservableCollection<Category> MaterialTypeList
        {
            get { return _materialTypeList; }
            set { SetProperty(ref _materialTypeList, value); }
        }

        private void GetMaterialTypeList()
        {
            MaterialTypeList = new ObservableCollection<Category>()
            {
                new Category { Id=1, Name="New" },
                new Category { Id=2, Name="Repaired" }
            };
        }

        private string _alarmsNotice;
        public string AlarmsNotice
        {
            get { return _alarmsNotice; }
            set { SetProperty(ref _alarmsNotice, value); }
        }

        private string _serviceEngineerRemarks;
        public string ServiceEngineerRemarks
        {
            get { return _serviceEngineerRemarks; }
            set { SetProperty(ref _serviceEngineerRemarks, value); }
        }

        private string _statusRemarks;
        public string StatusRemarks
        {
            get { return _statusRemarks; }
            set { SetProperty(ref _statusRemarks, value); }
        }

        private string _serviceEngineerRemarksWithRca;
        public string ServiceEngineerRemarksWithRca
        {
            get { return _serviceEngineerRemarksWithRca; }
            set { SetProperty(ref _serviceEngineerRemarksWithRca, value); }
        }

        private string _recommendation;
        public string Recommendation
        {
            get { return _recommendation; }
            set { SetProperty(ref _recommendation, value); }
        }

        private string _quantity;
        public string Quantity
        {
            get { return _quantity; }
            set { SetProperty(ref _quantity, value); }
        }

        private string _faultItemSerialNo;
        public string FaultItemSerialNo
        {
            get { return _faultItemSerialNo; }
            set { SetProperty(ref _faultItemSerialNo, value); }
        }

        private string _replacedItemSerialNo;
        public string ReplacedItemSerialNo
        {
            get { return _replacedItemSerialNo; }
            set { SetProperty(ref _replacedItemSerialNo, value); }
        }

        private string _remarks;
        public string Remarks
        {
            get { return _remarks; }
            set { SetProperty(ref _remarks, value); }
        }

        private ObservableCollection<SparePartModel> _sparePartList;
        public ObservableCollection<SparePartModel> SparePartList
        {
            get { return _sparePartList; }
            set { SetProperty(ref _sparePartList, value); }
        }

        private SparePartModel _sparePart;
        public SparePartModel SparePart
        {
            get { return _sparePart; }
            set { SetProperty(ref _sparePart, value); }
        }

        private Category _materialType;
        public Category MaterialType
        {
            get { return _materialType; }
            set { SetProperty(ref _materialType, value); }
        }

        private async void SetDefaultFields()
        {
            SparePartList = await _troubleTicketService.GetSpareParts();

            var ticketModel = TroubleTicketSession.Current.TroubleTicket;

            AlarmsNotice = ticketModel.TroubleTicketDetails.AlarmsNotice;
            ServiceEngineerRemarks = ticketModel.TroubleTicketDetails.ServiceEngineerRemarks;
            StatusRemarks = ticketModel.TroubleTicketDetails.StatusRemarks;
            ServiceEngineerRemarksWithRca = ticketModel.TroubleTicketDetails.ServiceEngineerRemarksWithRca;
            Recommendation = ticketModel.TroubleTicketDetails.Recommendation;

            var statusOfComplaints = ticketModel.TroubleTicketDetails.StatusOfComplaints;
            if (StatusComplalinList != null && statusOfComplaints > 0)
            {
                StatusComplaint = StatusComplalinList.Where(x => x.Id == Convert.ToInt32(statusOfComplaints)).ToList().FirstOrDefault();
            }

            if (TroubleTicketSession.Current.TroubleTicket.TroubleTicketMaterials.Count > 0)
            {
                HasTroubleTicketMaterialAdded = true;
            }

            MaterialType = new Category { Id = 1, Name = "New" };
        }


        private Category _statusComplaint;
        public Category StatusComplaint
        {
            get { return _statusComplaint; }
            set { SetProperty(ref _statusComplaint, value); }
        }

        private bool _hasTroubleTicketMaterialAdded;
        public bool HasTroubleTicketMaterialAdded
        {
            get { return _hasTroubleTicketMaterialAdded; }
            set { SetProperty(ref _hasTroubleTicketMaterialAdded, value); }
        }

        private bool _hasTroubleTicketMaterialSelected;
        public bool HasTroubleTicketMaterialSelected
        {
            get { return _hasTroubleTicketMaterialSelected; }
            set { SetProperty(ref _hasTroubleTicketMaterialSelected, value); }
        }

        private string _partSerialNo;
        public string PartSerialNo
        {
            get { return _partSerialNo; }
            set { SetProperty(ref _partSerialNo, value); }
        }

        DelegateCommand _addMaterialCommand;
        public DelegateCommand AddMaterialCommand => _addMaterialCommand != null ?
            _addMaterialCommand : (_addMaterialCommand = new DelegateCommand(AddMaterial));

        private void AddMaterial()
        {
            if (string.IsNullOrEmpty(Quantity))
            {
                CommonHelper.DisplayAlertMessage(string.Empty, "Quantity field can't be empty.");
                return;
            }

            var troubleTicketMaterial = new TroubleTicketMaterialModel();
            troubleTicketMaterial.Quantity = Convert.ToInt32(Quantity);
            troubleTicketMaterial.Remarks = Remarks;
            troubleTicketMaterial.SparePartId = SparePart.Id;
            troubleTicketMaterial.IsRepaired = (MaterialType.Id == 1) ? false : true;
            troubleTicketMaterial.TicketId = TroubleTicketSession.Current.TroubleTicketId;

            TroubleTicketSession.Current.TroubleTicket.TroubleTicketMaterials.Add(troubleTicketMaterial);

            if (TroubleTicketSession.Current.TroubleTicket.TroubleTicketMaterials.Count > 0)
            {
                HasTroubleTicketMaterialAdded = true;
            }

            Quantity = string.Empty;
            Remarks = string.Empty;
            PartSerialNo = string.Empty;
            HasTroubleTicketMaterialSelected = false;
        }

        DelegateCommand _viewMaterialCommand;
        public DelegateCommand ViewMaterialCommand => _viewMaterialCommand != null ?
            _viewMaterialCommand : (_viewMaterialCommand = new DelegateCommand(ViewMaterial));

        private async void ViewMaterial()
        {
            SaveToSession();

            await _navigationService.NavigateAsync("MaterialsDetailPage", null, true, false);
        }


        DelegateCommand _cancelCommand;
        public DelegateCommand CancelCommand => _cancelCommand != null ?
            _cancelCommand : (_cancelCommand = new DelegateCommand(Cancel));

        private async void Cancel()
        {
            if (TroubleTicketSession.Current.TroubleTicket.TroubleTicketMaterials.Count > 0)
            {
                TroubleTicketSession.Current.TroubleTicket.TroubleTicketMaterials.Clear();
            }

            await _navigationService.NavigateAsync("TroubleTicketTabListContentPage", null, false, false);
        }
    }
}
