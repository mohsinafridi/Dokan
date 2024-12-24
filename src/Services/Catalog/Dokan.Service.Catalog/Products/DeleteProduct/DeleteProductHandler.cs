
using Dokan.Service.Catalog.Products.CreateProduct;

namespace Dokan.Service.Catalog.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

public record DeleteProductResult(bool IsSuccess);

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required");
    }
}

internal class DeleteProductCommandHandler(IDocumentSession session)
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {       

        session.Delete(command);

        await session.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);


    }
}
