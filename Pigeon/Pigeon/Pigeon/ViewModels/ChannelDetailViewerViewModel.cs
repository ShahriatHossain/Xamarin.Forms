using Pigeon.Helpers;
using Pigeon.LocalDataAccess.Implementation;
using Pigeon.LocalDataAccess.Model;
using Pigeon.Services.Interface;
using Pigeon.Services.Model;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Pigeon.ViewModels
{
    public class ChannelDetailViewerViewModel : BindableBase, INavigationAware
    {
        #region Declaration
        private readonly IEventAggregator _eventAggregator;
        private readonly INavigationService _navigationService;
        private readonly IMessageService _messageService;
        private bool _isShowDefaultText;
        public bool IsShowDefaultText
        {
            get { return _isShowDefaultText; }
            set { SetProperty(ref _isShowDefaultText, value); }
        }

        public DelegateCommand OnRefresh { get; set; }

        public DelegateCommand AddNewNoticeCommand { get; set; }
        public DelegateCommand UnscribeChannelCommand { get; set; }
        public DelegateCommand GotoParentCommand { get; set; }
        private readonly IChannelService _channelService;
        private ChannelApi _channel;
        public ChannelApi Channel
        {
            get { return _channel; }
            set { SetProperty(ref _channel, value); }
        }

        private int? _channelId;
        public int? ChannelId
        {
            get { return _channelId; }
            set { SetProperty(ref _channelId, value); }
        }

        private string _cStatus;
        public string CStatus
        {
            get { return _cStatus; }
            set { SetProperty(ref _cStatus, value); }
        }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetProperty(ref _isRefreshing, value); }
        }

        private int currentPage = 1;
        private List<LocalDataAccess.Model.ChannelNotice> LocalNotices { get; set; }

        public bool HasNewMessage { get; set; }

        private ObservableCollection<MessageGroup> _messages;
        public ObservableCollection<MessageGroup> MessageGroups
        {
            get { return _messages; }
            set { SetProperty(ref _messages, value); }
        }

        private MessageViewModel _currentMessage;
        public MessageViewModel CurrentMessage
        {
            get { return _currentMessage; }
            set { SetProperty(ref _currentMessage, value); }
        }

        private bool _isOwner;
        public bool IsOwner
        {
            get { return _isOwner; }
            set { SetProperty(ref _isOwner, value); }
        }

        private ImageResizeHelper _imageResizeHelper;
        private CommonHelper _commonHelper;

        #endregion
        public ChannelDetailViewerViewModel() { }
        public ChannelDetailViewerViewModel(IEventAggregator eventAggregator, INavigationService navigationService,
            IMessageService messageService, IChannelService channelService)
        {
            _eventAggregator = eventAggregator;
            _navigationService = navigationService;
            _messageService = messageService;
            _channelService = channelService;
            _imageResizeHelper = new ImageResizeHelper();
            AddNewNoticeCommand = new DelegateCommand(AddNewNotice);
            UnscribeChannelCommand = new DelegateCommand(UnscribeChannel);
            OnRefresh = new DelegateCommand(FetchMoreMessagesAsync);
            GotoParentCommand = new DelegateCommand(GotoParent);
            _commonHelper = new CommonHelper();
        }

        private async void GotoParent()
        {
            await _navigationService.NavigateAsync("InstituteTabbedPage", null, false, false);
        }

        private async void InitiateChannel(string cStatus, int? channelId)
        {
            await _commonHelper.ShowLoaderAsync();
            this.LocalNotices = new List<LocalDataAccess.Model.ChannelNotice>();
            this.currentPage = 1;
            ChannelApi channel = await _channelService.GetChannelDetailAsync(channelId, cStatus);

            this.IsOwner = false;
            if (channel.OwnerFlag)
            {
                this.IsOwner = true;
            }

            this.MessageGroups = null;
            channel.Id = channelId;
            channel.Status = cStatus;
            this.Channel = channel;
            await UpdateNoticesInLocalStorage();
            await PopulateNoticesByPageAsync();
            if (!this.LocalNotices.Any())
            {
                IsShowDefaultText = true;
            }
            await _commonHelper.HideLoaderAsync();
        }

        public async void FetchMoreMessagesAsync()
        {
            this.IsRefreshing = true;
            await this.PopulateNoticesByPageAsync();
            this.IsRefreshing = false;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            try
            {
                if (parameters.Count > 0)
                {
                    if (parameters.ContainsKey("hasNewNotice") && Convert.ToBoolean(parameters["hasNewNotice"]))
                    {
                        this.NewNoticesArrive();
                        return;
                    }

                    CStatus = (string)parameters["cStatus"];
                    ChannelId = (int?)parameters["channelId"];
                    InitiateChannel(CStatus, ChannelId);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async Task UpdateNoticesInLocalStorage()
        {
            var userId = LocalStorageSettings.Token;//repalce with real user Id 

            var maxNoticeId = await PigeonNoticeHelper.GetLatestNoticeIdAsync(userId, this.Channel.Id.Value);
            if (!maxNoticeId.HasValue)
            {
                maxNoticeId = 0;
            }

            var remoteMessages = await _messageService.Get(this.Channel.Id.Value, maxNoticeId);
            if (remoteMessages != null && remoteMessages.Any())
            {
                var newNotices = remoteMessages.Select(x => new LocalDataAccess.Model.ChannelNotice()
                {
                    NoticeId = x.Id,
                    UserId = userId,
                    ChannelId = this.Channel.Id.Value,
                    Notice = x.Notice,
                    ResponseNeeded = x.ResponseNeeded,
                    FileType = this.ConvertMediaTypeToFileType(x.MediaType),
                    FileDisplayName = x.MediaDisplayName,
                    CreationTime = x.CreationTime,
                    VotingLastDate = x.VotingLastDate,
                    VotingStatus = x.VotingStatus,
                    IsVoteCasted = x.VotingStatus == "vote casted"
                }).ToList();

                await PigeonNoticeHelper.InsertAllAsync(newNotices);
            }

            return;
        }

        private string ConvertMediaTypeToFileType(string mediaType)
        {
            switch (mediaType)
            {
                case ".png":
                case ".jpg":
                case ".jpeg":
                case ".gif":
                    return "image";

                case ".pdf":
                    return "pdf";
            }

            return "text";
        }

        private async Task PopulateNoticesByPageAsync()
        {
            try
            {
                var pageSize = 5;
                var userId = LocalStorageSettings.Token;//repalce with real user Id     
                var localNotices = await PigeonNoticeHelper.GetAsync(userId, this.Channel.Id.Value, currentPage, pageSize);
                if (localNotices.Any())
                {
                    this.currentPage++;
                    PrepareForListView(localNotices);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void PrepareForListView(IList<LocalDataAccess.Model.ChannelNotice> localNotices)
        {
            if (!localNotices.Any())
            {
                return;
            }
            this.LocalNotices.AddRange(localNotices);

            var messages = this.LocalNotices.Select(ConvertToMessage).OfType<Message>().ToList();

            this.AddOrUpdateGroupMessages(messages, localNotices.Max(x => x.NoticeId));
            IsShowDefaultText = false;
        }

        private Message ConvertToMessage(LocalDataAccess.Model.ChannelNotice notice)
        {
            bool isVoteCasted = notice.VotingStatus == "vote casted";
            bool isVoteOver = (notice.VotingStatus == "voting over") && isVoteCasted != true;

            switch (notice.FileType)
            {
                case "pdf":
                    return new PDFMessage()
                    {
                        Id = notice.NoticeId,
                        PDFUrl = notice.Notice,
                        FileDisplayName = notice.FileDisplayName,
                        FileType = "pdf",
                        SendTime = notice.CreationTime,
                        VotingExpireDate = notice.VotingLastDate,
                        EnableVoting = notice.ResponseNeeded,
                        IsVoteCasted = isVoteCasted,
                        VotingStatus = notice.VotingStatus,
                        IsVoteOver = isVoteOver
                    };
                //break;
                case "image":
                    return new ImageMessage()
                    {
                        Id = notice.NoticeId,
                        ImageUrl = notice.MediaBasedUrl + "/" + notice.MediaName,
                        FileDisplayName = notice.FileDisplayName,
                        FileType = "image",
                        SendTime = notice.CreationTime,
                        VotingExpireDate = notice.VotingLastDate,
                        EnableVoting = notice.ResponseNeeded,
                        IsVoteCasted = isVoteCasted,
                        VotingStatus = notice.VotingStatus,
                        IsVoteOver = isVoteOver
                    };
                //break;
                default:
                    return new TextMessage()
                    {
                        Id = notice.NoticeId,
                        Text = notice.Notice,
                        SendTime = notice.CreationTime,
                        FileType = "text",
                        VotingExpireDate = notice.VotingLastDate,
                        EnableVoting = notice.ResponseNeeded,
                        IsVoteCasted = isVoteCasted,
                        VotingStatus = notice.VotingStatus,
                        IsVoteOver = isVoteOver
                    };
                    //break;
            }

        }

        private async void OnNewMessage(Message message)
        {
            if (!this.IsOwner)
            {
                this.NewNoticesArrive();
            }
        }

        private async void NewNoticesArrive()
        {
            await this.UpdateNoticesInLocalStorage();
            var userId = LocalStorageSettings.Token;//repalce with real user Id     
            var latestNoticeId = this.LocalNotices.Any() ? this.LocalNotices.Max(x => x.NoticeId) : 0;
            var localNotices = await PigeonNoticeHelper.GetNewerNoticesAsync(userId, this.Channel.Id.Value, latestNoticeId);
            PrepareForListView(localNotices);
        }

        private IList<MessageGroup> GetSortedGroupMessagesByDate(IList<Message> messages)
        {
            var groupMessages = new List<MessageGroup>();
            var messagesGroupByDate = messages.GroupBy(x => x.SendTime.Date).OrderBy(x => x.Key);
            foreach (var mGroup in messagesGroupByDate)
            {
                var messageGroup = new MessageGroup();
                messageGroup.SendTime = mGroup.Key;
                foreach (var item in mGroup.OrderBy(x => x.SendTime))
                {
                    messageGroup.Add(new MessageViewModel(item, _navigationService, _messageService, ChannelId, _channelService));
                }
                groupMessages.Add(messageGroup);
            }
            return groupMessages;
        }

        private void AddOrUpdateGroupMessages(IList<Message> newMessages, int messageIdToBeScroll)
        {
            this.HasNewMessage = true;
            var messageGroups = GetSortedGroupMessagesByDate(newMessages);
            this.CurrentMessage = messageGroups.SelectMany(x => x).FirstOrDefault(x => x.Message.Id == messageIdToBeScroll);
            if (this.MessageGroups == null)
            {
                this.MessageGroups = new ObservableCollection<MessageGroup>(messageGroups);
            }
            else
            {
                if (messageGroups.Count > 0)
                {
                    this.MessageGroups.Clear();
                    foreach (MessageGroup messageGroup in messageGroups)
                    {
                        this.MessageGroups.Add(messageGroup);
                    }
                }
            }
        }

        public async void AddNewNotice()
        {
            var param = new NavigationParameters();
            param.Add("channel", this.Channel);
            await _navigationService.NavigateAsync("NoticeCreatePage", param, useModalNavigation: false, animated: false);
        }

        private async void UnscribeChannel()
        {
            if (this.IsOwner)
            {
                _commonHelper.DisplayAlertMessage(string.Empty, "You can't unsubscribe your own created channel.");
                return;
            }
            var param = new NavigationParameters();
            param.Add("channelId", this.ChannelId);
            param.Add("cStatus", this.CStatus);
            await _navigationService.NavigateAsync("ChannelUnSubscribePage", param, false, false);
        }


        DelegateCommand _setSecurePinCommand;
        public DelegateCommand SetSecurePinCommand => _setSecurePinCommand != null ?
            _setSecurePinCommand : (_setSecurePinCommand = new DelegateCommand(NavigateToSecureIdPage));

        private async void NavigateToSecureIdPage()
        {
            if (!this.IsOwner)
            {
                _commonHelper.DisplayAlertMessage(string.Empty, "Sorry, You can't access this content.");
                return;
            }

            var param = new NavigationParameters();
            param.Add("channelId", ChannelId);
            param.Add("cStatus", CStatus);
            await _navigationService.NavigateAsync("ChannelSecureIdPage", param, false, false);
        }

        DelegateCommand _SubscriberCommand;
        public DelegateCommand SubscriberCommand => _SubscriberCommand != null ?
            _SubscriberCommand : (_SubscriberCommand = new DelegateCommand(NavigateToSubscriberPage));

        private async void NavigateToSubscriberPage()
        {
            if (!this.IsOwner)
            {
                _commonHelper.DisplayAlertMessage(string.Empty, "Sorry, You can't access this content.");
                return;
            }

            var param = new NavigationParameters();
            param.Add("channelId", ChannelId);
            param.Add("cStatus", CStatus);
            await _navigationService.NavigateAsync("SubscriberListPage", param, false, false);
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

    public class MessageGroup : List<MessageViewModel>
    {
        public DateTime SendTime { get; set; }

    }

}
