using System;
using System.Collections.Generic;
using System.Text;

namespace CourtCheckInPrism.Converters
{
    public class ItemTappedEventArgs : EventArgs
    {
        public object Item { get; }
        public object Group { get; }
    }
}
