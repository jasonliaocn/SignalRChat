using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace SignalRChatApi.HubExtensions
{
    public class ChatNotificationClient
    {
        private static HubConnection connection;
        private static IHubProxy chathubProxy;
        private static ChatNotificationClient client;
        private static object _connectlock = new object();
        private static object _clientlock = new object();

        readonly string _hubUrlString = ConfigurationManager.AppSettings["ChatHub"].ToString();

        private ChatNotificationClient()
        {

        }

        public static ChatNotificationClient GetInstance()
        {
            if (client == null)
            {
                lock (_clientlock)
                {
                    if (client == null)
                    {
                        client = new ChatNotificationClient();
                    }
                }
            }
            return client;
        }

        public void Start()
        {
            if (connection == null)
            {
                connection = new HubConnection(_hubUrlString);
                chathubProxy = connection.CreateHubProxy("ChatHub");
                connection.Start().ContinueWith(
                    task =>
                    {
                        if (task.IsFaulted)
                        {
                            Trace.WriteLine(string.Format("There was an error opening the connection: {0}", task.Exception.GetBaseException()));
                        }
                        else
                        {
                            Trace.WriteLine("Connected.");
                        }
                    }).Wait();
            }
        }

        public void PublishMessage(string method, params object[] args)
        {
            chathubProxy.Invoke(method, args).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Trace.WriteLine(string.Format("There was an error calling Send: {0}", task.Exception.GetBaseException()));
                }
                else
                {
                    Trace.WriteLine("Send complete.");
                }

            });
        }
    }
}