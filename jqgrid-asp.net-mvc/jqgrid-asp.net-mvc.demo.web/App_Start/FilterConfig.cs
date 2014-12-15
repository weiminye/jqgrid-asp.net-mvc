using System.Web;
using System.Web.Mvc;

namespace jqgrid_asp.net_mvc.demo.web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
