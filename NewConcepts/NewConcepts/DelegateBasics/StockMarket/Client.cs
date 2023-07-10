using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewConcepts.DelegateBasics.StockMarket
{
    public class Client //Subscriber
    {
        public string Name { get; private set; }
        public PriceChangedHandler OnPriceChange = null;

        public Client(string name)
        { Name = name; OnPriceChange = this.OnPriceChangeDefault; }

        //Has to be public because publisher calls it. But that means that you can change it from outside
        public virtual void OnPriceChangeDefault(decimal oldPrice, decimal newPrice)
        {
            if (oldPrice > newPrice)
                Console.WriteLine($" - Client {Name}: Recommendation: buy (old price: {oldPrice}, new price: {newPrice})");
            if (oldPrice < newPrice)
                Console.WriteLine($" - Client {Name}: Recommendation: sell (old price: {oldPrice}, new price: {newPrice})");
        }
    } // end Client
}
