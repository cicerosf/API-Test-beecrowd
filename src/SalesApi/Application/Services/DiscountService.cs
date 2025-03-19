using SalesApi.Domain.Interfaces;
using SalesApi.Infrastructure.Database;

namespace SalesApi.Application.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly SalesDbContext _context;

        public DiscountService(SalesDbContext context)
        {
            _context = context;
        }

        public decimal ApplyDiscount(int quantity, decimal unitPrice)
        {
            var discountConfig = _context.DiscountConfigurations
                .Where(d => quantity >= d.MinQuantity && quantity <= d.MaxQuantity)
                .OrderByDescending(d => d.DiscountPercentage)
                .FirstOrDefault();

            if (discountConfig != null)
            {
                return unitPrice * (1 - discountConfig.DiscountPercentage / 100);
            }

            return unitPrice;
        }
    }
}
