using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeggieApp.Model.Model.Cart;
using VeggieApp.Model.Model.Product;

namespace VeggieApp.DataSource.Data
{
    public class ProductStore
    {
        public List<Product> CartList = new List<Product>();
        public static List<CartViewModel> CartNewList = new();
        public int Value { get; set; }
        public event Action OnStateChange;
        public void SetValue()
        {
            this.Value = CartList.Count;
            /*this.Value = CartNewList.Count;*/
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnStateChange?.Invoke();
    }
}
