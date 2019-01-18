namespace Pigeon.Services.Configuration
{
    public class InstituteSession
    {
        private static InstituteSession _instituteSession;
        private static object locker = new object();

        public static InstituteSession Current
        {
            get
            {
                if (_instituteSession == null)
                {
                    lock (locker)
                    {
                        if (_instituteSession == null)
                        {
                            _instituteSession = new InstituteSession();
                        }
                    }
                }
                return _instituteSession;
            }
        }

        private bool _shouldInstituteFabButtonsHide;
        public void SetShouldInstituteFabButtonsHide(bool shouldInstituteFabButtonsHide)
        {
            _shouldInstituteFabButtonsHide = shouldInstituteFabButtonsHide;
        }
        public bool ShouldInstituteFabButtonsHide => _shouldInstituteFabButtonsHide;

        private int _selectedInstitute;
        public void SetSelectedInstitute(int selectedInstitute)
        {
            _selectedInstitute = selectedInstitute;
        }
        public int SelectedInstitute => _selectedInstitute;

        private int _selectedChannel;
        public void SetSelectedChannel(int selectedChannel)
        {
            _selectedChannel = selectedChannel;
        }
        public int SelectedChannel => _selectedChannel;
    }
}
