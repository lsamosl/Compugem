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
        List<Order> GetOrder(int pageNumber);

    }

    [DataContract]
    public class Order
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public Customer Customer { get; set; }
        [DataMember]
        public DateTime OrderDate { get; set; }
    }

    [DataContract]
    public class Customer
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}
