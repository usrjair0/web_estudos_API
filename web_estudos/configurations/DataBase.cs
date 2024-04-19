using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_estudos.configurations
{
    public class DataBase
    {
        public static string getConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["consultorioMY"].ConnectionString;
        }
    }
}