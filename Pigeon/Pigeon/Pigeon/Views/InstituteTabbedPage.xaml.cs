using System.Linq;
using Xamarin.Forms;

namespace Pigeon.Views
{
    public partial class InstituteTabbedPage : TabbedPage
    {
        public InstituteTabbedPage()
        {
            InitializeComponent();

            this.Children.Add(new ChannelListPage());
            this.Children.Add(new InstituteListPage());

            
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
