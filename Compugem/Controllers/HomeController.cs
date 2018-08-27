using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Compugem.OrderService;
using Compugem.Models;

namespace Compugem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int? pageNumber)
        {
            int _pageNumber = pageNumber != null ? pageNumber.Value : 1;
            ViewBag.pageNumber = _pageNumber;

            OrderServiceClient orderService = new OrderServiceClient();
            var orders = orderService.GetOrder(_pageNumber).ToList();
            List<OrderViewModel> orderViewModel = new List<OrderViewModel>();

            orders.ForEach(o => orderViewModel.Add(new OrderViewModel
            {
                CustomerName = o.Customer.Name,
                Id = o.Id,
                OrderDate = o.OrderDate
            }));

            return View(orderViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}