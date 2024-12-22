namespace Dokan.Service.Catalog.Products.CreateProduct;


// Command Query
public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;

// Command Response
public record CreateProductResult(Guid Id);
internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // Business logic for creating product, saving to database etc.
        // return CreateProductResult

        var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price,
            };


        session.Store(product);

        await session.SaveChangesAsync(cancellationToken);
        return new CreateProductResult(product.Id);

    }
}

