using ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC学习.Filter
{
    public class HeaderFooterFilter:ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ViewResult viewResult = filterContext.Result as ViewResult;
            if (viewResult != null)
            {
                BaseViewModel bvm = viewResult.Model as BaseViewModel;
                bvm.UserName = HttpContext.Current.User.Identity.Name;
                bvm.FooterData = new FooterViewModel();
                bvm.FooterData.CompanyName = "StepByStepSchools";
                bvm.FooterData.Year = DateTime.Now.Year.ToString();
            }

            base.OnActionExecuted(filterContext);
        }
    }
}