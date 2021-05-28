using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesDbControllers
{
    class OrderController
    {
        private static Connection connection { get; set; }

        private Order FillOrderFromReader(SqlDataReader reader)
        {
            var order = new Order()
            {
                Id = Convert.ToInt32(reader["Id"]),
                CustomerId = Convert.ToInt32(reader["CustomerId"]),
                Date = Convert.ToDateTime(reader["Date"]),
                Description = Convert.ToString(reader["Description"]),

            };
            return order;
        }

        private void FillOrderParameters(SqlCommand sqlcmd, Order order)
        {

            sqlcmd.Parameters.AddWithValue("@id", order.Id);
            sqlcmd.Parameters.AddWithValue("@customerid", order.CustomerId);
            sqlcmd.Parameters.AddWithValue("@date", order.Date);
            sqlcmd.Parameters.AddWithValue("@description", order.Description);
            
        }

        public List<Order> GetAll()
        {
            var sql = "SELECT * From Orders;";
            var cmd = new SqlCommand(sql, connection.SqlConn);
            var reader = cmd.ExecuteReader();
            var orders = new List<Order>();
            while (reader.Read())
            {
                var order = FillOrderFromReader(reader);
                orders.Add(order);
            }
            reader.Close();
            return orders;
        }

        public Order GetByPK(int id)
        {
            var sql = $"SELECT * From Orders where id = {id};";
            var sqlcmd = new SqlCommand(sql, connection.SqlConn);
            var reader = sqlcmd.ExecuteReader();
            if (!reader.HasRows)
            {
                reader.Close();
                return null;
            }
            reader.Read();
            var order = FillOrderFromReader(reader);
            reader.Close();
            return order;
        }
        public bool Remove(Order order)
        {
            var sql = $"DELETE from Orders " +
                         " Where Id = @id;";
            var sqlcmd = new SqlCommand(sql, connection.SqlConn);
            sqlcmd.Parameters.AddWithValue("@id", order.Id);
            FillOrderParameters(sqlcmd, order);
            var rowsAffected = sqlcmd.ExecuteNonQuery();

            return (rowsAffected == 1);
        }

        public bool change(Order order)
        {
            var sql = $"UPDATE Orders Set " +
                        " CustomerId = @customerid, Date = @date, Description = @description " +
                         " Where Id = @id;";
            var sqlcmd = new SqlCommand(sql, connection.SqlConn);
            sqlcmd.Parameters.AddWithValue("@id", order.Id);

            var rowsAffected = sqlcmd.ExecuteNonQuery();
            FillOrderParameters(sqlcmd, order);
            return (rowsAffected == 1);
        }

        public bool Create(Order order)
        {

            var sql = $"INSERT into Vendors" +
                        " (CustomerId, Date, Description) " +
                        " VALUES " +
                        $" (@customerid, @date, @description " ;
            var sqlcmd = new SqlCommand(sql, connection.SqlConn);
            FillOrderParameters(sqlcmd, order);
            var rowsAffected = sqlcmd.ExecuteNonQuery();

            return (rowsAffected == 1);
        }



        public OrderController(Connection connection) //dependency injection
        {
            OrderController.connection = connection;
        }
    }
}
