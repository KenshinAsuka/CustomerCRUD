using CustomerCRUD.Models;
using CustomerCRUD.DTO;

namespace CustomerCRUD.Repositories
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAll();

        Customer Get(Guid id);

        Guid Insert(CreateCustomerModel customer);

        void Update(Guid id, UpdateCustomerModel customer);

        void Delete(Guid id);

        void Save();
    }
}
