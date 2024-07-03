using System;

namespace DevCoffeeManagerApp.ViewModels
{
    public class EventAggregator
    {
        private static EventAggregator _instance;
        public static EventAggregator Instance => _instance ?? (_instance = new EventAggregator());

        public event EventHandler<MessageEventArgs> MessagePublished;

        public void PublishMessage(string message)
        {
            MessagePublished?.Invoke(this, new MessageEventArgs(message));
        }
    }
    public class MessageEventArgs
    {
        public string Message { get; }

        public MessageEventArgs(string message)
        {
            Message = message;
        }
    }
}
