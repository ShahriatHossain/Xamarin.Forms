using Pigeon.Services.Interface;
using Prism.Mvvm;
using Prism.Navigation;
using System;

namespace Pigeon.ViewModels
{
    public class PDFViewerContentPageViewModel : BindableBase, INavigationAware
    {
        private string _pdfUrl;
        public string PDFUrl
        {
            get { return _pdfUrl; }
            set { SetProperty(ref _pdfUrl, value); }
        }

        private readonly IMessageService _messageService;

        public PDFViewerContentPageViewModel(IMessageService messageService)
        {

            _messageService = messageService;
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
            var notices = await _messageService.GetNoticeDetail(noticeId);
            foreach (var notice in notices)
            {
                this.PDFUrl = notice.MediaBasedUrl + "/" + notice.MediaName;
            }
        }

    }
}
