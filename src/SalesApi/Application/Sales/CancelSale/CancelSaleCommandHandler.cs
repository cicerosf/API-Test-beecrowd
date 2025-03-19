using MediatR;
using Microsoft.EntityFrameworkCore;
using SalesApi.Infrastructure.Database;

namespace SalesApi.Application.Sales.CancelSale
{
    public class CancelSaleCommandHandler : IRequestHandler<CancelSaleCommand>
    {
        private readonly SalesDbContext _context;

        public CancelSaleCommandHandler(SalesDbContext context)
        {
            _context = context;
        }

        public async Task Handle(CancelSaleCommand command, CancellationToken cancellationToken)
        {
            var sale = await _context
                .Sales
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.Id == command.SaleId, cancellationToken);

            if (sale == null)
            {
                throw new Exception("Sale not found");
            }

            sale.Cancelled = true;
            
            foreach (var item in sale.Items)
            {
                item.IsCancelled = true;
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
