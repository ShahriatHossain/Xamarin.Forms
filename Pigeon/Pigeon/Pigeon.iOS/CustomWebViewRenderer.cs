using Foundation;
using System.ComponentModel;
using System.IO;
using System.Net;
using UIKit;
using Pigeon.ios;
using Pigeon.Xaml.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace Pigeon.ios
{
    public class CustomWebViewRenderer : ViewRenderer<CustomWebView, UIWebView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<CustomWebView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                SetNativeControl(new UIWebView());
            }
            //if (e.OldElement != null)
            //{
            //    // Cleanup
            //}
            //if (e.NewElement != null)
            //{
            //    var customWebView = Element as CustomWebView;
            //    string fileName = Path.Combine(NSBundle.MainBundle.BundlePath, string.Format("Content/{0}", WebUtility.UrlEncode(customWebView.Uri)));
            //    Control.LoadRequest(new NSUrlRequest(new NSUrl(fileName, false)));
            //    Control.ScalesPageToFit = true;
            //}
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if ("Uri".Equals(e.PropertyName))
            {
                var uri = ((CustomWebView)sender).Uri;
                //string fileName = Path.Combine(NSBundle.MainBundle.BundlePath,
                //    string.Format("Content/{0}", WebUtility.UrlEncode(uri)));
                Control.LoadRequest(new NSUrlRequest(new NSUrl(uri, false)));
                Control.ScalesPageToFit = true;
            }
        }
    }
}