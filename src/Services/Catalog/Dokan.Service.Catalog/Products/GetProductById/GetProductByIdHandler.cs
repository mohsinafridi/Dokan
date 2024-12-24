namespace Dokan.Service.Catalog.Products.GetProductById;


public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(Product Product);
internal class GetProductByIdQueryHandler(IDocumentSession session) 
            : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {

        var product  = await session.LoadAsync<Product>(query.Id.ToString(), cancellationToken);

        if(product == null)
        {
            throw new ProductNotFoundException();
        }
        
        return new GetProductByIdResult(product);
    }
}
