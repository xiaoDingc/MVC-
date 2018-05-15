using MVC学习.Filter;
using System.Web;
using System.Web.Mvc;

namespace MVC学习
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new EmployeeExceptionFilter());
            //filters.Add(new HandleErrorAttribute());
            //filters.Add(new AuthorizeAttribute());
        }
    }
}
