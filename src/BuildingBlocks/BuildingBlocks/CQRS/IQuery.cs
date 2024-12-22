using MediatR;

namespace BuildingBlocks.CQRS;

    public interface IQuery<out TResonse> : IRequest<TResonse>
    where TResonse : notnull
    {
    }
    
