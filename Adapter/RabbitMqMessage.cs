namespace Adapter
{
    public class RabbitMqMessage
    {
        public string Content { get; set; }
        public string QueueName { get; set; }

        public RabbitMqMessage(string content, string queueName)
        {
            Content = content;
            QueueName = queueName;

        }
    }
}