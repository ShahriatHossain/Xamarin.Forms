using MnMFiber.Core.Dtos.HotoFiber;
using MnMFiber.Core.Models.HotoFiber;

namespace MnMFiber.Persistence.Sessions
{
    public class HotoFiberSession
    {
        private static HotoFiberSession _hotoFiberSession;
        private static object locker = new object();
        public static HotoFiberSession Current
        {
            get
            {
                if (_hotoFiberSession == null)
                {
                    lock (locker)
                    {
                        if (_hotoFiberSession == null)
                        {
                            _hotoFiberSession = new HotoFiberSession();
                        }
                    }
                }
                return _hotoFiberSession;
            }
        }

        public void SetHotoFiberSession(HotoFiberSession hotoFiberSession)
        {
            _hotoFiberSession = hotoFiberSession;
        }


        private TicketSpotDto _ticketSpot;
        public void SetTicketSpot(TicketSpotDto ticketSpot)
        {
            _ticketSpot = ticketSpot;
        }
        public TicketSpotDto TicketSpot => _ticketSpot;

        private TicketBasicDto _ticketBasic;
        public void SetTicketBasic(TicketBasicDto ticketBasic)
        {
            _ticketBasic = ticketBasic;
        }
        public TicketBasicDto TicketBasic => _ticketBasic;


        private SpotCategoryFlagsDto _spotCategoryFlags;
        public void SetSpotCategoryFlags(SpotCategoryFlagsDto spotCategoryFlags)
        {
            _spotCategoryFlags = spotCategoryFlags;
        }
        public SpotCategoryFlagsDto SpotCategoryFlags => _spotCategoryFlags;


        private SignatureFlagsDto _signatureFlags;
        public void SetSignatureFlags(SignatureFlagsDto signatureFlags)
        {
            _signatureFlags = signatureFlags;
        }
        public SignatureFlagsDto SignatureFlags => _signatureFlags;


        private TicketSignatureUploadModel _signatureDetail;
        public void SetSignatureDetail(TicketSignatureUploadModel signatureDetail)
        {
            _signatureDetail = signatureDetail;
        }
        public TicketSignatureUploadModel SignatureDetail => _signatureDetail;


        private string _selectedSignatureId;
        public void SetSelectedSignatureId(string selectedSignatureId)
        {
            _selectedSignatureId = selectedSignatureId;
        }
        public string SelectedSignatureId => _selectedSignatureId;


        private MediaDto _mediaFiles;
        public void SetMediaFiles(MediaDto mediaFiles)
        {
            _mediaFiles = mediaFiles;
        }
        public MediaDto MediaFiles => _mediaFiles;


        private MemoryStreamDto _memoryStreams;
        public void SetMemoryStreams(MemoryStreamDto memoryStreams)
        {
            _memoryStreams = memoryStreams;
        }
        public MemoryStreamDto MemoryStreams => _memoryStreams;


        private TypeOfCrossingFlagsDto _typeOfCrossingFlags;
        public void SetTypeOfCrossingFlags(TypeOfCrossingFlagsDto typeOfCrossingFlags)
        {
            _typeOfCrossingFlags = typeOfCrossingFlags;
        }
        public TypeOfCrossingFlagsDto TypeOfCrossingFlags => _typeOfCrossingFlags;


        private string _addspotBackPage;
        public void SetAddSpotBackPage(string addspotBackPage)
        {
            _addspotBackPage = addspotBackPage;
        }
        public string AddSpotBackPage => _addspotBackPage;


        private MediaFileFlagsDto _mediaFileFlags;
        public void SetMediaFileFlags(MediaFileFlagsDto mediaFileFlags)
        {
            _mediaFileFlags = mediaFileFlags;
        }
        public MediaFileFlagsDto MediaFileFlags => _mediaFileFlags;
    }
}
