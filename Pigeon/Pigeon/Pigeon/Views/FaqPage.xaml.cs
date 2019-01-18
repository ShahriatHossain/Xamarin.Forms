using Pigeon.Helpers;
using Xamarin.Forms;

namespace Pigeon.Views
{
    public partial class FaqPage : ContentPage
    {
        public FaqPage()
        {
            var _commonHelper = new CommonHelper();
            if (_commonHelper.HasNetwork())
            {
                var webView = new WebView
                {
                    Source = "http://webapp-uno.prqvb2ukxu.ap-south-1.elasticbeanstalk.com/faq.htm",
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                Content = webView;
            }
            else
            {
                var label = new Label
                {
                    Text = "No network found!",
                    HorizontalTextAlignment = TextAlignment.Center,
                    Margin = new Thickness(0, 20, 0, 0)
                };
                Content = label;
            }
            

        }
    }
}
