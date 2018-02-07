using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomCafe
{
    class MenuItem
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private double price;

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        private List<Product> productList;

        public List<Product> ProductList
        {
            get { return productList; }
            set { productList = value; }
        }

        public MenuItem()
        {

        }

        public MenuItem(string n, double p)
        {
            Name = n;
            Price = p;
        }
        public double GetTotalPrice()
        {
            double totalprice = 0;
            foreach (Product p in ProductList) //Get the base price of each Product. This value is fixed.
            {
                totalprice += p.GetPrice();
            }
            if (ProductList.Count > 1) //Fixed assignment of bundlemeal prices since if we calculate the total price of the products it would exceed the stated price
            {
                foreach (Product p in ProductList) 
                {
                    if (p.Name == "Hotcakes with sausage")
                    {
                        totalprice = 7.90;
                        break;
                    }
                    else if (p.Name == "Hamburger")
                    {
                        totalprice = 10.20;
                        break;
                    }
                    else if (p.Name == "Ribeye Steak")
                    {
                        totalprice = 18.50;
                        break;
                    }

                }

            }
            return totalprice;
        }

        public override string ToString()
        {
            return $"{Name}\n${Price:0.00}";
        }

    }
}
