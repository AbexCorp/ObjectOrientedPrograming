using System;
using System.Xml.Linq;

namespace XMLToLinqToXMLConverter
{
    class Program
    {
        public static void Main(string[] args)
        {
            XDocument issueToCopy = XDocument.Load("..\\..\\..\\issues.xml");
            XDocument issue = issueToCopy;
            issue.Save("issues.xml");
            if(issue != issueToCopy)
                throw new Exception("Something went wrong when copying the file");


            //string newfile = Path.Combine(Directory.GetCurrentDirectory(), "issues.xml");
            //File.CreateText(newfile);
            //File.AppendAllLines(newfile, issue.ToString().);
        }
    }
}
