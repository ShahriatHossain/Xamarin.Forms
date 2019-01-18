using MnMLilon.Helper;
using MnMLilon.Service.Configuration;
using MnMLilon.Service.Interface;
using MnMLilon.Service.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Linq;

namespace MnMLilon.ViewModels.TroubleTicket
{
    public class MaterialsDetailPageViewModel : BindableBase, INavigatedAware
    {
        private readonly INavigationService _navigationService;
        private readonly ITroubleTicketService _troubleTicketService;
        public MaterialsDetailPageViewModel(INavigationService navigationService, ITroubleTicketService troubleTicketService)
        {
            _navigationService = navigationService;
            _troubleTicketService = troubleTicketService;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            SetDefaultFields();
        }

        private ObservableCollection<CustomTroubleTicketMaterialModel> _materialList;
        public ObservableCollection<CustomTroubleTicketMaterialModel> MaterialList
        {
            get { return _materialList; }
            set { SetProperty(ref _materialList, value); }
        }

        private async void SetDefaultFields()
        {
            var sparePartList = await _troubleTicketService.GetSpareParts();
            var materialsList = TroubleTicketSession.Current.TroubleTicket.TroubleTicketMaterials;
            MaterialList = new ObservableCollection<CustomTroubleTicketMaterialModel>();

            if (materialsList != null)
            {
                foreach (var item in materialsList)
                {
                    var sparePart = sparePartList.Single(x => x.Id == item.SparePartId);

                    var customMaterial = new CustomTroubleTicketMaterialModel();
                    customMaterial.Id = item.Id;
                    customMaterial.SparePartId = sparePart.Id;
                    customMaterial.PartDescription = sparePart.PartDescription;
                    customMaterial.PartSerialNo = sparePart.PartSerialNo;
                    customMaterial.Quantity = item.Quantity;
                    customMaterial.Remarks = item.Remarks;
                    customMaterial.MaterialType = item.IsRepaired ? "Repaired" : "New";

                    MaterialList.Add(customMaterial);
                }
            }
        }


        public async void RemoveMaterial(CustomTroubleTicketMaterialModel materialModel)
        {
            var canDelete = await CommonHelper.AlertMessageWithCancelOrOkAsync("", "Do you want to remove?");
            if (canDelete)
            {
                var indexOfItem = MaterialList.IndexOf(materialModel);

                MaterialList.Remove(materialModel);

                if (materialModel != null)
                {
                    RemoveMaterialFromSession(indexOfItem);
                }
            }
        }

        private static void RemoveMaterialFromSession(int indexOfItem)
        {
            TroubleTicketSession.Current.TroubleTicket.TroubleTicketMaterials.RemoveAt(indexOfItem);
        }


        DelegateCommand _dismissCommand;
        public DelegateCommand DismissCommand => _dismissCommand != null ?
            _dismissCommand : (_dismissCommand = new DelegateCommand(Dissmiss));

        private async void Dissmiss()
        {
            await _navigationService.GoBackAsync();
        }
    }
}
