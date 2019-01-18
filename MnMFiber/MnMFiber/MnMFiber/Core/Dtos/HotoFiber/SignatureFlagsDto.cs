namespace MnMFiber.Core.Dtos.HotoFiber
{
    public class SignatureFlagsDto : BindableDto
    {
        private bool _isSignature1Visited;
        public bool IsSignature1Visited
        {
            get { return _isSignature1Visited; }
            set
            {
                _isSignature1Visited = value;
                OnPropertyChanged(nameof(IsSignature1Visited));
            }
        }

        private bool _isSignature2Visited;
        public bool IsSignature2Visited
        {
            get { return _isSignature2Visited; }
            set
            {
                _isSignature2Visited = value;
                OnPropertyChanged(nameof(IsSignature2Visited));
            }
        }

        private bool _isSignature3Visited;
        public bool IsSignature3Visited
        {
            get { return _isSignature3Visited; }
            set
            {
                _isSignature3Visited = value;
                OnPropertyChanged(nameof(IsSignature3Visited));
            }
        }
    }
}
