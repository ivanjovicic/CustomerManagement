using CustomerManagement.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public CustomerRepository()
            : this(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString)
        {
        }
        public async Task<List<Customer>> GetAllAsync()
        {
            var customers = new List<Customer>();

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(
                "SELECT Id, FirstName, LastName, Email, IsActive FROM Customers", conn))
            {
                await conn.OpenAsync().ConfigureAwait(false);

                using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                {
                    while (await reader.ReadAsync().ConfigureAwait(false))
                    {
                        customers.Add(new Customer
                        {
                            Id = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Email = reader.GetString(3),
                            IsActive = reader.GetBoolean(4)
                        });
                    }
                }
            }

            return customers;
        }


        public async Task<Customer> GetByIdAsync(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(
                @"SELECT Id, FirstName, LastName, Email, IsActive
          FROM Customers WHERE Id = @Id", conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                await conn.OpenAsync().ConfigureAwait(false);

                using (var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                {
                    if (!await reader.ReadAsync().ConfigureAwait(false))
                        return null;

                    return new Customer
                    {
                        Id = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Email = reader.GetString(3),
                        IsActive = reader.GetBoolean(4)
                    };
                }
            }
        }


        public async Task AddAsync(Customer customer)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(
                @"INSERT INTO Customers (FirstName, LastName, Email, IsActive)
          VALUES (@FirstName, @LastName, @Email, @IsActive)", conn))
            {
                cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@IsActive", customer.IsActive);

                await conn.OpenAsync().ConfigureAwait(false);
                await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }

        public async Task UpdateAsync(Customer customer)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(
                @"UPDATE Customers
                  SET FirstName = @FirstName,
                      LastName = @LastName,
                      Email = @Email,
                      IsActive = @IsActive
                  WHERE Id = @Id", conn))
            {
                cmd.Parameters.AddWithValue("@Id", customer.Id);
                cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@IsActive", customer.IsActive);

                await conn.OpenAsync().ConfigureAwait(false);
                await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(
                "DELETE FROM Customers WHERE Id = @Id", conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                await conn.OpenAsync().ConfigureAwait(false);
                await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }
    }
}
