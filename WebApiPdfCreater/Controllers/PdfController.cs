using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebApiPdfCreater.Helper;

namespace WebApiPdfCreater.Controllers
{
    public class PdfController : ApiController
    {

        // Taken from
        // https://stackoverflow.com/questions/26038856/how-to-return-a-file-filecontentresult-in-asp-net-webapi

        [HttpGet]
        public HttpResponseMessage GetFile( )
        {
           // string aFileName = fileName;
            var stream = new MemoryStream();
            // processing the stream.

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(stream.ToArray())
            };
            result.Content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = "CertificationCard.pdf"
                };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            return result;
        }
        /*
        // https://localhost:44346/api/Pdf/getfile/fff
        [HttpGet]
        [Route("Api/pdf/GetFile/{fileName}")]
        public HttpResponseMessage GetFile(string fileName)
        {
            string aFileName = fileName;
            var stream = new MemoryStream();
            // processing the stream.

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(stream.ToArray())
            };
            result.Content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = "CertificationCard.pdf"
                };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            return result;
        }
        */
        /*

        [HttpGet]
        public HttpResponseMessage GetFile1()
        {
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.Buffer = false;
            response.BufferOutput = false;
            response.Charset = "UTF-8";
            response.ContentEncoding = System.Text.Encoding.UTF8;
            response.AppendHeader("Content-disposition", "attachment; filename=" + fileName);
            response.Write(excelXml);
            response.Flush();
            response.End();
            HttpContext.Current.Response.End();
        }
        */
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }


        [Route("Api/pdf/GetFile/{reportAddress}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetFile(string reportAddress)
        {
            try
            {
                var reportAddressAscii =  Base64Decode(reportAddress);
                string filename = "convert2pdfpro.pdf";
                var company = "pretendcompany";
                var path = $"{System.Web.HttpContext.Current.Server.MapPath($"~/pdfs/{DateTime.Now.Year}")}";
                path = path.Replace("/", @"\");
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                PdfFiles pdfFiles = new PdfFiles();
                filename = await pdfFiles.CreatePdf(reportAddressAscii);

                // Large
                //string reportUrl = "http://goengagexopa.cloudapp.net/Reports/10640/report.html";

                // Small
                //"http://goengagexopa.cloudapp.net/Reports/10650/report.html"

                //FileResult fileResult = pdfFiles.CreatePdf(reportUrl);

                // ($"/pdfs/{DateTime.Now.Year}")
                //string var = 
                HttpResponseMessage objResponse = Request.CreateResponse(HttpStatusCode.OK);
                var mappedPath = HttpContext.Current.Server.MapPath($"~/pdfs/{DateTime.Now.Year}/" + filename);

                objResponse.Content = new StreamContent(new FileStream(mappedPath, FileMode.Open, FileAccess.Read));
                objResponse.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                objResponse.Content.Headers.ContentDisposition.FileName = filename;
                return objResponse;
            }catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.Created, ex.Message);
            }
        }

        /*
        [HttpGet]
        public HttpResponseMessage Get(string fileName)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            string fileLocation = HttpContext.Current.Server.MapPath("~/Downloads" + fileName);

            if (!File.Exists(fileLocation))
            {
                throw new HttpResponseException(HttpStatusCode.OK);
            }

            Stream fileStream = File.Open(fileLocation, FileMode.Open);
            result.Content = new StreamContent(fileStream);

            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ms-excel");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            return result;
        }
        */
    }
}
