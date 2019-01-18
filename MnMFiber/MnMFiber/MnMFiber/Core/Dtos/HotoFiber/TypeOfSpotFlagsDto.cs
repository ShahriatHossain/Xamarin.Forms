namespace MnMFiber.Core.Dtos.HotoFiber
{
    public class TypeOfSpotFlagsDto : BindableDto
    {
        private bool _isTypeTower;
        public bool IsTypeTower
        {
            get { return _isTypeTower; }
            set
            {
                _isTypeTower = value;
                OnPropertyChanged(nameof(IsTypeTower));
            }
        }

        private bool _isTypeManhole;
        public bool IsTypeManhole
        {
            get { return _isTypeManhole; }
            set
            {
                _isTypeManhole = value;
                OnPropertyChanged(nameof(IsTypeManhole));
            }
        }

        private bool _isTypeHandHole;
        public bool IsTypeHandHole
        {
            get { return _isTypeHandHole; }
            set
            {
                _isTypeHandHole = value;
                OnPropertyChanged(nameof(IsTypeHandHole));
            }
        }

        private bool _isTypeRouteMarker;
        public bool IsTypeRouteMarker
        {
            get { return _isTypeRouteMarker; }
            set
            {
                _isTypeRouteMarker = value;
                OnPropertyChanged(nameof(IsTypeRouteMarker));
            }
        }

        private bool _isFiberOnRoute;
        public bool IsFiberOnRoute
        {
            get { return _isFiberOnRoute; }
            set
            {
                _isFiberOnRoute = value;
                OnPropertyChanged(nameof(IsFiberOnRoute));
            }
        }

        private bool _isTypeOthers;
        public bool IsTypeOthers
        {
            get { return _isTypeOthers; }
            set
            {
                _isTypeOthers = value;
                OnPropertyChanged(nameof(IsTypeOthers));
            }
        }
    }
}
