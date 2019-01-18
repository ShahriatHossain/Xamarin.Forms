namespace MnMFiber.Core.Dtos.HotoFiber
{
    public class TypeOfCrossingFlagsDto : BindableDto
    {
        private bool _isBridgeCrossing;
        public bool IsBridgeCrossing
        {
            get { return _isBridgeCrossing; }
            set
            {
                _isBridgeCrossing = value;
                OnPropertyChanged(nameof(IsBridgeCrossing));
            }
        }


        private bool _isRailwayCrossing;
        public bool IsRailwayCrossing
        {
            get { return _isRailwayCrossing; }
            set
            {
                _isRailwayCrossing = value;
                OnPropertyChanged(nameof(IsRailwayCrossing));
            }
        }


        private bool _isCulvertCrossing;
        public bool IsCulvertCrossing
        {
            get { return _isCulvertCrossing; }
            set
            {
                _isCulvertCrossing = value;
                OnPropertyChanged(nameof(IsCulvertCrossing));
            }
        }


        private bool _isRoadCrossing;
        public bool IsRoadCrossing
        {
            get { return _isRoadCrossing; }
            set
            {
                _isRoadCrossing = value;
                OnPropertyChanged(nameof(IsRoadCrossing));
            }
        }

        private bool _isOthersCrossing;
        public bool IsOthersCrossing
        {
            get { return _isOthersCrossing; }
            set
            {
                _isOthersCrossing = value;
                OnPropertyChanged(nameof(IsOthersCrossing));
            }
        }
    }
}
