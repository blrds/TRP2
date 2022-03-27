using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TRP2.Infrastructure.ObservableObjects
{
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        public void Notify([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(caller));

        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
