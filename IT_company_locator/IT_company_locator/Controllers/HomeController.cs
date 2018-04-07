
using System.Collections.Generic;
using System.Web.Mvc;
using IT_company_locatorBL;
using IT_Company_locatorBL;

namespace IT_company_locatorBL.Controllers
{
    /// <summary>
    /// This Class contains controllers for MVC architecture
    /// </summary>
    public class HomeController : Controller
    {

        /// <summary>
        /// This Action handler handles all the GET Requests
        /// </summary>
        [HttpGet]
        public ActionResult index()
        {
            return View();
        }
        public ActionResult it_company_locator()
        {
            return View();
        }

        /// <summary>
        /// This method deals with HttpPost Methods
        /// </summary>
        /// <param name="textbox">It conatains location query entered by user</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult it_company_locator(string textbox)
        {
            Parsing parser = new Parsing();
            var resultlist = parser.GetLocation(textbox);

            //MAPPING
            if (resultlist.Count != 0)
            {
                var resultCompany = new List<details>();
                foreach (var item in resultlist)
                {
                    var companylist = new details
                    {
                        name = item.name,
                        address = item.address
                    };
                    resultCompany.Add(companylist);
                }
                return View(resultCompany);
            }

            else
            {

                return View("NoCompany");
            }
        }
    }
}