using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRP2.Models
{
    class DataPoint
    {
        private double time;
        public double Time {
            get{ return time; }
            set{
                if (time != value) {
                    time = value;
                }
            } }

        public DataPoint(double t) {
            Time = t;
        }
    }
}
