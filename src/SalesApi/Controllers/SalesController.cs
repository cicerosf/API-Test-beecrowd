using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesApi.Application.Sales.CancelSale;
using SalesApi.Application.Sales.CreateSale;
using SalesApi.Application.Sales.GetSales;

namespace SalesApi.Controllers
{
    [ApiController]
    [Route("sales")]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SalesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetSales()
        {
            var result = await _mediator.Send(new GetSales());
            
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleCommand command)
        {
            try
            {
                var sale = await _mediator.Send(command);

                return Ok(new { data = sale, status = "success", message = "Venda criada com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { type = "InvalidData", error = ex.Message, detail = "Error creating sale" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelSale(Guid id)
        {
            try
            {
                await _mediator.Send(new CancelSaleCommand { SaleId = id });

                return Ok(new { status = "success", message = "Venda cancelada com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { type = "InvalidData", error = ex.Message, detail = "Error cancelling sale" });
            }
        }
    }
}
