using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiPdfCreater.Helper
{
    public class FileNameCreater
    {
        public static string CreateDatetimeFilename()
        {
            string year = DateTime.Now.Year.ToString();
            string month = (DateTime.Now.Month.ToString().Length == 1) ? $"0{DateTime.Now.Month.ToString()}" : DateTime.Now.Month.ToString();
            string day = (DateTime.Now.Day.ToString().Length == 1) ? $"0{DateTime.Now.Day.ToString()}" : DateTime.Now.Day.ToString();            

            TimeSpan mySpan = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            string secondsSinceMidnight = mySpan.TotalSeconds.ToString();
            return $"{year}_{month}_{day}_{secondsSinceMidnight}";
        }
    }
}