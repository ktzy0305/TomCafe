using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomCafe
{
    class OrderItem
    {
        private int quantity;

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        private MenuItem item ;

        public MenuItem Item
        {
            get { return item ; }
            set { item  = value; }
        }

        public OrderItem()
        {

        }

        public OrderItem(MenuItem mi)
        {
            Item = mi;
        }

        public void AddQty()
        {
            Quantity++;
        }

        public bool RemoveQty()
        {
            Quantity--;
            if (Quantity<=0)
            {
                Quantity = 0;
                return false;
            }
            else
            {
                return true;
            }
        }

        public double GetItemTotalAmount()
        {
            return Item.GetTotalPrice() * Quantity;
        }


        public override string ToString()
        {
            return $"{Item.Name} x{Quantity}\n${Item.Price:0.00}";
        }

    }
}
