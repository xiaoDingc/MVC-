using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MVC学习.Logger
{
    public class FileLogger
    {
        public void LogException(Exception e)
        {
            if (!Directory.Exists("C:/Error"))
            {
                Directory.CreateDirectory("C:/Error");
            }
            File.WriteAllLines("C://Error//" + DateTime.Now.ToString("dd-MM-yyyy mm hh ss") + ".txt",
                new string[]
                {
                    "message"+e.Message,
                   "StackTrace"+e.StackTrace
                });

        }
    }
}