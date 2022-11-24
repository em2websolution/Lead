using Domain.Models;

namespace Domain.Services
{
    public interface ILeadsServices
    {
        Guid Insert(Leads leads);
        void Update(Leads leads);
        void Delete(Guid id);
        Leads Get(Guid id);
        List<Leads> GetAll();
    }
}
