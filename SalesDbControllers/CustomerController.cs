using System;
using System.Collections.Generic;
using System.Text;

namespace SalesDbControllers
{
    public class CustomerController
    {
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

        //read all data
        public List<Customer> GetAll()
        {
            var sql = "SELECT * From Customers;";
            var cmd = new SqlCommand(sql, connection.SqlConn);
            var reader = cmd.ExecuteReader();
            var customers = new List<Customer>();
            while (reader.Read())
            {
                var vendor = FillCustomerFromReader(reader);
                customers.Add(customer);
            }
            reader.Close();
            return customer;
        }
        //read by primary key
        public Vendor GetByPK(int id)
        {
            var sql = $"SELECT * From Vendors where id = {id};";
            var sqlcmd = new SqlCommand(sql, connection.SqlConn);
            var reader = sqlcmd.ExecuteReader();
            if (!reader.HasRows)
            {
                reader.Close();
                return null;
            }
            reader.Read();
            var vendor = FillVendorFromReader(reader);
            reader.Close();
            return vendor;
        }
    }

}
