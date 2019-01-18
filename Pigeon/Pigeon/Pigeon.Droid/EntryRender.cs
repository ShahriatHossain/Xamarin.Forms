using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Pigeon.Droid;

[assembly: ExportRenderer(typeof(Entry), typeof(EntryRender))]
namespace Pigeon.Droid
{
    using Android.Views.InputMethods;
    using Xamarin.Forms.Platform.Android;

    public class EntryRender : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
                Control.ImeOptions = (ImeAction)ImeFlags.NoExtractUi;
        }
    }
}