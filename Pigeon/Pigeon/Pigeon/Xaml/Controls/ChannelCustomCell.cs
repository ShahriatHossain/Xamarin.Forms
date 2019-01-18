using Xamarin.Forms;

namespace Pigeon.Xaml.Controls
{
    public class ChannelCustomCell : ViewCell
    {
        public static readonly BindableProperty NameProperty =
        BindableProperty.Create("Name", typeof(string), typeof(ChannelCustomCell), "");

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public static readonly BindableProperty ExpiredProperty =
        BindableProperty.Create("Expired", typeof(string), typeof(ChannelCustomCell), "");

        public string Expired
        {
            get { return (string)GetValue(ExpiredProperty); }
            set { SetValue(ExpiredProperty, value); }
        }

        public static readonly BindableProperty NewNoticeIndicatorProperty =
        BindableProperty.Create("NewNoticeIndicator", typeof(string), typeof(ChannelCustomCell), "");

        public string NewNoticeIndicator
        {
            get { return (string)GetValue(NewNoticeIndicatorProperty); }
            set { SetValue(NewNoticeIndicatorProperty, value); }
        }
    }
}
