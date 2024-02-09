using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VeggieApp.Model.Model;
using VeggieApp.Model.Model.Authentication;
using VeggieApp.Model.Model.Product;

namespace VeggieApp.DataSource.Service.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

       


        //For get all orders
        public async Task<IEnumerable<Order>> GetOrderList()
        {
            try
            {
                //var response1 = await _httpClient.GetAsync("api/Order/GetOrders");
                //var d =response1.Content.ReadFromJsonAsync<dynamic>();
                //var response = response1.Content.ReadFromJsonAsync<IEnumerable<Order>>();
                //var f = response;
                //return d;
                var response = await _httpClient.GetFromJsonAsync<IEnumerable<Order>>("api/Order/GetOrders");
                var orders = response.ToList();
                return orders;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //For Create Order
        public async Task<OrderViewModel> CreateOrder(OrderViewModel order)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync<OrderViewModel>("api/Order/CreateOrder",order);
                var orderResult = JsonSerializer.Deserialize<OrderViewModel>(await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (!response.IsSuccessStatusCode)
                {
                    return orderResult!;
                }
                return orderResult;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
