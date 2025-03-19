using MediatR;
using Microsoft.EntityFrameworkCore;
using SalesApi.Domain.Entities;
using SalesApi.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApi.Application.Sales.GetSales
{
    public class GetSalesHandler : IRequestHandler<GetSales, List<Sale>>
    {
        private readonly SalesDbContext _context;

        public GetSalesHandler(SalesDbContext context)
        {
            _context = context;
        }

        public async Task<List<Sale>> Handle(GetSales request, CancellationToken cancellationToken)
        {
            return await _context
                .Sales
                .Include(s => s.Items)
                .ToListAsync(cancellationToken);
        }
    }
}
