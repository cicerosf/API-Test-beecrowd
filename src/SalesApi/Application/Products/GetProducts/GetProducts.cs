using MediatR;
using SalesApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApi.Application.Products.GetProducts
{
    public class GetProducts : IRequest<IEnumerable<Product>>
    {
    }
}
