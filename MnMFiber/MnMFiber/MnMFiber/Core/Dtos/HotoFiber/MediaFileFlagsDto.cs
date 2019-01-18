namespace MnMFiber.Core.Dtos.HotoFiber
{
    public class MediaFileFlagsDto : BindableDto
    {
        private bool _isMedia1Uploaded;
        public bool IsMedia1Uploaded
        {
            get { return _isMedia1Uploaded; }
            set
            {
                _isMedia1Uploaded = value;
                OnPropertyChanged(nameof(IsMedia1Uploaded));
            }
        }

        private bool _isMedia2Uploaded;
        public bool IsMedia2Uploaded
        {
            get { return _isMedia2Uploaded; }
            set
            {
                _isMedia2Uploaded = value;
                OnPropertyChanged(nameof(IsMedia2Uploaded));
            }
        }
    }
}
