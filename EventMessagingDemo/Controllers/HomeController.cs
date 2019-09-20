using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EventMessagingDemo.Models;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Text;

namespace EventMessagingDemo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("View");
        }

        [HttpPost]
        public IActionResult PlaceOrder(OrderModel order)
        {
            const string ServiceBusConnectionString = "";
            const string QueueName = "orders";

            IQueueClient queueClient;

            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);

            var message = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(order)));
            message.ContentType = "application/json";

            queueClient.SendAsync(message);

            return View("View");
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


    }
}
