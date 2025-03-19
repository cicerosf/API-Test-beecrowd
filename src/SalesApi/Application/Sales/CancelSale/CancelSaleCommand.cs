﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApi.Application.Sales.CancelSale
{
    public class CancelSaleCommand : IRequest
    {
        public Guid SaleId { get; set; }
    }
}
