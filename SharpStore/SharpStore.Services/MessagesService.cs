using SharpStore.Data;
using SharpStore.Data.Models;
using SimpleHttpServer.Utilities;
using System.Collections;
using System.Collections.Generic;

namespace SharpStore.Services
{
    public class MessagesService
    {
        private SharpStoreContext context;

        public MessagesService()
        {
            this.context = Data.Data.Context;
        }

        public void AddMessageFromFormData(string content)
        {
            IDictionary<string, string> formDataVariables = QueryStringParser.Parse(content);
            Message message = new Message()
            {
                MessageText = formDataVariables["message"],
                Sender = formDataVariables["email"],
                Subject = formDataVariables["subject"]
            };

            this.context.Messages.Add(message);
            this.context.SaveChanges();
        }
    }
}
