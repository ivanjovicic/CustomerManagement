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
        public List<Customer> GetAll()
        {
            var customers = new List<Customer>();

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(
                "SELECT Id, FirstName, LastName, Email, IsActive FROM Customers", conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(new Customer
                        {
                            Id = (int)reader["Id"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                            IsActive = (bool)reader["IsActive"]
                        });
                    }
                }
            }

            return customers;
        }

        public Customer GetById(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(
                @"SELECT Id, FirstName, LastName, Email, IsActive
                  FROM Customers WHERE Id = @Id", conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (!reader.Read()) return null;

                    return new Customer
                    {
                        Id = (int)reader["Id"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Email = reader["Email"].ToString(),
                        IsActive = (bool)reader["IsActive"]
                    };
                }
            }
        }

        public void Add(Customer customer)
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

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Customer customer)
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

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(
                "DELETE FROM Customers WHERE Id = @Id", conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
