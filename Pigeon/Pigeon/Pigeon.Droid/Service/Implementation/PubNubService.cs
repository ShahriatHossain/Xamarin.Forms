using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Pigeon.Services;
//using PubNubMessaging.Core;
using Pigeon.Services.Model;
//using Newtonsoft.Json.Linq;
using Pigeon.Droid.Service.Implementation;
using System.Threading;

[assembly: Xamarin.Forms.Dependency(typeof(PubNubService))]
namespace Pigeon.Droid.Service.Implementation
{
    public class PubNubService : IPubNubService
    {
        //private Pubnub pubnub = new Pubnub("pub-c-78ca4d63-7d67-4411-8088-6328bd469f8a", "sub-c-fb478db8-727b-11e6-80e7-02ee2ddab7fe");
        Action<Services.Model.Message> _subscribeCallback;

        public void Subscribe(string channel, Action<Services.Model.Message> subscribeCallback, Action<string> errorCallback)
        {
            _subscribeCallback = subscribeCallback;
            //ThreadPool.QueueUserWorkItem(o => pubnub.Subscribe<string>(channel, PubNubReceiveMessage, PubNubReceiveStatus, PubNubError));
        }

        void PubNubReceiveStatus(string connectMessage)
        {

        }

        void PubNubReceiveMessage(string result)
        {
            //if (!string.IsNullOrEmpty(result) && !string.IsNullOrEmpty(result.Trim()))
            //{
            //    List<object> deserializedMessage = pubnub.JsonPluggableLibrary.DeserializeToListOfObject(result);
            //    if (deserializedMessage != null && deserializedMessage.Count > 0)
            //    {
            //        var subscribedObject = (JObject)deserializedMessage[0];
            //        if (subscribedObject != null)
            //        {

            //            //string resultActualMessage = pubnub.JsonPluggableLibrary.SerializeToJsonString(subscribedObject);
            //            //this.AddMessage(subscribedObject.ToObject<TextMessage>());
            //            _subscribeCallback(subscribedObject.ToObject<TextMessage>());
            //        }
            //    }
            //}
        }

        //void PubNubError(PubnubClientError pubnubError)
        //{

        //}

        public void Unsubscribe(string channel)
        {
           // ThreadPool.QueueUserWorkItem(o => pubnub.Unsubscribe<string>(channel, PubNubReceiveMessage, PubNubReceiveStatus, x => { }, PubNubError));
            
        }

        public void QueueUserWorkItem(Action action)
        {
            ThreadPool.QueueUserWorkItem(o => action());
        }
    }
}