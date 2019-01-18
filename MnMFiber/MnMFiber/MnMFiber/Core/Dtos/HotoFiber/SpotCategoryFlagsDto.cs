namespace MnMFiber.Core.Dtos.HotoFiber
{
    public class SpotCategoryFlagsDto : BindableDto
    {
        private bool _isTypeOfSpotsVisited;
        public bool IsTypeOfSpotsVisited
        {
            get { return _isTypeOfSpotsVisited; }
            set
            {
                _isTypeOfSpotsVisited = value;
                OnPropertyChanged(nameof(IsTypeOfSpotsVisited));
            }
        }

        private bool _isFiberDetailsVisited;
        public bool IsFiberDetailsVisited
        {
            get { return _isFiberDetailsVisited; }
            set
            {
                _isFiberDetailsVisited = value;
                OnPropertyChanged(nameof(IsFiberDetailsVisited));
            }
        }

        private bool _isCableDetailVisited;
        public bool IsCableDetailVisited
        {
            get { return _isCableDetailVisited; }
            set
            {
                _isCableDetailVisited = value;
                OnPropertyChanged(nameof(IsCableDetailVisited));
            }
        }

        private bool _isTypeOfCrossingsVisited;
        public bool IsTypeOfCrossingsVisited
        {
            get { return _isTypeOfCrossingsVisited; }
            set
            {
                _isTypeOfCrossingsVisited = value;
                OnPropertyChanged(nameof(IsTypeOfCrossingsVisited));
            }
        }

        private bool _isAerialCableVisited;
        public bool IsAerialCableVisited
        {
            get { return _isAerialCableVisited; }
            set
            {
                _isAerialCableVisited = value;
                OnPropertyChanged(nameof(IsAerialCableVisited));
            }
        }

        private bool _isOtherDetailVisited;
        public bool IsOtherDetailVisited
        {
            get { return _isOtherDetailVisited; }
            set
            {
                _isOtherDetailVisited = value;
                OnPropertyChanged(nameof(IsOtherDetailVisited));
            }
        }

        private bool _isChambersVisited;
        public bool IsChambersVisited
        {
            get { return _isChambersVisited; }
            set
            {
                _isChambersVisited = value;
                OnPropertyChanged(nameof(IsChambersVisited));
            }
        }
    }
}
