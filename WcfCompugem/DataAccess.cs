using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WcfCompugem
{
    public class DataAccess
    {
        private string ConnectionString { get; set; }
        public DataAccess()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["CS_Compugen"].ConnectionString;
        }

        public List<Order> GetDBOrders(int pageNumber)
        {
            try
            {
                SqlDataReader reader;
                List<Order> Orders = new List<Order>(); ;
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                using (SqlCommand cmd = new SqlCommand("sp_orders", conn))
                {
                    SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                    adapt.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapt.SelectCommand.Parameters.Add(new SqlParameter("@PageNumber", SqlDbType.Int));
                    adapt.SelectCommand.Parameters["@PageNumber"].Value = pageNumber;
                    

                    conn.Open();

                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Orders.Add(new Order {
                                Id = reader.GetInt32(0),
                                Customer = new Customer() { Id = reader.GetInt32(1),
                                                            Name = reader.GetString(2)},
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
    }
}