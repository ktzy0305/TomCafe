using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomCafe
{
    class ValueMeal : Product
    {
        private DateTime startTime;

        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        private DateTime endTime;

        public DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        public ValueMeal()
        {

        }

        public ValueMeal(string n, double p, DateTime st, DateTime et) : base(n, p)
        {
            StartTime = st;
            EndTime = et;
        }

        public override double GetPrice()
        {
            return base.Price;
        }

        public bool IsAvailable()
        {
            if (DateTime.Now >= StartTime && DateTime.Now <= EndTime)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return base.ToString() + "\t" + StartTime + "\t" + EndTime;
        }

    }
}

