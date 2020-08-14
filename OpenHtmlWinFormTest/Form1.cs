using OpenHtmlToPdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenHtmlWinFormTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnCreatePdf_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StringBuilder sb = new StringBuilder();
           
            var currentDir = Directory.GetCurrentDirectory();
            var saveDirectory = currentDir + "\\" + "pdfStuf";
            if (!Directory.Exists(saveDirectory))
                Directory.CreateDirectory(saveDirectory);
            var webPageFullLocalPath = saveDirectory + "\\" + "report.html";

            var pdfFullLocalPath = saveDirectory + "\\aFile.pdf";
            if (!File.Exists(pdfFullLocalPath))
                File.Delete(pdfFullLocalPath);

            if (File.Exists(webPageFullLocalPath))
                File.Delete(webPageFullLocalPath);

            WebClient Client = new WebClient();
            
            sb.AppendLine("Starting download - " + DateTime.Now);
            txtOutputStatus.Text = sb.ToString();
            Client.DownloadFile("http://goengagexopa.cloudapp.net/Reports/10640/report.html", webPageFullLocalPath);

            long seconds = sw.ElapsedMilliseconds / 1000;
            sw.Stop();
            sb.AppendLine($"Finished download took {seconds} seconds");

            txtOutputStatus.Text = sb.ToString();

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
            double mb = ConvertBytesToMegabytes(content.Length);

            sb.AppendLine("Saving pdf - " + DateTime.Now);
            txtOutputStatus.Text = sb.ToString();
            seconds = sw.ElapsedMilliseconds / 1000;
            sw.Stop();
            File.WriteAllBytes(pdfFullLocalPath, content.ToArray());
            sb.AppendLine($"Creating pdf took {seconds} seconds");
            sb.AppendLine("Finished - " + DateTime.Now);
            txtOutputStatus.Text = sb.ToString();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }
    }
}
