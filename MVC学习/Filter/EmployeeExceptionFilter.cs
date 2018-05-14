using MVC学习.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC学习.Filter
{
    public class EmployeeExceptionFilter:HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            FileLogger fLog = new FileLogger();
            fLog.LoggerException(filterContext.Exception);
            base.OnException(filterContext);
        }
    }
}