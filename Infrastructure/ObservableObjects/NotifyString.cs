using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRP2.Infrastructure.ObservableObjects
{
    public class NotifyString:ObservableObject
    {
        private string value;
        public string Value
        {
            get { return value; }
            set
            {
                this.value = value;
                Notify();
            }
        }
    }
}
