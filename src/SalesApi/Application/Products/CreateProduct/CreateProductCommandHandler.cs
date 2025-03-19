using MediatR;
using SalesApi.Domain.Entities;
using SalesApi.Domain.Interfaces;
using SalesApi.Infrastructure.Database;
using SalesApi.Infrastructure.Messaging;

namespace SalesApi.Application.Products.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly SalesDbContext _context;
        private readonly IEventPublisher _eventPublisher;

        public CreateProductCommandHandler(SalesDbContext context, IEventPublisher eventPublisher)
        {
            _context = context;
            _eventPublisher = eventPublisher;
        }

        public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Title = request.Title,
                Price = request.Price,
                Description = request.Description,
                Category = request.Category,
                Image = request.Image,
                Id = Guid.NewGuid()
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync(cancellationToken);

            _eventPublisher.PublishProductCreated(product);
            
            return product;
        }
    }
}
