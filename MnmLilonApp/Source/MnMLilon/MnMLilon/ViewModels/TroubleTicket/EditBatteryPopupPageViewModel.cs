using Prism.Mvvm;
using Prism.Navigation;

namespace MnMLilon.ViewModels.TroubleTicket
{
    public class EditBatteryPopupPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        public EditBatteryPopupPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public async System.Threading.Tasks.Task NavigateToBatteryDetailPageAsync()
        {
            await _navigationService.NavigateAsync("BatteryDetailsPage", null, false, false);
        }
    }
}
