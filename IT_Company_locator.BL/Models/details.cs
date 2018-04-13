using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT_Company_locatorBL
{
    public class details
    {   private string _name;
        private string _address;

        #region properties
        public string name
        {
            get { return _name; }
            set { _name =  value;}
        }
        public string address
        {
            get { return _address; }
            set { _address = value; }
        }

    }
 }
#endregion