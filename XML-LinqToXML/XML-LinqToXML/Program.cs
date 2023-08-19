using System;
using System.ComponentModel;
using System.Reflection.Emit;
using System.Xml.Linq;
using System.Xml.Serialization;

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
            var sectionRef = (issues
                ?.Descendants("issue")
                ?.Descendants("section")
                ?.Descendants("abbrev")
                ?.FirstOrDefault(x => (x.Attribute("locale") ?? throw new ArgumentException("Locale not found")).Value == "en_US") ?? throw new ArgumentException("SectionRef not found"))
                .Value;


            string fullDoi;

            XDocument newFile = new();
            foreach(XElement article in articles)
            {
                //Collect data
                fullDoi = GetArticleDoi(in article).Trim();
                string[] doi = fullDoi.Split('-', StringSplitOptions.RemoveEmptyEntries);
                doi = new string[] { doi[doi.Length - 2], doi[doi.Length-1] };

                string enUsTitle = GetArticleTitle(in article).Trim();
                string? prefix = GetArticleTitlePrefix(enUsTitle, out string newTitle);
                enUsTitle = newTitle;

                string enUsText = GetArticleText(in article);
                string[] enUsKeywords = GetArticleKeywords(in article);

                var authors = article
                    ?.Descendants("author")
                    ?.Select(x => 
                    (
                        firstname: (x?.Descendants("firstname")?.FirstOrDefault() ?? throw new ArgumentException("First name not found")).Value
                        ,lastname: (x?.Descendants("lastname")?.FirstOrDefault() ?? throw new ArgumentException("Last name not found")).Value
                        ,affiliation: (x?.Descendants("affiliation")
                            ?.FirstOrDefault( z => (z.Attribute("locale") ?? throw new ArgumentException("Locale not found")).Value == "en_US")
                            ?? throw new ArgumentException("Affiliation not found")).Value
                        ,email: (x?.Descendants("email")?.FirstOrDefault() ?? throw new ArgumentException("Email not found")).Value
                        ,country: (x?.Descendants("country")?.FirstOrDefault())?.Value ?? null
                        ,primaryContact: x?.Attributes().FirstOrDefault(y => y.Name == "primary_contact")?.Value ?? "false"
                    ));
                
                string pages = GetArticlePages(in article);
                string publishDate = GetArticlePublishDate(in article);

                var permissions = (article
                    ?.Descendants("permissions")
                    ?.Select(x =>
                    (
                        licenseUrl: (x?.Descendants("license_url").FirstOrDefault() ?? throw new ArgumentException("License url not found")).Value
                        ,copyrightHolder: (x?.Descendants("copyright_holder")
                            ?.FirstOrDefault( z => (z.Attribute("locale") ?? throw new ArgumentException("Locale not found")).Value == "en_US")
                            ?? throw new ArgumentException("Copyright holder not found")).Value
                        ,copyrightYear: (x?.Descendants("copyright_year")?.FirstOrDefault() ?? throw new ArgumentException("Copyright year not found")).Value
                    )) ?? throw new ArgumentException("Something went wrong with permissions"))
                    .First();

                var galley = article
                    ?.Descendants("galley")
                    ?.Select(x =>
                    (
                        label: (x?.Descendants("label").FirstOrDefault() ?? throw new ArgumentException("Label not found")).Value
                        , fileRemote: (x?.Descendants("file")?.Descendants("remote").Attributes("src").FirstOrDefault() ?? throw new ArgumentException("File source not found")).Value
                    ));





                //Convert data
                XNamespace xNamespace = "http://pkp.sfu.ca";
                XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";

                XElement keywordTags = new XElement("keywords", new XAttribute("locale", "en_US"));
                foreach(var keyword in enUsKeywords)
                {
                    keywordTags.Add(new XElement("keyword", keyword));
                }

                XElement authorTags = new XElement("authors",
                  new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                  new XAttribute(xsi + "schemaLocation", "http://pkp.sfu.ca native.xsd"));
                foreach(var author in authors)
                {
                    XElement newAuthor = new XElement("author",
                        new XElement("givenname", author.firstname, new XAttribute("locale", "en_US")),
                        new XElement("familyname", author.lastname, new XAttribute("locale", "en_US")),
                        new XElement("affiliation", author.affiliation, new XAttribute("locale", "en_US")),
                        new XElement("email", author.email)
                    );
                    if (author.primaryContact == "true")
                        newAuthor.Add(new XAttribute("primary_contact", "true"));
                    newAuthor.Add(new XAttribute("include_in_browse", "true"),new XAttribute("user_group_ref", "Author"));

                    if (author.country != null)
                        (newAuthor.Descendants("email").FirstOrDefault() ?? throw new Exception()).AddBeforeSelf(new XElement("country", author.country));

                    authorTags.Add(newAuthor);
                }

                newFile = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement(xNamespace + "Article",
                      new XAttribute("xmlns", "http://pkp.sfu.ca"),
                      new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                      new XAttribute("locale", "en_US"),
                      new XAttribute("date_submitted", "2000-12-25"), //DONT KNOW
                      new XAttribute("stage", "production"), //DONT KNOW
                      new XAttribute("date_published", publishDate),
                      new XAttribute("section_ref", sectionRef),
                      new XAttribute("seq", "1"), //DONT KNOW
                      new XAttribute("access_status", "0"), //DONT KNOW
                      new XAttribute(xsi + "schemaLocation", "http://pkp.sfu.ca native.xsd"),
                        new XElement("id",
                          new XAttribute("type", "internal"),
                          new XAttribute("advice", "ignore"),
                          $"{doi[0]}{doi[1]}"
                        ),
                        new XElement("title",
                          new XAttribute("locale", "en_US"),
                          $"{newTitle}"
                        ),
                        new XElement("prefix",
                          new XAttribute("locale", "en_US"),
                          $"{prefix}"
                        ),
                        new XElement("abstract",
                          new XAttribute("locale", "en_US"),
                          $"{enUsText}"
                        ),
                        new XElement("licenseUrl", permissions.licenseUrl),
                        keywordTags,
                        authorTags,
                        new XElement("article_galley",
                          new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                          new XAttribute(xsi + "schemaLocation", "http://www.w3.org/2001/XMLSchema-instance"),
                          new XAttribute("aproved", "true"),
                            new XElement("id",
                              new XAttribute("type", "internal"),
                              new XAttribute("advice", "ignore"),
                              1 //DONT KNOW
                            ),
                            new XElement("name",
                              new XAttribute("locale", "en_US"),
                              "HTML"
                            ),
                            new XElement("seq", 0), //DONT KNOW
                            new XElement("remote",
                              new XAttribute("src", fullDoi)
                            )
                        ),
                        new XElement("issue_identification",
                            new XElement("volume", volume),
                            new XElement("number", number),
                            new XElement("year", year)
                        ),
                        new XElement("pages", pages)
                    )
                );
                newFile.Save(Path.Combine(Directory.GetCurrentDirectory(), $"{doi[0]}-{doi[1]}.xml"));
            }
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
        private static string? GetArticleTitlePrefix(string title, out string newTitle)
        {
            string prefix = title.Substring(0, title.IndexOf(' '));
            if(prefix.Length <= 3 && (prefix.ToLower() == "the" || prefix.ToLower() == "an" || prefix.ToLower() == "a"))
            {
                newTitle = title.Remove(0, title.IndexOf(' ')).Trim();
                string firstLetter = newTitle[0].ToString().ToUpper();
                newTitle = newTitle.Remove(0, 1).Insert(0, firstLetter);
                return prefix;
            }
            newTitle = title;
            return null;
        }
    }
}
