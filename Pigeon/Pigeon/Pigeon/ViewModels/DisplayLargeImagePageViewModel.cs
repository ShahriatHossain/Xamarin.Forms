using Pigeon.Helpers;
using Pigeon.Services.Interface;
using Prism.Mvvm;
using Prism.Navigation;
using System;

namespace Pigeon.ViewModels
{
    public class DisplayLargeImagePageViewModel : BindableBase, INavigationAware
    {
        private ImageResizeHelper _imageResizeHelper;
        private readonly IMessageService _messageService;
        private CommonHelper _commonHelper;
        public DisplayLargeImagePageViewModel(IMessageService messageService)
        {
            _imageResizeHelper = new ImageResizeHelper();
            _messageService = messageService;
            _commonHelper = new CommonHelper();
        }

        private string _imageUrl;
        public string ImageUrl
        {
            get { return _imageUrl; }
            set { SetProperty(ref _imageUrl, value); }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            var noticeId = Convert.ToInt32(parameters["noticeId"]);
            SetDefaultField(noticeId);
        }

        private async void SetDefaultField(int noticeId)
        {
            await _commonHelper.ShowLoaderAsync();
            var notices = await _messageService.GetNoticeDetail(noticeId);
            foreach (var notice in notices)
            {
                this.ImageUrl = notice.MediaBasedUrl + "/" + notice.MediaName;
            }
            await _commonHelper.HideLoaderAsync();
        }
    }
}
