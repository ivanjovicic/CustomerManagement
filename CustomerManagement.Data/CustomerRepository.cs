using CustomerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private static List<Customer> _customers = new List<Customer>
        {
            new Customer { Id = 1, FirstName = "Ivan", LastName = "Jovicic", Email = "ivan@test.com", IsActive = true },
            new Customer { Id = 2, FirstName = "Marko", LastName = "Markovic", Email = "marko@test.com", IsActive = false }
        };

        public List<Customer> GetAll() => _customers;

        public Customer GetById(int id) =>
            _customers.FirstOrDefault(c => c.Id == id);

        public void Add(Customer customer)
        {
            customer.Id = _customers.Max(c => c.Id) + 1;
            _customers.Add(customer);
        }

        public void Update(Customer customer)
        {
            var existing = GetById(customer.Id);
            if (existing == null) return;

            existing.FirstName = customer.FirstName;
            existing.LastName = customer.LastName;
            existing.Email = customer.Email;
            existing.IsActive = customer.IsActive;
        }

        public void Delete(int id)
        {
            var customer = GetById(id);
            if (customer != null)
                _customers.Remove(customer);
        }
    }
}
