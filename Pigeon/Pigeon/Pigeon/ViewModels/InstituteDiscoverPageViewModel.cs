using Pigeon.Helpers;
using Pigeon.Services.Interface;
using Pigeon.Services.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;

namespace Pigeon.ViewModels
{
    public class InstituteDiscoverPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IInstituteService _instituteService;
        private readonly IChannelService _channelService;
        private CommonHelper _commonHelper;

        public InstituteDiscoverPageViewModel(INavigationService navigationService, IInstituteService instituteService, IChannelService channelService)
        {
            _navigationService = navigationService;
            _instituteService = instituteService;
            _channelService = channelService;
            _commonHelper = new CommonHelper();
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            this.InitiateDefaultValues();
        }

        private ObservableCollection<Institute> _instituteList;
        public ObservableCollection<Institute> InstituteList
        {
            get { return _instituteList; }
            set { SetProperty(ref _instituteList, value); }
        }
        private async void InitiateDefaultValues()
        {
            InstituteList = await _instituteService.GetInstituteBySearchTextAsync(0, string.Empty, false);
        }

        public async void InstituteDetail(Institute institute)
        {
            try
            {
                var param = new NavigationParameters();
                param.Add("instituteId", institute.Id);
                param.Add("instituteName", institute.Name);
                await _navigationService.NavigateAsync("InstituteChannelList", param, false, false);
            }
            catch (Exception ex)
            {

            }
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        DelegateCommand _instituteSearchCommand;
        public DelegateCommand InstituteSearchCommand => _instituteSearchCommand != null ?
            _instituteSearchCommand : (_instituteSearchCommand = new DelegateCommand(InstituteSearchAsync));

        private async void InstituteSearchAsync()
        {
            SearchText = string.IsNullOrWhiteSpace(SearchText) ? string.Empty : SearchText;
            InstituteList = await _instituteService.GetInstituteBySearchTextAsync(0, SearchText, false);
        }


        DelegateCommand<string> _subscribeCommand;
        public DelegateCommand<string> SubscribeCommand => _subscribeCommand != null ?
            _subscribeCommand : (_subscribeCommand = new DelegateCommand<string>(SubscribeChannelAsync));

        private async void SubscribeChannelAsync(string id)
        {
            await _commonHelper.ShowLoaderAsync();
            int instituteId = Convert.ToInt32(id);

            var subscribedInstitute = await _instituteService.GetInstituteDetailBySubscribedAsync(instituteId);
            if (subscribedInstitute != null && subscribedInstitute.Id > 0)
            {
                await _commonHelper.HideLoaderAsync();
                _commonHelper.DisplayAlertMessage("", "You already have subscribed this institute.");
                return;
            }

            var isInstituteSubscribed = await _instituteService.SubscribeInstituteAsync(instituteId);

            if (isInstituteSubscribed)
            {
                var isDefault = true;
                var channel = await _channelService.GetChannelByInstituteByDefault(instituteId, isDefault);

                if (channel != null && channel.Count > 0)
                {
                    var isSubscribed = await _channelService.SubscribeChannelAsync(channel[0].Id);
                    if (isSubscribed)
                    {
                        _commonHelper.SubscribeChannelForNotification(channel[0].Id.ToString());

                        var institute = await _instituteService.GetInstituteDetailAsync(instituteId);
                        this.InstituteDetail(institute);
                    }
                }
            }

            await _commonHelper.HideLoaderAsync();

        }

        DelegateCommand _LoadLargeImage;
        public DelegateCommand LoadLargeImage => _LoadLargeImage != null ?
            _LoadLargeImage : (_LoadLargeImage = new DelegateCommand(LoadLargeImageAsync));

        private void LoadLargeImageAsync()
        {

        }
        public virtual bool? OnBackButtonPressed()
        {
            //false is default value when system call back press
            return false;
        }

        public virtual void OnSoftBackButtonPressed()
        {
            _navigationService.NavigateAsync("InstituteTabbedPage/ChannelListPage", null, false, false);
        }
    }
}
