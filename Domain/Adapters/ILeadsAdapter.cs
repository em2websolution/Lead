using Domain.Models;

namespace Domain.Adapters
{
    public interface ILeadsAdapter
    {
        Guid Insert(Leads leads);
        void Update(Leads leads);
        void Delete(Guid id);
        Leads Get(Guid id);
        List<Leads> GetAll();
    }
}
