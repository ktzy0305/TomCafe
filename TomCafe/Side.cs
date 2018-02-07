using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomCafe
{
    class Side : Product
    {
        public Side()
        {

        }

        public Side(string n, double p) : base(n, p)
        {

        }

        public override double GetPrice()
        {
            return base.Price;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
