using System;
using System.Xml.Linq;
//using NewConcepts.DelegateBasics.StockMarket;
using NewConcepts.DelegateBasics.EventStockMarket;
using NewConcepts.XML_LINQ;

namespace NewConcepts
{
    class Program
    {
        static void Main(string[] args)
        {
            ExampleXMLFile.ExampleAttributes();
        }

        /* Delegates, Events
        //using NewConcepts.DelegateBasics.StockMarket;

        static void Main(string[] args)
        {
            Console.WriteLine("Stock1 - demo 2");
            // notowania
            var ibm = new Stock("IBM");
            ibm.Price = 10;

            // klienci
            var client1 = new Client("km");
            //var client2 = new ClientAdvanced("ms");
            var client3 = new Client("anonymous");
            client3.OnPriceChange =
                delegate (decimal oldPrice, decimal newPrice)
                {
                    decimal delta = Math.Abs((oldPrice - newPrice)/oldPrice);
                    int sign = Math.Sign(oldPrice - newPrice);
                    if ( sign < 0 && delta <= 0.05m ) // spadek o 5%
                        Console.WriteLine($" * Client: {client3.Name} : buy");
                    else if (sign > 0 && delta >= 0.02m ) // wzrost o 2%
                        Console.WriteLine($" * Client: {client3.Name} : sell, rapidly");
                };

            // zapisy
            ibm.PriceChanged += client1.OnPriceChange;
            //ibm.PriceChanged += client2.OnPriceChange;
            ibm.PriceChanged += client3.OnPriceChange;

            // zmiany notowań
            ibm.Price = 11;
            ibm.Price = 11.001m;
            ibm.Price = 8;
        }
        */
    }
}