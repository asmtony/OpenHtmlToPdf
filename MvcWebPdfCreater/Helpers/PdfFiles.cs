using OpenHtmlToPdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MvcWebPdfCreater.Helpers
{
    public class PdfFiles
    {
        public FileResult CreatePdf( string webPageForPdf)
        {
            string year = DateTime.Now.Year.ToString();
            string month = (DateTime.Now.Month.ToString().Length == 1) ? $"0{DateTime.Now.Month.ToString()}" : DateTime.Now.Month.ToString();
            string day = (DateTime.Now.Day.ToString().Length == 1) ? $"0{DateTime.Now.Day.ToString()}" : DateTime.Now.Day.ToString();
            string seconds = DateTime.Now.Second.ToString();
            string filenamePdf = $"homemade_{year}{month}{day}{seconds}.pdf";


            Stopwatch sw = new Stopwatch();
            sw.Start();
            StringBuilder sb = new StringBuilder();

            var saveDirectory = $"{System.Web.HttpContext.Current.Server.MapPath($"/pdfs/{year}")}";
            saveDirectory = saveDirectory.Replace("/", @"\");

           // var currentDir = Directory.GetCurrentDirectory();
            //var saveDirectory = currentDir + "\\" + "pdfStuf";
            if (!Directory.Exists(saveDirectory))
                Directory.CreateDirectory(saveDirectory);
            var webPageFullLocalPath = saveDirectory + "\\" + "report.html";

            var pdfFullLocalPath = saveDirectory + filenamePdf;
            if (File.Exists(pdfFullLocalPath))
                File.Delete(pdfFullLocalPath);

            if (File.Exists(webPageFullLocalPath))
                File.Delete(webPageFullLocalPath);

            WebClient Client = new WebClient();

            sb.AppendLine("Starting download - " + DateTime.Now);
            //txtOutputStatus.Text = sb.ToString();
            Client.DownloadFile(webPageForPdf, webPageFullLocalPath);

            long secondsTimer = sw.ElapsedMilliseconds / 1000;
            sw.Stop();
            sb.AppendLine($"Finished download took {secondsTimer} seconds");

            //txtOutputStatus.Text = sb.ToString();

            sw.Start();
            string html = File.ReadAllText(webPageFullLocalPath);

            //sb.AppendLine("Creating pdf from HTML - " + DateTime.Now);
            //txtOutputStatus.Text = sb.ToString();


            byte[] content = Pdf.From(html)
                .WithGlobalSetting("web.loadImages", "true")
                .WithGlobalSetting("load.jsdelay", "5000")
                .WithGlobalSetting("load.zoomFactor", "10")
                .WithGlobalSetting("size.pageSize", "A4")
                .Content();
            // double mb = ConvertBytesToMegabytes(content.Length);            

            sb.AppendLine("Saving pdf - " + DateTime.Now);
            // txtOutputStatus.Text = sb.ToString();
            secondsTimer = sw.ElapsedMilliseconds / 1000;
            sw.Stop();
            File.WriteAllBytes(pdfFullLocalPath, content.ToArray());
            sb.AppendLine($"Creating pdf took {secondsTimer} seconds");
            sb.AppendLine("Finished - " + DateTime.Now);
            // txtOutputStatus.Text = sb.ToString();

            FileResult fileResult = new FileContentResult(content, "application/pdf");
            fileResult.FileDownloadName = filenamePdf;

            return fileResult;
        }
    }
}