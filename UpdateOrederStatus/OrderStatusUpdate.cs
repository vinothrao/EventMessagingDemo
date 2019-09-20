using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace UpdateOrederStatus
{
    public static class OrderStatusUpdate
    {
        [FunctionName("OrderStatusUpdate")]
        public static void Run([ServiceBusTrigger("orderstatus", Connection = "SBConString")]string statusMessage, [SignalR(HubName = "notifications")] IAsyncCollector<SignalRMessage> signalRMessages, ILogger log)
        {

            log.LogInformation($"C# ServiceBus queue trigger function processed message: {statusMessage}");

            signalRMessages.AddAsync(
                new SignalRMessage
                {
                    Target = "orderstatus",
                    Arguments = new[] { statusMessage }
                });
        }
    }
}

