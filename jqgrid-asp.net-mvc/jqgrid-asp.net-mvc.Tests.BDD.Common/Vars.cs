using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace jqgrid_asp.net_mvc.Tests.BDD.Common
{
    public class Vars
    {
        public static readonly string DemoSiteWebHost = ConfigurationManager.AppSettings["DemoSiteWebHost"];
    }
}
