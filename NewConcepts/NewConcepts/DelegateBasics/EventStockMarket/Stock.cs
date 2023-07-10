using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewConcepts.DelegateBasics.EventStockMarket
{

    public class Stock //Publisher
    {
        public class StockEventArgs : EventArgs //!!!
        {
            public string Symbol { get; }
            public decimal OldPrice { get; }
            public decimal NewPrice { get; }
            public StockEventArgs(string symbol, decimal oldPrice, decimal newPrice)
            {
                Symbol = symbol; OldPrice = oldPrice; NewPrice = newPrice;
            }
        }


        private string symbol; //ex. "ibm", "cisco", "microsoft", ... 

        public event EventHandler PriceChanged = null; //!!!

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
                PriceChanged?.Invoke(this, new StockEventArgs(symbol, oldPrice, price)); //Multicast //!!!
            }
        }

        public Stock(string symbol) { this.symbol = symbol; }

    } // end Stock
}
