using MediatR;
using SalesApi.Domain.Entities;
using SalesApi.Domain.Exceptions;
using SalesApi.Domain.Interfaces;
using SalesApi.Infrastructure.Database;

namespace SalesApi.Application.Sales.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, Sale>
    {
        private readonly SalesDbContext _context;
        private readonly IDiscountService _discountService;

        public CreateSaleHandler(SalesDbContext context, IDiscountService discountService)
        {
            _context = context;
            _discountService = discountService;
        }

        public async Task<Sale> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            var sale = new Sale
            {
                SaleNumber = command.SaleNumber,
                SaleDate = command.SaleDate,
                CustomerId = command.CustomerId,
                BranchId = command.BranchId,
                Cancelled = false
            };

            decimal totalAmount = 0;
            foreach (var itemCommand in command.Items)
            {
                if (itemCommand.Quantity > 20)
                {
                    throw new BusinessRuleException("Cannot sell more than 20 identical items");
                }

                decimal unitPriceAfterDiscount = _discountService.ApplyDiscount(itemCommand.Quantity, itemCommand.UnitPrice);
                decimal discount = itemCommand.UnitPrice - unitPriceAfterDiscount;
                decimal total = unitPriceAfterDiscount * itemCommand.Quantity;

                // Items below 4 cannot have discount.
                if (itemCommand.Quantity < 4 && discount > 0)
                {
                    throw new BusinessRuleException("Discounts are not allowed for quantities below 4");
                }

                var saleItem = new SaleItem
                {
                    ProductId = itemCommand.ProductId,
                    Quantity = itemCommand.Quantity,
                    UnitPrice = itemCommand.UnitPrice,
                    Discount = discount,
                    Total = total,
                    IsCancelled = false
                };

                sale.Items.Add(saleItem);
                totalAmount += total;
            }

            sale.TotalAmount = totalAmount;
            _context.Add(sale);

            await _context.SaveChangesAsync(cancellationToken);

            return sale;
        }
    }
}
