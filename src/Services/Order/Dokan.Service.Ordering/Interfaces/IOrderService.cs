using Dokan.Service.Ordering.Models;

namespace Dokan.Service.Ordering.Interfaces
{
    public interface IOrderService
    {
        Task<TransactionResult> AddOrder(Models.Order order);
        Task<IList<Models.Order>> GetOrders();
        Task<Models.Order> GetOrderById(string Id);
        Task<TransactionResult> UpdateOrder(Models.Order order);
        Task<bool> DeleteOrder(string id);
    }
}
