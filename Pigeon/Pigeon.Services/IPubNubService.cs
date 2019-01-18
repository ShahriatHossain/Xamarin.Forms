using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pigeon.Services.Model;

namespace Pigeon.Services
{
    public interface IPubNubService
    {
        void Subscribe(string channel, Action<Message> subscribeCallback, Action<string> errorCallback);
        void Unsubscribe(string channel);
        void QueueUserWorkItem(Action action);
    }
}
