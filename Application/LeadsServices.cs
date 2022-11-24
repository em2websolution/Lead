

using Domain.Adapters;
using Domain.Models;
using Domain.Services;

namespace Application
{
    public class LeadsServices : ILeadsServices
    {

        private ILeadsAdapter _leadsAdapter;

        public LeadsServices(ILeadsAdapter leadsAdapter)
        {
            _leadsAdapter = leadsAdapter;
        }

        public void Delete(Guid id)
        {
            _leadsAdapter.Delete(id);
        }

        public Leads Get(Guid id)
        {
           return _leadsAdapter.Get(id);
        }

        public List<Leads> GetAll()
        {
            return _leadsAdapter.GetAll();
        }

        public Guid Insert(Leads leads)
        {
            if (IsValid(leads))
                throw new ArgumentNullException(nameof(leads));

            return _leadsAdapter.Insert(leads);
        }

        private static bool IsValid(Leads leads)
        {
            return leads == null || string.IsNullOrEmpty(leads.FirsName) || string.IsNullOrEmpty(leads.LastName) || string.IsNullOrEmpty(leads.Email);
        }

        public void Update(Leads leads)
        {
            if (leads == null)
                throw new NotImplementedException();

            _leadsAdapter.Update(leads);
        }
    }
}