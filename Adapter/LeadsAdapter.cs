using Domain.Adapters;
using Domain.Models;

namespace Adapter
{
    public class LeadsAdapter : ILeadsAdapter
    {
        private readonly List<Leads> _database;

        public LeadsAdapter()
        {
            _database = new List<Leads>();
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
            return GetById(id);
        }

        public List<Leads> GetAll()
        {
            return _database;
        }

        public Guid Insert(Leads leads)
        {
            _database.Add(leads);

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