namespace Dokan.Service.Catalog.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<UpdateProductResult>;


public record UpdateProductResult(bool IsSuccess);


public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("Product Id is required");
        RuleFor(c => c.Name).NotEmpty().WithMessage("Product Id is required").Length(2, 150).WithMessage("Name must be b/w 2 and 150 characters.");
        RuleFor(c => c.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}

internal class UpdateProductCommandHandler(IDocumentSession session)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (product == null)
        {
            throw new ProductNotFoundException();
        }

        product.Name = command.Name;
        product.Category = command.Category;
        product.Description = command.Description;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;

        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }
}
