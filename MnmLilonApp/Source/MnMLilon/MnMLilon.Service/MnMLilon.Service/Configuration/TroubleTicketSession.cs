using MnMLilon.Service.Model;
using Plugin.FilePicker.Abstractions;
using System.IO;

namespace MnMLilon.Service.Configuration
{
    public class TroubleTicketSession
    {
        private static TroubleTicketSession _troubleTicketSession;
        private static object locker = new object();
        public static TroubleTicketSession Current
        {
            get
            {
                if (_troubleTicketSession == null)
                {
                    lock (locker)
                    {
                        if (_troubleTicketSession == null)
                        {
                            _troubleTicketSession = new TroubleTicketSession();
                        }
                    }
                }
                return _troubleTicketSession;
            }
        }
        public void SetTroubleTicketSession(TroubleTicketSession troubleTicketSession)
        {
            _troubleTicketSession = troubleTicketSession;
        }


        private int _troubleTicketId;
        public void SetTroubleTicketId(int troubleTicketId)
        {
            _troubleTicketId = troubleTicketId;
        }
        public int TroubleTicketId => _troubleTicketId;


        private int _troubleTicketSiteId;
        public void SetTroubleTicketSiteId(int troubleTicketSiteId)
        {
            _troubleTicketSiteId = troubleTicketSiteId;
        }
        public int TroubleTicketSiteId => _troubleTicketSiteId;


        private bool _isGeneralInformationFilled;
        public void SetIsGeneralInformationFilled(bool isGeneralInformationFilled)
        {
            _isGeneralInformationFilled = isGeneralInformationFilled;
        }
        public bool IsGeneralInformationFilled => _isGeneralInformationFilled;


        private bool _isEquipmentDetailsFilled;
        public void SetIsEquipmentDetailsFilled(bool isEquipmentDetailsFilled)
        {
            _isEquipmentDetailsFilled = isEquipmentDetailsFilled;
        }
        public bool IsEquipmentDetailsFilled => _isEquipmentDetailsFilled;


        private bool _isSiteDetailsFilled;
        public void SetIsSiteDetailsFilled(bool isSiteDetailsFilled)
        {
            _isSiteDetailsFilled = isSiteDetailsFilled;
        }
        public bool IsSiteDetailsFilled => _isSiteDetailsFilled;


        private bool _isDetailsofWorkDoneFilled;
        public void SetIsDetailsofWorkDoneFilled(bool isDetailsofWorkDoneFilled)
        {
            _isDetailsofWorkDoneFilled = isDetailsofWorkDoneFilled;
        }
        public bool IsDetailsofWorkDoneFilled => _isDetailsofWorkDoneFilled;


        private bool _isFunctionalTestonBatteryFilled;
        public void SetIsFunctionalTestonBatteryFilled(bool isFunctionalTestonBatteryFilled)
        {
            _isFunctionalTestonBatteryFilled = isFunctionalTestonBatteryFilled;
        }
        public bool IsFunctionalTestonBatteryFilled => _isFunctionalTestonBatteryFilled;


        private bool _isCustomerRemarksFilled;
        public void SetIsCustomerRemarksFilled(bool isCustomerRemarksFilled)
        {
            _isCustomerRemarksFilled = isCustomerRemarksFilled;
        }
        public bool IsCustomerRemarksFilled => _isCustomerRemarksFilled;


        private bool _isSignatureFilled;
        public void SetIsSignatureFilled(bool isSignatureFilled)
        {
            _isSignatureFilled = isSignatureFilled;
        }
        public bool IsSignatureFilled => _isSignatureFilled;


        private bool _isBatteryDetailsFilled;
        public void SetIsBatteryDetailsFilled(bool isBatteryDetailsFilled)
        {
            _isBatteryDetailsFilled = isBatteryDetailsFilled;
        }
        public bool IsBatteryDetailsFilled => _isBatteryDetailsFilled;


        private bool _isPowerPlantNOtherDetailsFilled;
        public void SetIsPowerPlantNOtherDetailsFilled(bool isPowerPlantNOtherDetailsFilled)
        {
            _isPowerPlantNOtherDetailsFilled = isPowerPlantNOtherDetailsFilled;
        }
        public bool IsPowerPlantNOtherDetailsFilled => _isPowerPlantNOtherDetailsFilled;


        private TroubleTicketViewModel _troubleTicket;
        public void SetTroubleTicket(TroubleTicketViewModel troubleTicket)
        {
            _troubleTicket = troubleTicket;
        }
        public TroubleTicketViewModel TroubleTicket => _troubleTicket;


        private PendingTroubleTicketModel _pendingTroubleTicket;
        public void SetPendingTroubleTicket(PendingTroubleTicketModel pendingTroubleTicket)
        {
            _pendingTroubleTicket = pendingTroubleTicket;
        }
        public PendingTroubleTicketModel PendingTroubleTicket => _pendingTroubleTicket;


        private bool _isSignature1TakenFilled;
        public void SetIsSignature1TakenFilled(bool isSignature1TakenFilled)
        {
            _isSignature1TakenFilled = isSignature1TakenFilled;
        }
        public bool IsSignature1TakenFilled => _isSignature1TakenFilled;


        private bool _isSignature2TakenFilled;
        public void SetIsSignature2TakenFilled(bool isSignature2TakenFilled)
        {
            _isSignature2TakenFilled = isSignature2TakenFilled;
        }
        public bool IsSignature2TakenFilled => _isSignature2TakenFilled;


        private bool _isFile1UploadedFilled;
        public void SetIsFile1UploadedFilled(bool isFile1UploadedFilled)
        {
            _isFile1UploadedFilled = isFile1UploadedFilled;
        }
        public bool IsFile1UploadedFilled => _isFile1UploadedFilled;


        private FileData _file1;
        public void SetFile1(FileData file1)
        {
            _file1 = file1;
        }
        public FileData File1 => _file1;


        private MemoryStream _signature1;
        public void SetSignature1(MemoryStream signature1)
        {
            _signature1 = signature1;
        }
        public MemoryStream Signature1 => _signature1;


        private MemoryStream _signature2;
        public void SetSignature2(MemoryStream signature2)
        {
            _signature2 = signature2;
        }
        public MemoryStream Signature2 => _signature2;


        private string _customerContactPersonName;
        public void SetCustomerContactPersonName(string customerContactPersonName)
        {
            _customerContactPersonName = customerContactPersonName;
        }
        public string CustomerContactPersonName => _customerContactPersonName;


        private string _customerContactPersonMobile;
        public void SetCustomerContactPersonMobile(string customerContactPersonMobile)
        {
            _customerContactPersonMobile = customerContactPersonMobile;
        }
        public string CustomerContactPersonMobile => _customerContactPersonMobile;


        private string _customerContactPersonDesignation;
        public void SetCustomerContactPersonDesignation(string customerContactPersonDesignation)
        {
            _customerContactPersonDesignation = customerContactPersonDesignation;
        }
        public string CustomerContactPersonDesignation => _customerContactPersonDesignation;


        private bool _isNewMessageArrived;
        public void SetIsNewMessageArrived(bool isNewMessageArrived)
        {
            _isNewMessageArrived = isNewMessageArrived;
        }
        public bool IsNewMessageArrived => _isNewMessageArrived;


        private bool _isFaultyAssetsSelected;
        public void SetIsFaultyAssetsSelected(bool isFaultyAssetsSelected)
        {
            _isFaultyAssetsSelected = isFaultyAssetsSelected;
        }
        public bool IsFaultyAssetsSelected => _isFaultyAssetsSelected;
    }
}
