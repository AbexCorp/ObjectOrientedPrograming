using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewConcepts.DelegateBasics.EventStockMarket
{
    public class Client //Subscriber
    {
        public string Name { get; private set; }

        public EventHandler OnPriceChange = Client.OnPriceChangeDefault; //!!!

        public Client(string name)
        { Name = name; }

        //Has to be public because publisher calls it. But that means that you can change it from outside
        public static void OnPriceChangeDefault(object sender, EventArgs args)
        {
            Stock.StockEventArgs args1 = (Stock.StockEventArgs)args; //!!!
            if (args1.OldPrice > args1.NewPrice)
                Console.WriteLine($"Client. Recommendation: {args1.Symbol} buy");
            if (args1.OldPrice < args1.NewPrice)
                Console.WriteLine($"Client. Recommendation: {args1.Symbol} sell");
        }
    } // end Client
}
