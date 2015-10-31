using SignalRChatApi.HubExtensions;
using SignalRChatApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SignalRChatApi.Repositories
{
    public class ChatContentRepository : RepositoryBase<ChatContent>, IChatContentRepository
    {
        readonly string _connString = ConfigurationManager.ConnectionStrings["ChatConnection"].ConnectionString;
        public ChatContentRepository()
            : base()
        {

        }

        public IEnumerable<ChatContent> GetAllChats()
        {
            var chats = new List<ChatContent>();

            string commandText = null;
            var query = ActiveContext.ChatContents as DbQuery<ChatContent>;
            commandText = query.ToString();

            using (var connection = new SqlConnection(_connString))
            {
                connection.Open();
                using (var command = new SqlCommand(commandText, connection))
                {
                    command.Notification = null;

                    var dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        chats.Add(item: new ChatContent { Id = (int)reader["ID"], SendBy = (int)reader["SendBy"], SendTo = (int)reader["SendTo"], MessageContent = (string)reader["MessageContent"], SendOn = Convert.ToDateTime(reader["SendOn"]) });
                    }
                }
            }
            return chats;
        }

        public IEnumerable<ChatContent> GetAllChatsInSession(int sessionId)
        {
            var chats = new List<ChatContent>();

            string commandText = null;
            var parameters = new SqlParameter[0];

            //var query = ActiveContext.ChatSessions.Where(c => c.Id.Equals(sessionId)).Join(ActiveContext.ChatContents,
            //    session => session.Id, content => content.SessionId, (session, content) =>
            //        new { session.Id, session.Topic, content.MessageContent, content.SendBy, content.SendOn, content.SendTo })
            //         as DbQuery<ChatSessionContent>;

            //var query = ActiveContext.ChatContents.Where(c=>c.SessionId.Equals(sessionId)) as DbQuery<ChatContent>;
            //commandText = query.ToString();
            commandText = "Select content.Id, content.SessionId, session.Topic, content.MessageContent, content.SendBy, content.SendOn, content.SendTo "
                + "from ChatSession session left join ChatContent content on session.Id = content.SessionId "
                + "where session.Id = " + sessionId;
            //var internalQuery = query.GetType()
            //.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
            //.Where(field => field.Name == "_internalQuery")
            //.Select(field => field.GetValue(query))
            //.First();

            //var objectQuery = internalQuery.GetType()
            //    .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
            //    .Where(field => field.Name == "_objectQuery")
            //    .Select(field => field.GetValue(internalQuery))
            //    .Cast<ObjectQuery<ChatSessionContent>>()
            //    .First();

            //parameters = objectQuery.Parameters.Select(x => new SqlParameter(x.Name, x.Value)).ToArray();

            using (var connection = new SqlConnection(_connString))
            {
                connection.Open();
                using (var command = new SqlCommand(commandText, connection))
                {
                    command.Notification = null;
                    //command.Parameters.AddRange(parameters);

                    var dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        chats.Add(item: new ChatContent { Id = (int)reader["ID"], SessionId = (int)reader["SessionId"], SendBy = (int)reader["SendBy"], SendTo = (int)reader["SendTo"], MessageContent = (string)reader["MessageContent"], SendOn = Convert.ToDateTime(reader["SendOn"])});
                    }
                }
            }
            return chats;
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                ChatNotificationClient NotifyClient = ChatNotificationClient.GetInstance();
                NotifyClient.Start();
                NotifyClient.PublishMessage("SendMessages", "Test");
            }
        }
    }
}