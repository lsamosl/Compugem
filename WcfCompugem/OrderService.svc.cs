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
        public List<Order> GetOrder(int pageNumber)
        {
            List<Order> orders = new List<Order>();

            if (pageNumber > 0)
            {
                DataAccess da = new DataAccess();
                orders = da.GetDBOrders(pageNumber);
            }

            return orders;
        }
    }
}
