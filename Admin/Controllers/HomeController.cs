using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Admin.Models;
using Entities.DB;
using EventMessagingDemo.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using SignalRChat.Hubs;

namespace Admin.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<OrderModel> orderList = new List<OrderModel>();

            using (var ctx = new EventMessagingDemoContext())
            {
                var orders = ctx.Orders.ToList();

                orderList = orders.Select(x => new OrderModel
                {
                    OrderId = x.OrderId,
                    ProductName = x.ProductName,
                    Quantity = (int)x.Quantity,
                    Status = x.Status
                }).ToList();
            }

            return View($"OrderList", orderList);
        }

        public string ChangeOrderStatus(int orderId, int status)
        {
            string orderStatus = "";

            orderStatus = status == 1 ? "Order Accepted" : "Order Rejected";

            using (var ctx = new EventMessagingDemoContext())
            {
                var orders = ctx.Orders.Find(orderId);

                orders.Status = orderStatus;

                ctx.SaveChanges();
            }

            UpdateServiceBus(orderStatus);

            return "Order Updated";
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        private void UpdateServiceBus(string statusMessage)
        {
            const string ServiceBusConnectionString = "";
            const string QueueName = "orderstatus";

            IQueueClient queueClient;

            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);

             

            var message = new Message(Encoding.UTF8.GetBytes( statusMessage) );
            message.ContentType = "application/json";

            queueClient.SendAsync(message);

        }
    }
}
