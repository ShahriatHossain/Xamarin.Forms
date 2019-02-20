using System.ComponentModel;

namespace Core.Dtos
{
    public class BindableDto : INotifyPropertyChanged
    {
        public BindableDto()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
              new PropertyChangedEventArgs(propertyName));
        }
    }
}
