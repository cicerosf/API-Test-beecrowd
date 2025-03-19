using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesApi.Application.Products.CreateProduct;
using SalesApi.Application.Products.GetProducts;

namespace SalesApi.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _mediator.Send(new GetProducts());

            return Ok(new { data = products, status = "success", message = "Operação concluída com sucesso" });
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            if (command == null)
            {
                return BadRequest(new { type = "InvalidData", error = "Product data is required", detail = "Product details cannot be empty." });
            }

            var product = await _mediator.Send(command);

            return Ok(new { data = product, status = "success", message = "Produto criado com sucesso" });
        }
    }
}
