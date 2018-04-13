using System;
using System.Collections.Generic;
using System.Xml;
using IT_company_locatorDAL;

namespace IT_Company_locatorBL
{
    public class Parsing
    {
        #region Variables
        private String key = "AIzaSyCF_IUcs-I8BXC9MhgBLjYaZeuNlu1u62w";
        static String Url;
        static String Token = "";
        int counter = 0;
        #endregion
        #region methods
        /// This methods splits the query into words as required in API 
        /// </summary>
        /// <param name="textbox"></param>
        /// <returns></returns>
        public String SplitToWords(String textbox)
        {
            String _mlocationname;
            _mlocationname = "";
            String[] words = textbox.Split(' ');
            foreach (var iterator in words)
            {
                _mlocationname = _mlocationname + iterator + "+";
            }
            return _mlocationname;
        }
     /// <summary>
        /// This method parses the XML response obtained from API
        /// <param name="apiResponse">It contains API response obtained from API</param>
        /// <returns>This method returns a list of company names and addresses.</returns>
    public List<details> ParsingXMLResponse(String apiResponse)
        { 
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(apiResponse);
            XmlNodeList name = doc.GetElementsByTagName("name");
            if (name.Count != 0)
            {
                List<string> nameList = new List<string>();
                XmlNodeList address = doc.GetElementsByTagName("formatted_address");

                if (counter < 2)
                {
                    XmlNodeList pageToken = doc.GetElementsByTagName("next_page_token");
                    try
                    {
                        Token = pageToken[0].InnerText;
                    }
                    catch(Exception e)
                    {

                    }
                }

                List<string> addressList = new List<string>();
                details[] comp = new details[20];
                int count = 0;
                foreach (XmlNode nameIterator in name)
                {

                    nameList.Add(nameIterator.InnerText);
                    count++;
                }
                count = 0;
                foreach (XmlNode addressIterator in address)
                {
                    addressList.Add(addressIterator.InnerText);
                    count++;
                }

                var companies = new List<details>();
                for (int i = 0; i < count; i++)
                {
                    var company = new details
                    {
                        name = nameList[i],
                        address = addressList[i]
                    };
                    companies.Add(company);
                }
                counter++;
                return companies;
            }
            else
            {
                var companiest = new List<details>();
                return companiest;
            }
        }
        /// <summary>
        /// checks for numeric strings in location query
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        /// <summary>
        /// This method takes location from UI and passes it to API class 
        /// </summary>
        /// <param name="location">This string contains location query entered by user</param>
        /// <returns>This method returns list of companies and addresses</returns>
        public List<details> GetLocation(string location)
        {
            if (location.Length != 0 && !location.Contains("@") && !location.Contains("*") && !location.Contains("#") && !IsDigitsOnly(location))
            {
                String splitLocation = SplitToWords(location);
                Dataaccesslayer con = new Dataaccesslayer();
                Url = "https://maps.googleapis.com/maps/api/place/textsearch/xml?query=it+companies+in+" + splitLocation + "&hasNextPage=true&nextPage()=true" + "&key=" + key + "&pagetoken=";
                List<details> companies = ParsingXMLResponse(con.Connection(Url));
                Url = Url + Token;
                var firstToken = Token;
                System.Threading.Thread.Sleep(1500);
                List<details> nextCompanies = ParsingXMLResponse(con.Connection(Url));
                companies.AddRange(nextCompanies);
                var originalString = "&pagetoken=" + firstToken;
                Url = Url.Replace(originalString, "&pagetoken=");
                Url = Url + Token;
                System.Threading.Thread.Sleep(1500);
                List<details> lastCompanies = ParsingXMLResponse(con.Connection(Url));
                Token = "";
                companies.AddRange(lastCompanies);
                return companies;
            }
            else
            {
                List<details> noresult = new List<details>();
                return noresult;

            }
        }
    }
    #endregion
}