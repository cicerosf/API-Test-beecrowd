using SalesApi.Domain.Entities;

namespace SalesApi.Domain.Interfaces
{
    public interface IEventPublisher
    {
        void PublishProductCreated(Product product);
        void PublishSaleCancelled(Sale sale);
        void PublishSaleCreated(Sale sale);
    }
}