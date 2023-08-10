using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NewConcepts.XML_LINQ
{
    //More
    //https://kmolenda.github.io/linq-to-xml.html

    public class ExampleXMLFile
    {
        //XComment - XML Comment

        //XDeclaration - Metadata, used xml standard, encoding type, using outside references
        //new XDeclaration("1.0", "utf-8", "yes")
        //<?xml version="1.0" encoding="utf-8" standalone="yes"?>
        
        //XProcessingInstruction - additional information on how the used. Mostly used to combine stylesheets with xml files.
        //new XProcessingInstruction("xml-stylesheet",  - Targed
        //@"href=""style.css"" type=""text/css"""), - Parameters



        public static XDocument CreateExampleXML()
        {
            XDocument xml = 
                new XDocument(
                    new XElement("Employees",
                        new XElement("Employee",
                            new XElement("Name", "Bob James"),
                            new XElement("PhoneNumber", "123456789")
                        ),
                        
                        new XElement("Employee",
                            new XElement("Name", "Billy Bob"),
                            new XElement("PhoneNumber", "987654321")
                        )
                    )
                );
            return xml;
        }
        public static void ReadingExampleXML()
        {
            XDocument exXML = CreateExampleXML();
            Console.WriteLine(exXML);
            Console.WriteLine();
            Console.WriteLine("Example made");
            Console.WriteLine();
            Console.WriteLine();


            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Nodes");
            //Returns everything including comments, so it return IEnumerable<XNode>
            Console.WriteLine();
            foreach (var node in exXML.Nodes())
            {
                Console.WriteLine(node);
            }


            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Elements");
            //Returns IEnumerable<XElement>
            Console.WriteLine();
            foreach (var employee in exXML.Root.Elements("Employee"))
            {
                //Console.WriteLine(employee);

                Console.WriteLine(employee.Name);
                foreach(var x in employee.Elements())
                {
                    Console.WriteLine(x.Name + " " + x.Value);
                }
            }


            //Other
            //Element
            //Descendants
            //DescendantsAndSelf
            //Ancestors
            //AncestorsAndSelf
            //Parent
        }
        public static void EditingExampleXML()
        {
            XDocument exXML = CreateExampleXML();
            Console.WriteLine("Before editing");
            Console.WriteLine();
            Console.WriteLine(exXML);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            //Add
            XElement newEmployee = new XElement("Employee", new XComment("Mega Giga"), new XElement("Name", "Mary Read"), new XElement("PhoneNumber", "213742069"));
            exXML.Root.Add(newEmployee);
            newEmployee = new XElement("Employee", new XElement("Name", "Bohdan Khmelnytsky"), new XElement("PhoneNumber", "169401649"));
            exXML.Root.Add(newEmployee);


            #pragma warning disable CS8600, CS8602 // Converting null literal or possible null value to non-nullable type.
            //Remove

            XElement employeeToRemove = exXML
                .Descendants("Employee")
                .FirstOrDefault(x => x.Element("Name").Value == "Bob James");
                //.FirstOrDefault(x => (x.Element("Name")?? throw new ArgumentNullException()).Value == "Bob James") ?? throw new ArgumentNullException(); //Correct
            employeeToRemove.Remove();


            //Change
            XElement employeeToChange = exXML
                .Descendants("Employee")
                .FirstOrDefault(x => x.Element("Name").Value == "Billy Bob");
            employeeToChange.SetElementValue("Name", "Bob Bob");

            #pragma warning restore CS8600, CS8602 // Converting null literal or possible null value to non-nullable type.


            Console.WriteLine("After editing");
            Console.WriteLine();
            Console.WriteLine(exXML);


            //Other
            //AddFirst
            //AddBeforeSelf (for child elements)
            //AddAfterSelf
            //RemoveNode
        }
        public static void ExampleAttributes()
        {
            XDocument exXML = 
                new XDocument(
                    new XElement("Employees",
                        new XElement("Employee",
                                new XAttribute("Gender", "Male"),
                                new XAttribute("Id", "1"),
                            new XElement("Name", "Bob James"),
                            new XElement("PhoneNumber", "123456789")
                        ),
                        
                        new XElement("Employee",
                            new XElement("Name", "Billy Bob"),
                            new XElement("PhoneNumber", "987654321")
                        )
                    )
                );
            Console.WriteLine(exXML);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            foreach(var x in exXML.Descendants("Employee"))
            {
                // ?. to avoid exception if any Employee is missing the attribute
                var id = x.Attribute("Id")?.Value;
                var gender = x.Attribute("Gender")?.Value;
                Console.WriteLine($"{id} {gender}");
            }

            #pragma warning disable CS8600, CS8602 // Converting null literal or possible null value to non-nullable type.

            XElement employeeToRemoveFrom = exXML
                .Descendants("Employee")
                .FirstOrDefault(x => x.Element("Name").Value == "Bob James");
            employeeToRemoveFrom?.Attribute("Gender").Remove();

            XElement employeeToAddTo = exXML
                .Descendants("Employee")
                .FirstOrDefault(x => x.Element("Name").Value == "Billy Bob");
            employeeToAddTo.SetAttributeValue("Id", "2");

            #pragma warning restore CS8600, CS8602 // Converting null literal or possible null value to non-nullable type.


            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(exXML);
        }
        private static void ExampleLinqToXml()
        {
            static void OutputProductsAsXml()
            {
                /*
                using (var db = new Northwind())
                {
                    var productsForXml = db.Products.ToArray();
                    var xml = new XElement("products", 
                        from p in productsForXml 
                        select new XElement("product",
                        new XAttribute("id", p.ProductID),
                        new XAttribute("price", p.UnitPrice),
                        new XElement("name", p.ProductName)));
                    Console.WriteLine(xml.ToString());
                }
                */


                /*
                <products>
                  <product id="1" price="18">
                    <name>Chai</name>
                  </product>
                  <product id="2" price="19">
                    <name>Chang</name>
                  </product>
                ...
                */
            }
        }
    }
}
