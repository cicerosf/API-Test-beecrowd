namespace SalesApi.Domain.Interfaces
{
    public interface IDiscountService
    {
        decimal ApplyDiscount(int quantity, decimal unitPrice);
    }
}