using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MVC学习.Logger
{
    public class FileLogger
    {
        public void LoggerException(Exception e)
        {
            File.WriteAllLines("C://Error//" + DateTime.Now.ToString("dd-MM-yyyy mm hh ss") + ".txt", new string[]
            {
                "message:"+e.Message,
                "StackTrace:"+e.StackTrace
            });
        }
    }
}