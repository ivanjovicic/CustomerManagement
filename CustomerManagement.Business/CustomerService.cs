using CustomerManagement.Data;
using CustomerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<Customer>> GetCustomersAsync(string searchTerm, bool? isActive)
        {
            try
            {
                var customers = await _repository.GetAllAsync();

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
            catch
            {
                return GetDemoCustomers();
            }
        }

        private List<Customer> GetDemoCustomers()
        {
            return new List<Customer>
            {
                new Customer { Id = 1, FirstName = "Demo", LastName = "User", Email = "demo1@test.com", IsActive = true },
                new Customer { Id = 2, FirstName = "Sample", LastName = "Customer", Email = "demo2@test.com", IsActive = false }
            };
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            try
            {
                return await _repository.GetByIdAsync(id);
            }
            catch
            {
                // Fallback to demo data when DB is not reachable
                return GetDemoCustomers().FirstOrDefault(c => c.Id == id);
            }
        }

        public async Task AddAsync(Customer customer)
        {
            if (customer == null) throw new ArgumentNullException(nameof(customer));

            if (string.IsNullOrWhiteSpace(customer.Email))
                throw new ArgumentException("Email is required.", nameof(customer));

            var allCustomers = await _repository.GetAllAsync();
            bool emailExists = allCustomers
                .Any(c => c.Email.Equals(customer.Email, StringComparison.OrdinalIgnoreCase));

            if (emailExists)
            {
                throw new InvalidOperationException("A customer with the same email address already exists.");
            }
            await _repository.AddAsync(customer);
        }

        public async Task UpdateAsync(Customer customer)
        {
            await _repository.UpdateAsync(customer);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
