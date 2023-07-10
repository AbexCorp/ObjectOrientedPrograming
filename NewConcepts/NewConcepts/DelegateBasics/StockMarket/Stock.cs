using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewConcepts.DelegateBasics.StockMarket
{
    public delegate void PriceChangedHandler(decimal oldPrice, decimal newPrice); //Declaration of delegate type

    public class Stock //Publisher
    {
        private string symbol; //ex. "ibm", "cisco", "microsoft", ... 

        public PriceChangedHandler PriceChanged = null; //Multicast delegate
        //Because it's public anyone can: add/delete new subscriber, reset the list, fake invoke without chaning the price
        //Should be private and have functions to subscribe, unsubscribe
        //Other way to fix it is:
        //public event PriceChangedHandler PriceChanged; //default null
        //Event is public but contains an internal private delegate
        //You can access it by +=, -=, and also use invoke

        private decimal price; //actual price
        public decimal Price
        {
            get => price;
            set
            {
                if (price == value) return;      // Exit if nothing has changed
                Console.WriteLine( $"Price changed! Stock: {symbol}, new price = {value}" );
                decimal oldPrice = price;
                price = value;
                history.Add(DateTime.Now, price);
                PriceChanged?.Invoke(oldPrice, price); //Multicast
            }
        }

        private IDictionary<DateTime, decimal> history 
                    = new Dictionary<DateTime, decimal>();

        public Stock(string symbol) { this.symbol = symbol; }

        public IReadOnlyDictionary<DateTime, decimal> StockHistory
                    => (IReadOnlyDictionary<DateTime, decimal>)history;

    } // end Stock
}
