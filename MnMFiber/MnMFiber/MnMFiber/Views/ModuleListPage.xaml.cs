using System.Linq;
using Xamarin.Forms;

namespace MnMFiber.Views
{
    public partial class ModuleListPage : ContentPage
    {
        public ModuleListPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            ClearNavigationStack();
            return false;
        }
        private void ClearNavigationStack()
        {
            var existingPages = Navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                if (page != this)
                    Navigation.RemovePage(page);
            }
        }
    }
}
