using Plugin.Media.Abstractions;

namespace MnMFiber.Core.Dtos.HotoFiber
{
    public class MediaDto : BindableDto
    {
        private MediaFile _media1;
        public MediaFile Media1
        {
            get { return _media1; }
            set
            {
                _media1 = value;
                OnPropertyChanged(nameof(Media1));
            }
        }

        private MediaFile _media2;
        public MediaFile Media2
        {
            get { return _media2; }
            set
            {
                _media2 = value;
                OnPropertyChanged(nameof(Media2));
            }
        }
    }
}
