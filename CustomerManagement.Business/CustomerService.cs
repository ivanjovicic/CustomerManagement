using CustomerManagement.Data;
using CustomerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Business
{
    public class CustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public CustomerService()
          : this(new CustomerRepository())
        {
        }

        public List<Customer> GetCustomers(string searchTerm, bool? isActive)
        {
            var customers = _repository.GetAll();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                customers = customers
                     .Where(c =>
                       c.FirstName.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
                       c.LastName.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
                       c.Email.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();
            }

            if (isActive.HasValue)
            {
                customers = customers
                    .Where(c => c.IsActive == isActive.Value)
                    .ToList();
            }

            return customers;
        }

        public Customer GetById(int id) => _repository.GetById(id);

        public void Add(Customer customer)
        {
            _repository.Add(customer);
        }

        public void Update(Customer customer)
        {
            _repository.Update(customer);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
