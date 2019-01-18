using Pigeon.Services.Configuration;
using Pigeon.Services.Interface;
using Pigeon.Services.Model;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Pigeon.ViewModels
{
    public class MessageVoteResultPageViewModel : BindableBase, INavigationAware
    {
        Message _message;
        NoticeDetail _noticeDetail;
        private readonly IMessageService _messageService;
        private readonly IChannelService _channelService;
        public MessageVoteResultPageViewModel(IMessageService messageService, IChannelService channelService)
        {
            _messageService = messageService;
            _channelService = channelService;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public Message Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public NoticeDetail NoticeDetail
        {
            get { return _noticeDetail; }
            set { SetProperty(ref _noticeDetail, value); }
        }

        private bool _isText;
        public bool IsText
        {
            get { return _isText; }
            set { SetProperty(ref _isText, value); }
        }

        private bool _isImageUrl;
        public bool IsImageUrl
        {
            get { return _isImageUrl; }
            set
            {
                if (value)
                {
                    LogoSource = ImageSource.FromResource("Pigeon.Images.image_icon.png");
                }

                SetProperty(ref _isImageUrl, value);
            }
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            try
            {
                this.Message = (Message)parameters["message"];
                this.NoticeDetail = await _messageService.GetDetail(this.Message.Id);
                this.SetImageUrl();
                this.InitiateVotingResult();
                var channelId = (int?)parameters["channelId"];
                this.GetChannelDetail(channelId);

                InstituteSession.Current.SetSelectedChannel(Convert.ToInt32(channelId));
            }
            catch (Exception ex)
            {
                //ex
            }

        }

        private bool _isPDF;
        public bool IsPDF
        {
            get { return _isPDF; }
            set
            {
                if (value)
                {
                    LogoSource = ImageSource.FromResource("Pigeon.Images.pdf_icon.png");
                }

                SetProperty(ref _isPDF, value);
            }
        }

        private ImageSource _logoSource;
        public ImageSource LogoSource
        {
            get { return _logoSource; }
            set { SetProperty(ref _logoSource, value); }
        }

        private void SetImageUrl()
        {
            this.SetTypesToFalse();
            if (string.IsNullOrEmpty(this.Message.FileType))
                return;

            switch (this.Message.FileType)
            {
                case "image":
                    this.IsImageUrl = true;
                    break;
                case "pdf":
                    this.IsPDF = true;
                    break;
                default:
                    this.IsText = true;
                    break;
            }
        }

        private void InitiateVotingResult()
        {
            if (this.NoticeDetail != null)
            {
                this.Message.UpVoteCount = NoticeDetail.Yes;
                this.Message.DownVoteCount = NoticeDetail.No;
                this.Votes = new ObservableCollection<KeyValuePair<string, int>>();
                this.Votes.Add(new KeyValuePair<string, int>("Not voted", 0));
                this.Votes.Add(new KeyValuePair<string, int>("Yes", NoticeDetail.Yes));
                this.Votes.Add(new KeyValuePair<string, int>("No", NoticeDetail.No));
            }
        }

        private void SetTypesToFalse()
        {
            this.IsText = false;
            this.IsImageUrl = false;
            this.IsPDF = false;
        }

        private ChannelApi _channel;
        public ChannelApi Channel
        {
            get { return _channel; }
            set { SetProperty(ref _channel, value); }
        }
        private async void GetChannelDetail(int? channelId)
        {
            Channel = await _channelService.GetChannelDetailAsync(channelId, "A");
        }

        private ObservableCollection<KeyValuePair<string, int>> _votes;
        public ObservableCollection<KeyValuePair<string, int>> Votes
        {
            get { return _votes; }
            set { SetProperty(ref _votes, value); }
        }
    }
}
