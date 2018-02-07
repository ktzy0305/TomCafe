using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomCafe
{
    class Order
    {
        private int orderNo;

        public int OrderNo
        {
            get { return orderNo; }
            set { orderNo = value; }
        }

        private List<OrderItem> itemList;

        public List<OrderItem> ItemList
        {
            get { return itemList; }
            set { itemList = value; }
        }

        public Order()
        {

        }

        public void Add(OrderItem o)
        {
            itemList.Add(o);
        }

        public void Remove(int i)
        {
            itemList.RemoveAt(i);
        }

        public double GetTotalAmt()
        {
            double totalamount = 0;
            foreach (OrderItem o in itemList)
            {
                if (o.Item.ProductList.Count > 1)
                {
                    totalamount += o.Item.Price; //The Price attribute of the bundlemeal is modified as custom drinks are added.
                }
                else
                {
                    totalamount += o.GetItemTotalAmount();
                }
            }
            return totalamount;
        }

        public override string ToString()
        {
            return OrderNo+"\t"+ItemList;
        }

    }
}
