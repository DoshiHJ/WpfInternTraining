using System.Collections.ObjectModel;
using POC13_NavigationApp.Models;

namespace POC13_NavigationApp.ViewModels
{
    public class ProductsViewModel
    {
        public ObservableCollection<Product> Products
        {
            get;
            
        }

        public ProductsViewModel()
        {
            Products = new ObservableCollection<Product>()
            {
                    new Product()
                    {
                        Name="Laptop",
                        Price=50000
                    },

                    new Product()
                    {
                        Name="Keyboard",
                        Price=1500
                    },

                    new Product()
                    {
                        Name="Mouse",
                        Price=700
                    },

                    new Product()
                    {
                        Name="Monitor",
                        Price=12000
                    }
            };
        }
    }
}