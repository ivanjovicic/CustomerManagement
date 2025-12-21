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

        public async Task<Customer> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task AddAsync(Customer customer)
        {
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
