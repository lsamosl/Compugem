using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SQLDataAccess
    {
        private string ConnectionString { get; set; }

        public SQLDataAccess()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["CS_Compugen"].ConnectionString;
        }

        public List<Order> GetOrders(int pageNumber)
        {
            try
            {
                SqlDataReader reader;
                List<Order> Orders = new List<Order>(); ;
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                using (SqlCommand cmd = new SqlCommand("sp_orders", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PageNumber",SqlDbType.Int).Value = pageNumber;

                    conn.Open();

                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Orders.Add(new Order
                            {
                                Id = reader.GetInt32(0),
                                Customer = new Customer()
                                {
                                    Id = reader.GetInt32(1),
                                    Name = reader.GetString(2)
                                },
                                OrderDate = reader.GetDateTime(3)
                            });

                        }
                    }

                    conn.Close();

                    return Orders;
                }
            }
            catch
            {
                return new List<Order>();
            }
        }

        public List<OrderDetail> GetOrderDetail(int orderId)
        {
            try
            {
                SqlDataReader reader;
                List<OrderDetail> orderDetail = new List<OrderDetail>();
                string query = "SELECT od.Id, od.OrderId, od.ProductId, p.Name ProductName , od.Amount, od.Price" + Environment.NewLine;
                query += "FROM OrdersDetail od" + Environment.NewLine;
                query += "INNER JOIN Products p ON p.Id = od.ProductId" + Environment.NewLine;
                query += "WHERE od.OrderId = @OrderId";

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OrderId", orderId);

                    conn.Open();

                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            orderDetail.Add(new OrderDetail
                            {
                                Id = reader.GetInt32(0),
                                OrderId = reader.GetInt32(1),
                                Product = new Product
                                {
                                    Id = reader.GetInt32(2),
                                    Name = reader.GetString(3)
                                },
                                Amount = reader.GetInt32(4),
                                Price = reader.GetDecimal(5)
                            });
                        }
                    }

                    conn.Close();

                    return orderDetail;
                }
            }
            catch
            {
                return new List<OrderDetail>();
            }
        }
    }
}
