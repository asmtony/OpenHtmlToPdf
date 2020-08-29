using MvcWebPdfCreater.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcWebPdfCreater.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CreatePdf()
        {
            
            PdfFiles pdfFiles = new PdfFiles();
            string reportUrl = "http://goengagexopa.cloudapp.net/Reports/10650/report.html";

            FileResult fileResult = pdfFiles.CreatePdf(reportUrl);

            /*
             * 
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-Type", "application/pdf");
            HttpContext.Current.Response.AddHeader("Cache-Control", "max-age=100");
            HttpContext.Current.Response.AddHeader("Accept-Ranges", "none");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ".pdf");
            Stream.WriteTo(HttpContext.Current.Response.OutputStream);
            Stream.Close();
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();

            */

            return fileResult;
        }
    }
}