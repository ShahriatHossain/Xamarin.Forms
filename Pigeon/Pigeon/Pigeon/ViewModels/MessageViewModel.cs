using Pigeon.LocalDataAccess.Implementation;
using Pigeon.LocalDataAccess.Model;
using Pigeon.Services.Configuration;
using Pigeon.Services.Interface;
using Pigeon.Services.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;

namespace Pigeon.ViewModels
{
    public class MessageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        private readonly IMessageService _messageService;
        private readonly IChannelService _channelService;
        public DelegateCommand UpVoteCommand { get; set; }
        public DelegateCommand DownVoteCommand { get; set; }
        public DelegateCommand ViewVoteResult { get; set; }
        public DelegateCommand LoadLargeImage { get; set; }
        public DelegateCommand LoadPDF { get; set; }

        private int? _channelId;
        public int? ChannelId
        {
            get { return _channelId; }
            set { SetProperty(ref _channelId, value); }
        }


        public MessageViewModel(Message message, INavigationService navigationService,
            IMessageService messageService, int? channelId, IChannelService channelService)
        {
            _navigationService = navigationService;
            _messageService = messageService;
            _channelService = channelService;
            this.Message = message;
            this.ChannelId = channelId;
            this.UpVoteCommand = new DelegateCommand(UpVote);
            this.DownVoteCommand = new DelegateCommand(DownVote);
            this.ViewVoteResult = new DelegateCommand(NavigateVoteResult);
            this.LoadLargeImage = new DelegateCommand(ShowLargeImage);
            this.LoadPDF = new DelegateCommand(ShowPDF);
            this.SetCanVoteAsync();
        }
        public Message Message { get; private set; }

        private async void UpVote()
        {
            if (this.CanVote)
            {
                await this._messageService.SaveVote(this.Message.Id, "up");
                this.Message.IsVoteCasted = true;
                this.Message.VotingStatus = "vote casted";
                await this.UpdateVoteCastAsync();
                this.SetCanVoteAsync();
            }
        }
        private async void DownVote()
        {
            if (this.CanVote)
            {
                await this._messageService.SaveVote(this.Message.Id, "down");
                this.Message.IsVoteCasted = true;
                this.Message.VotingStatus = "vote casted";
                await this.UpdateVoteCastAsync();
                this.SetCanVoteAsync();
            }
        }

        private async System.Threading.Tasks.Task UpdateVoteCastAsync()
        {
            await PigeonNoticeHelper.UpdateVoteCastAsync(this.Message.Id, LocalStorageSettings.Token);
        }
        private void NavigateVoteResult()
        {
            InstituteSession.Current.SetSelectedChannel(Convert.ToInt32(this.ChannelId));

            var param = new NavigationParameters();
            param.Add("message", this.Message);
            param.Add("channelId", ChannelId);
            _navigationService.NavigateAsync("MessageVoteResultPage", param, false, false);
        }
        private async void ShowLargeImage()
        {
            InstituteSession.Current.SetSelectedChannel(Convert.ToInt32(this.ChannelId));

            var param = new NavigationParameters();
            param.Add("noticeId", ((ImageMessage)this.Message).Id);
            await _navigationService.NavigateAsync("DisplayLargeImagePage", param, useModalNavigation: false, animated: false);
        }
        private async void ShowPDF()
        {
            InstituteSession.Current.SetSelectedChannel(Convert.ToInt32(this.ChannelId));

            var param = new NavigationParameters();
            param.Add("noticeId", ((PDFMessage)this.Message).Id);
            await _navigationService.NavigateAsync("PDFViewerContentPage", param, useModalNavigation: false, animated: false);
        }
        private async void SetCanVoteAsync()
        {
            var notice = await PigeonNoticeHelper.GetNoticeDetailAsync(this.Message.Id);

            var channel = await _channelService.GetChannelDetailAsync(ChannelId, string.Empty);

            bool res = DateTime.Now > this.Message.VotingExpireDate;
            var canVote = this.Message.EnableVoting && !res;

            this.CanVote = (canVote && notice.IsVoteCasted == false) ? true : false;
            this.IsVoteOver = res;
            this.IsVoteCasted = (channel.OwnerFlag) ? true : notice.IsVoteCasted;
            this.VotingStatus = notice.VotingStatus;
        }
        private string _votingStatus;
        public string VotingStatus
        {
            get { return _votingStatus; }
            set { SetProperty(ref _votingStatus, value); }
        }

        private bool _isVoteCasted;
        public bool IsVoteCasted
        {
            get { return _isVoteCasted; }
            set { SetProperty(ref _isVoteCasted, value); }
        }

        private bool _isVoteOver;
        public bool IsVoteOver
        {
            get { return _isVoteOver; }
            set { SetProperty(ref _isVoteOver, value); }
        }

        private bool _canVote;
        public bool CanVote
        {
            get { return _canVote; }
            set { SetProperty(ref _canVote, value); }
        }
    }
}
