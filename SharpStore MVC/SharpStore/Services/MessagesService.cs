using SharpStore.BindingModels;
using SharpStore.Data;
using SharpStore.Models;

namespace SharpStore.Services
{
    public class MessagesService
    {
        private SharpStoreContext context;

        public MessagesService(SharpStoreContext context)
        {
            this.context = context;
        }

        public void AddMessageFrombind(MessageBinding messageBindingModel)
        {
            Message message = new Message()
            {
                Email = messageBindingModel.Email,
                MessageText = messageBindingModel.MessageText,
                Subject = messageBindingModel.Subject
            };

            this.context.Messages.Add(message);
            context.SaveChanges();
        }
    }
}
