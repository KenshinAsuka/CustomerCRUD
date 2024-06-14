using Microsoft.EntityFrameworkCore;
using CustomerCRUD.Models;
using CustomerCRUD.DTO;
using CustomerCRUD.DataContext;

namespace CustomerCRUD.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MainContext _mainContext;
        public CustomerRepository(MainContext mainContext)
        {
            _mainContext = mainContext;
        }

        public void Delete(Guid id)
        {
            var customers = _mainContext.Customers?.Find(id);

            if (customers != null)
            {
                _mainContext.Customers?.Remove(customers);
                Save();
            }
        }

        public Customer Get(Guid id)
        {
            var customer = _mainContext.Customers?.Find(id);

            if (customer != null)
                return customer;
            else
                return new Customer();
        }

        public IEnumerable<Customer> GetAll()
        {
            var list = _mainContext.Customers?.ToList();

            if (list != null)
                return list;
            else
                return new List<Customer>();
        }

        public Guid Insert(CreateCustomerModel customer)
        {
            var newCustomer = new Customer();

            newCustomer.Id = Guid.NewGuid();
            newCustomer.Name = customer.Name;
            newCustomer.Email = customer.Email;
            newCustomer.Phone = customer.Phone;
            newCustomer.Address = customer.Address;
            newCustomer.DateOfBirth = customer.DateOfBirth.Value;

            _mainContext.Customers?.Add(newCustomer);
            Save();

            return newCustomer.Id;
        }

        public void Update(Guid Id, UpdateCustomerModel customer)
        {
            Customer updatedCustomer = new Customer();
            updatedCustomer.Id = Id;
            updatedCustomer.Name = customer.Name;
            updatedCustomer.Email = customer.Email;
            updatedCustomer.Phone = customer.Phone;
            updatedCustomer.Address = customer.Address;
            updatedCustomer.DateOfBirth = customer.DateOfBirth.Value;
            _mainContext.Entry(updatedCustomer).State = EntityState.Modified;
            Save();
        }

        public void Save()
        {
            _mainContext.SaveChanges();
        }
    }
}
