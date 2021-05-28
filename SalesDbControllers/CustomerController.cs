using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesDbControllers
{
    public class CustomerController 
    {      //establish connection
        private static Connection connection { get; set; }
        public CustomerController(Connection connection)
        {
            CustomerController.connection = connection;
        }
        //consolidating duplicate lines
        private Customer FillCustomerFromReader(SqlDataReader reader)
        {
            var customer = new Customer()
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = Convert.ToString(reader["Name"]),
                City = Convert.ToString(reader["City"]),
                State = Convert.ToString(reader["State"]),
                Sales = Convert.ToDecimal(reader["Sales"]),
                Active = Convert.ToBoolean(reader["Active"])
            };
            return customer;
        }
        private void FillCustomerFromUpdateorChange(SqlCommand sqlcmd, Customer customer)
        {   sqlcmd.Parameters.AddWithValue("@Name", customer.Name);
            sqlcmd.Parameters.AddWithValue("@City", customer.City);
            sqlcmd.Parameters.AddWithValue("@State", customer.State);
            sqlcmd.Parameters.AddWithValue("@Sales", customer.Sales);
            sqlcmd.Parameters.AddWithValue("@Active", customer.Active);
        }
        //Delete records
        public bool Remove(Customer customer)
        {
            var sql = $"Delete From Customerss " +
                  "Where Id = @Id;";
            var sqlcmd = new SqlCommand(sql, connection.SqlConn);
            sqlcmd.Parameters.AddWithValue("@Id", customer.Id);
            var rowsAffected = sqlcmd.ExecuteNonQuery();
            return (rowsAffected == 1);
        }
        //update data in table

        public bool Change(Customer customer)
        {
            var sql = $"UPDATE Customers Set " +
                "Name = @Name, " +
                "City = @City, " +
                "State = @State, " +
                "Sales = @Sales, " +
                "Active = @Active" +
                  "Where Id = @Id;";
            var sqlcmd = new SqlCommand(sql, connection.SqlConn);
            sqlcmd.Parameters.AddWithValue("@Id", customer.Id);
            FillCustomerFromUpdateorChange(sqlcmd, customer);
            var rowsAffected = sqlcmd.ExecuteNonQuery();
            return (rowsAffected == 1);
        }
        //create data in table
        public bool Create(Customer customer)
        {
            var sql = $"INSERT into Customers " + "(Id, Name, City, State, Sales, Active)" +
                   " VALUES " + $" (@Id, @Name, @City, @State, @Sales, @Active);";
            var sqlcmd = new SqlCommand(sql, connection.SqlConn);
            FillCustomerFromUpdateorChange(sqlcmd, customer);
            var rowsAffected = sqlcmd.ExecuteNonQuery();
            return (rowsAffected == 1);
        }
        //read all data
        public List<Customer> GetAll()
        {
            var sql = "SELECT * From Customers;";
            var cmd = new SqlCommand(sql, connection.SqlConn);
            var reader = cmd.ExecuteReader();
            var customers = new List<Customer>();
            while (reader.Read())
            {
                var customer = FillCustomerFromReader(reader);
                customers.Add(customer);
            }
            reader.Close();
            return customers;
        }
        //read by primary key
        public Customer GetByPK(int id)
        {
            var sql = $"SELECT * From Customers where id = {id};";
            var sqlcmd = new SqlCommand(sql, connection.SqlConn);
            var reader = sqlcmd.ExecuteReader();
            if (!reader.HasRows)
            {
                reader.Close();
                return null;
            }
            reader.Read();
            var vendor = FillCustomerFromReader(reader);
            reader.Close();
            return vendor;
        }
    }

}
