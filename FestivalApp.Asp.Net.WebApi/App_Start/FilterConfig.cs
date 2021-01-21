using System.Web;
using System.Web.Mvc;

namespace FestivalApp.Asp.Net.WebApi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
