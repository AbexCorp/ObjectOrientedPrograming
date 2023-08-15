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


            GetIssueMetaData(in issues, out string year, out string number, out string volume);


            var articles = issues
                ?.Descendants("issue")
                ?.Descendants("section")
                ?.Descendants("article") ?? throw new ArgumentException("Articles were not found");


            string fullDoi;

            XDocument newFile = new();
            foreach(XElement article in articles)
            {
                //Collect data
                fullDoi = GetArticleDoi(in article).Trim();
                string[] doi = fullDoi.Split('-', StringSplitOptions.RemoveEmptyEntries);
                doi = new string[] { doi[doi.Length - 2], doi[doi.Length-1] };

                string enUsTitle = GetArticleTitle(in article);
                string enUsText = GetArticleText(in article);
                string[] enUsKeywords = GetArticleKeywords(in article);
                //authors
                string pages = GetArticlePages(in article);
                string publishDate = GetArticlePublishDate(in article);
                //permissions
                //galley





                //Convert data
                //newFile = new XDocument(
                //    new XDeclaration("1.0", "utf-8", "yes"),
                //    new XElement("Article")
            }



            //string newfile = Path.Combine(Directory.GetCurrentDirectory(), "issues.xml");
            //File.CreateText(newfile);
            //File.AppendAllLines(newfile, issue.ToString().);
        }



        private static bool GetIssueMetaData(in XDocument issues, out string year, out string number, out string volume)
        {
            try
            {
                volume = (issues
                    ?.Descendants("issue")
                    ?.Descendants("volume")
                    ?.FirstOrDefault() ?? throw new ArgumentException("Volume not found"))
                    .Value;
                number = (issues
                    ?.Descendants("issue")
                    ?.Descendants("number")
                    ?.FirstOrDefault() ?? throw new ArgumentException("Number not found"))
                    .Value;
                year = (issues
                    ?.Descendants("issue")
                    ?.Descendants("year")
                    ?.FirstOrDefault() ?? throw new ArgumentException("Year not found"))
                    .Value;
                return true;
            }
            catch (ArgumentException)
            {
                volume = null;
                number = null;
                year = null;
                return false;
            }
        }
        private static string GetArticleDoi(in XElement article)
        {
            string fullDoi = (article
                ?.Descendants("galley")
                ?.Descendants("file")
                ?.Descendants("remote")
                ?.Attributes("src")
                ?.FirstOrDefault() ?? throw new ArgumentException("Doi source was not found"))
                .Value;
            return fullDoi;
        }
        private static string GetArticleTitle(in XElement article)
        {
            string enUsTitle = (article
                ?.Descendants("title")
                ?.FirstOrDefault(x => (x.Attribute("locale") ?? throw new ArgumentException("Locale not found")).Value == "en_US") ?? throw new ArgumentException("Title not found"))
                .Value;
            return enUsTitle;
        }
        private static string GetArticleText(in XElement article)
        {
            string enUsText = (article
                ?.Descendants("abstract")
                ?.FirstOrDefault(x => (x.Attribute("locale") ?? throw new ArgumentException("Locale not found")).Value == "en_US") ?? throw new ArgumentException("Article text not found"))
                .Value;
            return enUsText;
        }
        private static string[] GetArticleKeywords(in XElement article)
        {
            string enUsKeywords = (article
                ?.Descendants("indexing")
                ?.Descendants("subject")
                ?.FirstOrDefault(x => (x.Attribute("locale") ?? throw new ArgumentException("Locale not found")).Value == "en_US") ?? throw new ArgumentException("Keywords not found"))
                .Value;
            return enUsKeywords.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        }
        private static string GetArticlePages(in XElement article)
        {
            string pages = (article
                ?.Descendants("pages")
                ?.FirstOrDefault() ?? throw new ArgumentException("Pages not found"))
                .Value;
            return pages;
        }
        private static string GetArticlePublishDate(in XElement article)
        {
            string publishDate = (article
                ?.Descendants("date_published")
                ?.FirstOrDefault() ?? throw new ArgumentException("Publish date not found"))
                .Value;
            return publishDate;
        }
    }
}
