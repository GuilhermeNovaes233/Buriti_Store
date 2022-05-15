using Buriti_Store.Orders.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Buriti_Store.Orders.Application.Queries.Interfaces
{
    public interface IOrderQueries
    {
        Task<CartViewModel> GetCartClient(Guid clientId);
        Task<IEnumerable<OrderViewModel>> GetOrdersClient(Guid clientId);
    }
}