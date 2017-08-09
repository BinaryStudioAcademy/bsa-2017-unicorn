using System.Web.Mvc;
using Unicorn.Filters;

namespace Unicorn
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new TokenAuthenticateAttribute());
        }
    }
}
