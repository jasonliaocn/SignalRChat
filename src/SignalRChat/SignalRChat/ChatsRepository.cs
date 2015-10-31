using SignalRChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChat
{
    public class ChatsRepository
    {
        readonly string _connString = ConfigurationManager.ConnectionStrings["ChatConnection"].ConnectionString;

        public IEnumerable<Chats> GetAllChats()
        {
            var chats = new List<Chats>();
            using (var connection = new SqlConnection(_connString))
            {
                connection.Open();
                using (var command = new SqlCommand(@"SELECT [Id], [Name], [Date], [MESSAGE] FROM [dbo].[Chats]", connection))
                {
                    command.Notification = null;

                    var dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        chats.Add(item: new Chats { Id = (int)reader["ID"], Name = (string)reader["Name"], MESSAGE = (string)reader["EmptyMessage"], Date = Convert.ToDateTime(reader["Date"]) });
                    }
                }

            }
            return chats;
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                ChatHub.SendMessages();
            }
        }
    }
}