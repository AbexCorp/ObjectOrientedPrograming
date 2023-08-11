using System;
using System.Xml.Linq;

namespace XMLToLinqToXMLConverter
{
    class Program
    {
        public static void Main(string[] args)
        {
            XDocument issueToCopy = XDocument.Load("..\\..\\..\\issues.xml");
            XDocument issues = issueToCopy;
            issues.Save("issues.xml");
            if(issues != issueToCopy)
                throw new Exception("Something went wrong when copying the file");


            string volume = (issues
                ?.Descendants("issue")
                ?.Descendants("volume")
                ?.FirstOrDefault() ?? throw new ArgumentException("Volume not found"))
                .Value;
            string number = (issues
                ?.Descendants("issue")
                ?.Descendants("number")
                ?.FirstOrDefault() ?? throw new ArgumentException("Number not found"))
                .Value;
            string year = (issues
                ?.Descendants("issue")
                ?.Descendants("year")
                ?.FirstOrDefault() ?? throw new ArgumentException("Year not found"))
                .Value;

            var articles = issues
                ?.Descendants("issue")
                ?.Descendants("section")
                ?.Descendants("article") ?? throw new ArgumentException("Articles were not found");


            string fullDoi;

            XDocument newFile = new();
            foreach(XElement article in articles)
            {
                fullDoi = (article
                    ?.Descendants("galley")
                    ?.Descendants("file")
                    ?.Descendants("remote")
                    ?.Attributes("src")
                    ?.FirstOrDefault() ?? throw new ArgumentException("Doi source was not found"))
                    .Value;
                string[] doi = fullDoi.Split('-', StringSplitOptions.RemoveEmptyEntries);
                doi = new string[] { doi[doi.Length - 2], doi[doi.Length-1] };
                Console.WriteLine($"{doi[0]},{doi[1]}");

                //newFile = new XDocument(
                //    new XDeclaration("1.0", "utf-8", "yes"),
                //    new XElement("Article")
            }



            //string newfile = Path.Combine(Directory.GetCurrentDirectory(), "issues.xml");
            //File.CreateText(newfile);
            //File.AppendAllLines(newfile, issue.ToString().);
        }
    }
}
