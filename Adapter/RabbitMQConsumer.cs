using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Adapter
{
    public class RabbitMQConsumer
    {
        private readonly string _hostName;

        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _queueName;

        public RabbitMQConsumer(string hostName, string queueName)
        {
            _hostName = hostName;
            var factory = new ConnectionFactory() { HostName = _hostName };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _queueName = queueName;
        }

        public void Consume()
        {
            _channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: true, arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var message = System.Text.Encoding.UTF8.GetString(ea.Body.ToArray());
                Console.WriteLine("Received message: " + message);
            };

            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
        }
    }
}
