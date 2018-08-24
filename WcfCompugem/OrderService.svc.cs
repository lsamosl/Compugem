using BusinessObjects;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfCompugem
{
    public class OrderService : IOrderService
    {
        public List<DCOrder> GetOrder(int pageNumber)
        {
            List<DCOrder> orders = new List<DCOrder>();

            if (pageNumber > 0)
            {
                List<Order> DBOrders = new List<Order>();
                SQLDataAccess da = new SQLDataAccess();
                DBOrders = da.GetOrders(pageNumber);

                DBOrders.ForEach(o => orders.Add(new DCOrder
                {
                    Customer = new DCCustomer { Id = o.Customer.Id, Name = o.Customer.Name },
                    Id = o.Id,
                    OrderDate = o.OrderDate
                }));
            }

            return orders;
        }

        public List<DCOrderDetail> GetOrderDetailByOrderId(int orderId)
        {
            List<DCOrderDetail> orderDetail = new List<DCOrderDetail>();

            if(orderId > 0)
            {
                SQLDataAccess da = new SQLDataAccess();
                List<OrderDetail> DBOrderDetail = da.GetOrderDetail(orderId);

                DBOrderDetail.ForEach(od => orderDetail.Add(new DCOrderDetail {
                    Id = od.Id,
                    OrderId = od.OrderId,
                    Product = new DCProduct
                    {
                        Id = od.Product.Id,
                        Name = od.Product.Name
                    },
                    Amount = od.Amount,
                    Price = od.Price
                }));
            }

            return orderDetail;
        }
    }
}
