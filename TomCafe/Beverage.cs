using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomCafe
{
    class Beverage : Product
    {
        private double tradeIn;

        public double TradeIn
        {
            get { return tradeIn; }
            set { tradeIn = value; }
        }

        public Beverage()
        {

        }

        public Beverage(string n, double p, double t) : base(n, p)
        {
            TradeIn = t;
        }

        public override double GetPrice()
        {
            return base.Price;
        }

        public override string ToString()
        {
            return Name + "\n" + $"+${TradeIn:0.00}";
        }
    }
}

