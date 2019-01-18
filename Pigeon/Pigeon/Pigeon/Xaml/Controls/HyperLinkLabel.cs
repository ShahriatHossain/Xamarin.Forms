using System;
using System.Windows.Input;
using Xamarin.Forms;


namespace XPigeon.Xaml.Controls
{
   
    public class HyperLinkLabel : Label
    {        
        public static readonly BindableProperty SubjectProperty = BindableProperty.Create("Subject", typeof(string),
            typeof(HyperLinkLabel), string.Empty, BindingMode.OneWay, null, null, null, null);

       
        public static readonly BindableProperty NavigateUriProperty = BindableProperty.Create("NavigateUri", typeof(string),
            typeof(HyperLinkLabel), string.Empty, BindingMode.OneWay, null, null, null, null);
                
        public static readonly BindableProperty NavigateCommandProperty = BindableProperty.Create("NavigateCommand", typeof(ICommand),
            typeof(HyperLinkLabel), null, BindingMode.OneWay, null, null, null, null);

        private TapGestureRecognizer _tapGestureRecognizer;

               static HyperLinkLabel()
        {
        }

       
        public HyperLinkLabel()
        {
            NavigateCommand = new Command(() =>
            {
                
            });

            this.TextColor = Color.FromHex("#B3687D");

            _tapGestureRecognizer = new TapGestureRecognizer() { Command = NavigateCommand };

            GestureRecognizers.Add(_tapGestureRecognizer);
        }

       
        public string Subject
        {
            get { return (string)base.GetValue(SubjectProperty); }
            set { base.SetValue(SubjectProperty, value); }
        }
                
        public string NavigateUri
        {
            get { return (string)base.GetValue(NavigateUriProperty); }
            set { base.SetValue(NavigateUriProperty, value); }
        }
            
        public ICommand NavigateCommand
        {
            get { return (ICommand)base.GetValue(NavigateCommandProperty); }
            set { base.SetValue(NavigateCommandProperty, value); }
        }

       protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == "NavigateCommand")
            {
                this.GestureRecognizers.Remove(_tapGestureRecognizer);

                _tapGestureRecognizer = new TapGestureRecognizer() { Command = NavigateCommand };

                GestureRecognizers.Add(_tapGestureRecognizer);
            }
        }        
    }
}