using CustomerManagement.Data;
using CustomerManagement.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Tests.Data
{
    [TestClass]
    public class CustomerRepositoryTests
    {
        private CustomerRepository _repository;

        [TestInitialize]
        public void Setup()
        {
            var cs = ConfigurationManager
                .ConnectionStrings["TestConnection"]
                .ConnectionString;

            _repository = new CustomerRepository(cs);
        }

        [TestMethod]
        public void Add_Should_Insert_Customer()
        {
            var customer = new Customer
            {
                FirstName = "Test",
                LastName = "User",
                Email = "test.user@test.com",
                IsActive = true
            };

            _repository.Add(customer);

            var customers = _repository.GetAll();
            Assert.IsTrue(customers.Exists(c => c.Email == "test.user@test.com"));
        }
    }
}
