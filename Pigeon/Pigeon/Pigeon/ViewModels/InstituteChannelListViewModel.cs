using Pigeon.Helpers;
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
    public class InstituteChannelListViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IChannelService _channelService;
        private readonly IInstituteService _instituteService;
        private readonly IChannelCategoryService _channelCategoryService;
        private CommonHelper _commonHelper;

        public InstituteChannelListViewModel(INavigationService navigationService, IChannelService channelService,
            IInstituteService instituteService, IChannelCategoryService channelCategoryService)
        {
            _navigationService = navigationService;
            _channelService = channelService;
            _instituteService = instituteService;
            _channelCategoryService = channelCategoryService;
            _commonHelper = new CommonHelper();
        }



        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            int instituteId = Convert.ToInt32(parameters["instituteId"]);
            var instituteName = Convert.ToString(parameters["instituteName"]);
            this.SetDefaultFieldsAsync(instituteId, instituteName);
        }

        private ObservableCollection<ChannelApi> _channelList;
        public ObservableCollection<ChannelApi> ChannelList
        {
            get { return _channelList; }
            set { SetProperty(ref _channelList, value); }
        }

        private Institute _institute;
        public Institute Institute
        {
            get { return _institute; }
            set { SetProperty(ref _institute, value); }
        }

        private async void SetDefaultFieldsAsync(int instituteId, string instituteName)
        {
            try
            {
                ChannelList = await _channelService.GetChannelByInstituteAsync(instituteId);

                Institute = await _instituteService.GetInstituteDetailAsync(instituteId);
            }
            catch (Exception ex)
            {

            }

        }

        public async void CreateChannel()
        {
            var param = new NavigationParameters();
            param.Add("instituteId", Institute.Id);
            param.Add("instituteName", Institute.Name);
            await _navigationService.NavigateAsync("ChannelCreatePage", param, false, false);
        }

        public async void ChannelDetail(ChannelApi channel)
        {
            try
            {
                var param = new NavigationParameters();
                param.Add("channelId", channel.Id);
                param.Add("cStatus", string.Empty);

                InstituteSession.Current.SetSelectedChannel(Convert.ToInt32(channel.Id));

                if (channel.OwnerFlag)
                {
                    await _navigationService.NavigateAsync("ChannelDetailViewer", param, false, false);
                }
                else
                {
                    var subscribedChannel = await _channelService.GetChannelDetailBySubscribedAsync(Convert.ToInt32(channel.Id), string.Empty);
                    if (subscribedChannel != null && subscribedChannel.Id > 0)
                    {
                        await _navigationService.NavigateAsync("ChannelDetailViewer", param, false, false);
                    }
                    else
                    {
                        if (channel.IsPrivate)
                        {
                            await _navigationService.NavigateAsync("VerifySecureIdPage", param, false, false);
                        }
                        else
                        {
                            await _navigationService.NavigateAsync("ChannelSubscribePage", param, false, false);
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        DelegateCommand _subscribeCommand;
        public DelegateCommand SubscribeCommand => _subscribeCommand != null ?
            _subscribeCommand : (_subscribeCommand = new DelegateCommand(SubscribeInstituteAsync));

        private async void SubscribeInstituteAsync()
        {
            var instituteDetail = await _instituteService.GetInstituteDetailAsync(Institute.Id);
            if (instituteDetail.IsOwner)
                return;

            await _commonHelper.ShowLoaderAsync();

            var subscribedInstitute = await _instituteService.GetInstituteDetailBySubscribedAsync(Institute.Id);
            if (subscribedInstitute != null && subscribedInstitute.Id > 0)
            {
                await _commonHelper.HideLoaderAsync();
                _commonHelper.DisplayAlertMessage("", "You already have subscribed this institute.");
                return;
            }

            var isInstituteSubscribed = await _instituteService.SubscribeInstituteAsync(Institute.Id);
            if (isInstituteSubscribed)
            {
                var isDefault = true;
                var channel = await _channelService.GetChannelByInstituteByDefault(Institute.Id, isDefault);

                if (channel != null && channel.Count > 0)
                {
                    var isSubscribed = await _channelService.SubscribeChannelAsync(channel[0].Id);
                    if (isSubscribed)
                    {
                        _commonHelper.SubscribeChannelForNotification(channel[0].Id.ToString());

                        var institute = await _instituteService.GetInstituteDetailAsync(Institute.Id);
                        this.InstituteDetail(institute);
                    }
                }
            }

            await _commonHelper.HideLoaderAsync();

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


        DelegateCommand _loadLargeImage;
        public DelegateCommand LoadLargeImage => _loadLargeImage != null ?
            _loadLargeImage : (_loadLargeImage = new DelegateCommand(LoadLargeImageAsync));

        private void LoadLargeImageAsync()
        {

        }

        DelegateCommand<string> _subscribeChannelCommand;
        public DelegateCommand<string> SubscribeChannelCommand => _subscribeChannelCommand != null ?
            _subscribeChannelCommand : (_subscribeChannelCommand = new DelegateCommand<string>(SubscribeChannelAsync));

        private async void SubscribeChannelAsync(string channelId)
        {
            var channel = await _channelService.GetChannelDetailAsync(Convert.ToInt32(channelId), string.Empty);

            if (channel != null)
            {
                var param = new NavigationParameters();
                param.Add("channelId", channel.Id);
                param.Add("cStatus", channel.Status);

                InstituteSession.Current.SetSelectedChannel(Convert.ToInt32(channel.Id));

                if (channel.OwnerFlag)
                {
                    await _navigationService.NavigateAsync("ChannelDetailViewer", param, false, false);
                }
                else
                {
                    var subscribedChannel = await _channelService.GetChannelDetailBySubscribedAsync(Convert.ToInt32(channelId), string.Empty);
                    if (subscribedChannel != null && subscribedChannel.Id > 0)
                    {
                        _commonHelper.DisplayAlertMessage("", "You already have subscribed this channel");
                        return;
                    }
                    await _navigationService.NavigateAsync("ChannelSubscribePage", param, false, false);
                }

            }
        }

        public virtual bool? OnBackButtonPressed()
        {
            //false is default value when system call back press
            return false;
        }

        public virtual void OnSoftBackButtonPressed()
        {
            _navigationService.NavigateAsync("InstituteTabbedPage/InstituteListPage", null, false, false);
        }
    }
}
