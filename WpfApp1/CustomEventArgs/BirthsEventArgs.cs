using System;
using WpfApp1.DatabaseFirst;

namespace WpfApp1.CustomEventArgs
{
    public class BirthsEventArgs : EventArgs
    {
        public Births Birth { get; set; }
    }
}
