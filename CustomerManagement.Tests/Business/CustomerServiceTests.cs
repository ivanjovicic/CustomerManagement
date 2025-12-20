using CustomerManagement.Business;
using CustomerManagement.Data;
using CustomerManagement.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Tests.Business
{
    [TestClass]
    public class CustomerServiceTests
    {
        [TestMethod]
        public async Task GetCustomers_Filters_By_SearchTerm()
        {
            // Arrange
            var mockRepo = new Mock<ICustomerRepository>();

            mockRepo.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<Customer>
                {
            new Customer { FirstName = "Ivan", LastName = "Ivanovic", Email = "ivan@test.com", IsActive = true },
            new Customer { FirstName = "Marko", LastName = "Markovic", Email = "marko@test.com", IsActive = false }
                });

            var service = new CustomerService(mockRepo.Object);

            // Act
            var result = await service.GetCustomersAsync("Ivan", null);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Ivan", result[0].FirstName);
        }


    }
}
