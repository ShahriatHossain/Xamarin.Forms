using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MnMLilon.ViewModels
{
    public abstract class BaseViewModel : BindableBase
    {
        public BaseViewModel()
        {

        }

        private int _transactionId;
        public int TransactionId
        {
            get { return _transactionId; }
            set { SetProperty(ref _transactionId, value); }
        }

        private string _transactionNumber;
        public string TransactionNumber
        {
            get { return _transactionNumber; }
            set { SetProperty(ref _transactionNumber, value); }
        }

        private bool _reAssignedToTechnician;
        public bool ReAssignedToTechnician
        {
            get { return _reAssignedToTechnician; }
            set { SetProperty(ref _reAssignedToTechnician, value); }
        }

    }
}
