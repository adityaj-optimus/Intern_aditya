using System;
using System.IO;
using System.Net;

namespace IT_company_locatorDAL
{
    public class Dataaccesslayer
    {
        public string Connection(string Query)
        {
            WebRequest request = WebRequest.Create(Query);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            String responsefromserver = reader.ReadToEnd();
            return responsefromserver;
        }
    }
}
