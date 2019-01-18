using System.IO;

namespace MnMFiber.Core.Dtos.HotoFiber
{
    public class MemoryStreamDto : BindableDto
    {
        private MemoryStream _signature1;
        public MemoryStream Signature1
        {
            get { return _signature1; }
            set
            {
                _signature1 = value;
                OnPropertyChanged(nameof(Signature1));
            }
        }

        private MemoryStream _signature2;
        public MemoryStream Signature2
        {
            get { return _signature2; }
            set
            {
                _signature2 = value;
                OnPropertyChanged(nameof(Signature2));
            }
        }

        private MemoryStream _signature3;
        public MemoryStream Signature3
        {
            get { return _signature3; }
            set
            {
                _signature3 = value;
                OnPropertyChanged(nameof(Signature3));
            }
        }
    }
}
