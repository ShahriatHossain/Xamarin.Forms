using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pigeon.Services.Model;
using Pigeon.ViewModels;
using Xamarin.Forms;

namespace Pigeon.Xaml.DataTemplateSelector
{
    public class MessageDataTemplateSelector : Xamarin.Forms.DataTemplateSelector
    {
        public DataTemplate TextMessageTemplate { get; set; }
        public DataTemplate ImageMessageTemplate { get; set; }
        public DataTemplate PDFMessageTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var message = ((MessageViewModel)item).Message;
            if (message is TextMessage)
            {
                return TextMessageTemplate;
            }
            else if (message is PDFMessage)
            {
                return PDFMessageTemplate;
            }
            return ImageMessageTemplate;
        }
    }
}
