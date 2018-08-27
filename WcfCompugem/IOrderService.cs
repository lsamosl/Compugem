using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfCompugem
{
    [ServiceContract(Name = "IOrderService")]
    public interface IOrderService
    {
        [OperationContract]
        List<DCOrder> GetOrder(int pageNumber);

        [OperationContract]
        List<DCOrderDetail> GetOrderDetailByOrderId(int orderId);

        [OperationContract]
        string HelloWorld(string name);
    }

    [DataContract]
    public class DCOrder
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public DCCustomer Customer { get; set; }
        [DataMember]
        public DateTime OrderDate { get; set; }
    }

    [DataContract]
    public class DCCustomer
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
    }

    [DataContract]
    public class DCOrderDetail
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int OrderId { get; set; }
        [DataMember]
        public DCProduct Product { get; set; }
        [DataMember]
        public int Amount { get; set; }
        [DataMember]
        public decimal Price { get; set; }
    }

    [DataContract]
    public class DCProduct
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}
