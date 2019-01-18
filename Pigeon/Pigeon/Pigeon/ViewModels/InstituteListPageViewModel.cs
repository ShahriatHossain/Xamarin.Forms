using Pigeon.Services.Configuration;
using Pigeon.Services.Interface;
using Pigeon.Services.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;

namespace Pigeon.ViewModels
{
    public class InstituteListPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IInstituteService _instituteService;
        public InstituteListPageViewModel(INavigationService navigationService, IInstituteService instituteService)
        {
            _navigationService = navigationService;
            _instituteService = instituteService;

            SetDefaultDataAsync();
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            SetDefaultDataAsync();
        }


        private ObservableCollection<Institute> _myInstituteList;
        public ObservableCollection<Institute> MyInstituteList
        {
            get { return _myInstituteList; }
            set { SetProperty(ref _myInstituteList, value); }
        }

        private ObservableCollection<Institute> _subscribedInstituteList;
        public ObservableCollection<Institute> SubscribedInstituteList
        {
            get { return _subscribedInstituteList; }
            set { SetProperty(ref _subscribedInstituteList, value); }
        }

        private bool _hasRecordForSubscribeInstitute;
        public bool HasRecordForSubscribeInstitute
        {
            get { return _hasRecordForSubscribeInstitute; }
            set { SetProperty(ref _hasRecordForSubscribeInstitute, value); }
        }

        private bool _hasRecordForMyInstitute;
        public bool HasRecordForMyInstitute
        {
            get { return _hasRecordForMyInstitute; }
            set { SetProperty(ref _hasRecordForMyInstitute, value); }
        }

        public async void SetDefaultDataAsync()
        {
            MyInstituteList = await _instituteService.GetInstituteByUserAsync(1);
            SubscribedInstituteList = await _instituteService.GetSubscribeInstituteByUserAsync();

            HasRecordForMyInstitute = (MyInstituteList.Count >= 1) ? false : true;
            HasRecordForSubscribeInstitute = (SubscribedInstituteList.Count >= 1) ? false : true;
        }

        public async void InstituteDetail(Institute institute)
        {
            try
            {
                var param = new NavigationParameters();
                param.Add("instituteId", institute.Id);
                param.Add("instituteName", institute.Name);

                InstituteSession.Current.SetSelectedInstitute(institute.Id);

                await _navigationService.NavigateAsync("InstituteChannelList", param, false, false);
            }
            catch (Exception ex)
            {

            }
        }

        DelegateCommand _LoadLargeImage;
        public DelegateCommand LoadLargeImage => _LoadLargeImage != null ?
            _LoadLargeImage : (_LoadLargeImage = new DelegateCommand(LoadLargeImageAsync));

        private void LoadLargeImageAsync()
        {

        }


        DelegateCommand<string> _EditInstituteInfo;
        public DelegateCommand<string> EditInstituteInfo => _EditInstituteInfo != null ?
            _EditInstituteInfo : (_EditInstituteInfo = new DelegateCommand<string>(EditMyInstituteInfo));

        private async void EditMyInstituteInfo(string id)
        {
            int instituteId = Convert.ToInt32(id);

            var param = new NavigationParameters();
            param.Add("InstituteId", instituteId);
            param.Add("cStatus", string.Empty);
            await _navigationService.NavigateAsync("EditInstitutePage", param, false, false);
        }
    }
}
