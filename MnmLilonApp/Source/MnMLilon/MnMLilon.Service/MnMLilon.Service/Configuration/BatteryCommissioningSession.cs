using MnMLilon.Service.Model;
using Plugin.FilePicker.Abstractions;
using Plugin.Media.Abstractions;
using System.Collections.ObjectModel;
using System.IO;

namespace MnMLilon.Service.Configuration
{
    public class BatteryCommissioningSession
    {
        private static BatteryCommissioningSession _batteryCommissioningSession;
        private static object locker = new object();
        public static BatteryCommissioningSession Current
        {
            get
            {
                if (_batteryCommissioningSession == null)
                {
                    lock (locker)
                    {
                        if (_batteryCommissioningSession == null)
                        {
                            _batteryCommissioningSession = new BatteryCommissioningSession();
                        }
                    }
                }
                return _batteryCommissioningSession;
            }
        }
        public void SetBatteryCommissioningSession(BatteryCommissioningSession batteryCommissioningSession)
        {
            _batteryCommissioningSession = batteryCommissioningSession;
        }


        private int _commissionTransactionId;
        public void SetCommissionTransactionId(int commissionTransactionId)
        {
            _commissionTransactionId = commissionTransactionId;
        }
        public int CommissionTransactionId => _commissionTransactionId;


        private string _commissionTransactionNo;
        public void SetCommissionTransactionNo(string commissionTransactionNo)
        {
            _commissionTransactionNo = commissionTransactionNo;
        }
        public string CommissionTransactionNo => _commissionTransactionNo;


        private bool _isSiteGeneralInformationFilled;
        public void SetIsSiteGeneralInformationFilled(bool isSiteGeneralInformationFilled)
        {
            _isSiteGeneralInformationFilled = isSiteGeneralInformationFilled;
        }
        public bool IsSiteGeneralInformationFilled => _isSiteGeneralInformationFilled;


        private bool _isBatteryGeneralInformationFilled;
        public void SetIsBatteryGeneralInformationFilled(bool isBatteryGeneralInformationFilled)
        {
            _isBatteryGeneralInformationFilled = isBatteryGeneralInformationFilled;
        }
        public bool IsBatteryGeneralInformationFilled => _isBatteryGeneralInformationFilled;


        private bool _isSiteStatusFilled;
        public void SetIsSiteStatusFilled(bool isSiteStatusFilled)
        {
            _isSiteStatusFilled = isSiteStatusFilled;
        }
        public bool IsSiteStatusFilled => _isSiteStatusFilled;


        private bool _isCheckListBeforeInstallationFilled;
        public void SetIsCheckListBeforeInstallationFilled(bool isCheckListBeforeInstallationFilled)
        {
            _isCheckListBeforeInstallationFilled = isCheckListBeforeInstallationFilled;
        }
        public bool IsCheckListBeforeInstallationFilled => _isCheckListBeforeInstallationFilled;


        private bool _isMechanicalInstallationFilled;
        public void SetIsMechanicalInstallationFilled(bool isMechanicalInstallationFilled)
        {
            _isMechanicalInstallationFilled = isMechanicalInstallationFilled;
        }
        public bool IsMechanicalInstallationFilled => _isMechanicalInstallationFilled;


        private bool _isElectricalInstallationFilled;
        public void SetIsElectricalInstallationFilled(bool isElectricalInstallationFilled)
        {
            _isElectricalInstallationFilled = isElectricalInstallationFilled;
        }
        public bool IsElectricalInstallationFilled => _isElectricalInstallationFilled;


        private bool _isBatteryBankDataInformationFilled;
        public void SetIsBatteryBankDataInformationFilled(bool isBatteryBankDataInformationFilled)
        {
            _isBatteryBankDataInformationFilled = isBatteryBankDataInformationFilled;
        }
        public bool IsBatteryBankDataInformationFilled => _isBatteryBankDataInformationFilled;


        private bool _isFunctionalTestOnBatteryFilled;
        public void SetIsFunctionalTestOnBatteryFilled(bool isFunctionalTestOnBatteryFilled)
        {
            _isFunctionalTestOnBatteryFilled = isFunctionalTestOnBatteryFilled;
        }
        public bool IsFunctionalTestOnBatteryFilled => _isFunctionalTestOnBatteryFilled;


        private bool _isPhotosAndSignatureFilled;
        public void SetIsPhotosAndSignatureFilled(bool isPhotosAndSignatureFilled)
        {
            _isPhotosAndSignatureFilled = isPhotosAndSignatureFilled;
        }
        public bool IsPhotosAndSignatureFilled => _isPhotosAndSignatureFilled;


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


        private bool _isPhoto1UploadedFilled;
        public void SetIsPhoto1UploadedFilled(bool isPhoto1UploadedFilled)
        {
            _isPhoto1UploadedFilled = isPhoto1UploadedFilled;
        }
        public bool IsPhoto1UploadedFilled => _isPhoto1UploadedFilled;


        private bool _isPhoto2UploadedFilled;
        public void SetIsPhoto2UploadedFilled(bool isPhoto2UploadedFilled)
        {
            _isPhoto2UploadedFilled = isPhoto2UploadedFilled;
        }
        public bool IsPhoto2UploadedFilled => _isPhoto2UploadedFilled;


        private bool _isFile1UploadedFilled;
        public void SetIsFile1UploadedFilled(bool isFile1UploadedFilled)
        {
            _isFile1UploadedFilled = isFile1UploadedFilled;
        }
        public bool IsFile1UploadedFilled => _isFile1UploadedFilled;


        private ObservableCollection<Category> _bbSrNoList;
        public void SetBBSrNoList(ObservableCollection<Category> bbSrNoList)
        {
            _bbSrNoList = bbSrNoList;
        }
        public ObservableCollection<Category> BBSrNoList => _bbSrNoList;


        private MediaFile _photo1;
        public void SetPhoto1(MediaFile photo1)
        {
            _photo1 = photo1;
        }
        public MediaFile Photo1 => _photo1;


        private MediaFile _photo2;
        public void SetPhoto2(MediaFile photo2)
        {
            _photo2 = photo2;
        }
        public MediaFile Photo2 => _photo2;


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


        private bool _reAssignedToTechnician;
        public void SetReAssignedToTechnician(bool reAssignedToTechnician)
        {
            _reAssignedToTechnician = reAssignedToTechnician;
        }
        public bool ReAssignedToTechnician => _reAssignedToTechnician;


        private bool _isNewMessageArrived;
        public void SetIsNewMessageArrived(bool isNewMessageArrived)
        {
            _isNewMessageArrived = isNewMessageArrived;
        }
        public bool IsNewMessageArrived => _isNewMessageArrived;


        private bool _isBatteryNo1Filled;
        public void SetIsBatteryNo1Filled(bool isBatteryNo1Filled)
        {
            _isBatteryNo1Filled = isBatteryNo1Filled;
        }
        public bool IsBatteryNo1Filled => _isBatteryNo1Filled;


        private bool _isBatteryNo2Filled;
        public void SetIsBatteryNo2Filled(bool isBatteryNo2Filled)
        {
            _isBatteryNo2Filled = isBatteryNo2Filled;
        }
        public bool IsBatteryNo2Filled => _isBatteryNo2Filled;


        private bool _isBatteryNo3Filled;
        public void SetIsBatteryNo3Filled(bool isBatteryNo3Filled)
        {
            _isBatteryNo3Filled = isBatteryNo3Filled;
        }
        public bool IsBatteryNo3Filled => _isBatteryNo3Filled;


        private bool _isBatteryNo4Filled;
        public void SetIsBatteryNo4Filled(bool isBatteryNo4Filled)
        {
            _isBatteryNo4Filled = isBatteryNo4Filled;
        }
        public bool IsBatteryNo4Filled => _isBatteryNo4Filled;


        private bool _isBatteryNo5Filled;
        public void SetIsBatteryNo5Filled(bool isBatteryNo5Filled)
        {
            _isBatteryNo5Filled = isBatteryNo5Filled;
        }
        public bool IsBatteryNo5Filled => _isBatteryNo5Filled;


        private bool _isBatteryNo6Filled;
        public void SetIsBatteryNo6Filled(bool isBatteryNo6Filled)
        {
            _isBatteryNo6Filled = isBatteryNo6Filled;
        }
        public bool IsBatteryNo6Filled => _isBatteryNo6Filled;


        private bool _isBatteryNo7Filled;
        public void SetIsBatteryNo7Filled(bool isBatteryNo7Filled)
        {
            _isBatteryNo7Filled = isBatteryNo7Filled;
        }
        public bool IsBatteryNo7Filled => _isBatteryNo7Filled;


        private bool _isBatteryNo8Filled;
        public void SetIsBatteryNo8Filled(bool isBatteryNo8Filled)
        {
            _isBatteryNo8Filled = isBatteryNo8Filled;
        }
        public bool IsBatteryNo8Filled => _isBatteryNo8Filled;


        private Transaction _transaction;
        public void SetTransaction(Transaction transaction)
        {
            _transaction = transaction;
        }
        public Transaction Transaction => _transaction;
    }
}
