using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeggieApp.Model.Model;

namespace VeggieApp.DataSource.Service.OrderService
{
    public interface IOrderService
    {

        Task<IEnumerable<Order>> GetOrderList();
        Task<OrderViewModel> CreateOrder(OrderViewModel order);
    }
}
