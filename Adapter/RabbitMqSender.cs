using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Adapter
{
    public class RabbitMqSender
    {
        private readonly string _hostName;
        public RabbitMqSender(string hostName)
        {
            _hostName = hostName;
        }

        public void SendMessage(RabbitMqMessage message)
        {
            var factory = new ConnectionFactory() { HostName = _hostName };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: message.QueueName,
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: true,
                                         arguments: null);

                    var body = System.Text.Encoding.UTF8.GetBytes(message.Content);

                    channel.BasicPublish(exchange: "",
                                         routingKey: message.QueueName,
                                         basicProperties: null,
                                         body: body);

                    Console.WriteLine(" [x] Sent {0}", message.Content);
                }
            }
        }
    }
}
