using MediatR;
using Microsoft.EntityFrameworkCore;
using SalesApi.Domain.Entities;
using SalesApi.Infrastructure.Database;

namespace SalesApi.Application.Products.GetProducts
{
    public class GetProductsHandler : IRequestHandler<GetProducts, IEnumerable<Product>>
    {
        private readonly SalesDbContext _context;

        public GetProductsHandler(SalesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> Handle(GetProducts request, CancellationToken cancellationToken)
        {
            return await _context.Products.ToListAsync(cancellationToken);
        }
    }
}
