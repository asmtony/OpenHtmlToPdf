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
            string seconds = DateTime.Now.Second.ToString();

            return $"{year}{month}{day}{seconds}";
        }
    }
}