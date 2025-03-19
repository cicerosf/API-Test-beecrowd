using SalesApi.Domain.Entities;
using SalesApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApi.Infrastructure.Messaging
{
    public class EventPublisher : IEventPublisher
    {
        public void PublishSaleCreated(Sale sale)
        {
            Console.WriteLine($"SaleCreated event published for Sale ID: {sale.Id}");
        }

        public void PublishSaleCancelled(Sale sale)
        {
            Console.WriteLine($"SaleCancelled event published for Sale ID: {sale.Id}");
        }

        public void PublishProductCreated(Product product)
        {
            Console.WriteLine($"ProductCreated event published for Product ID: {product.Id}");
        }
    }
}
