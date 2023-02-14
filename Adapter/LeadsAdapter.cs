using Domain.Adapters;
using Domain.Models;
using Microsoft.Extensions.Configuration;

namespace Adapter
{
    public class LeadsAdapter : ILeadsAdapter
    {
        private readonly List<Leads> _database;

        private RabbitMqSender _rabbitMqSender;

        private readonly IConfigurationSection _rabbitMqConfig;

        private string _hostName;

        public LeadsAdapter(IConfiguration configuration)
        {
            _database = new List<Leads>();

            _rabbitMqConfig = configuration.GetSection("RabbitMq");

            if (string.IsNullOrEmpty(_rabbitMqConfig["HostName"]))
                _hostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST");
            else _hostName = _rabbitMqConfig["HostName"];

            _rabbitMqSender = new RabbitMqSender(_hostName);
        }
        public Leads GetById(Guid id)
        {
            return _database.Find(lead => lead.Id == id);
        }

        public void Delete(Guid id)
        {
            var leadId = GetById(id);

            if (leadId == null)
                throw new InvalidOperationException("Lead not found");

            _database.Remove(leadId);
        }

        public Leads Get(Guid id)
        {
            var consumer = new RabbitMQConsumer(_hostName, RabbitMqQueues.ProcessNewLead);
            consumer.Consume();

            return GetById(id);
        }

        public List<Leads> GetAll()
        {
            return _database;
        }

        public Guid Insert(Leads leads)
        {
            _database.Add(leads);

            var rabbitMqMessage = new RabbitMqMessage(leads.ToString(), RabbitMqQueues.ProcessNewLead);
            _rabbitMqSender.SendMessage(rabbitMqMessage);

            return leads.Id;
        }

        public void Update(Leads lead)
        {
            var leadId = GetById(lead.Id);

            if(leadId == null)
               throw new InvalidOperationException("Lead not found");
            
            _database.Remove(leadId);
            _database.Add(lead);
        }
    }
}