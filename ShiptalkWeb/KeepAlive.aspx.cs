using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using System.Text;
using System.Threading;

using System.IO;

namespace ShiptalkWeb
{
    public partial class KeepAlive : System.Web.UI.Page
    {
        private static void WriteLogEntry(string additionalInfo, HttpContext context)
        {
            try
            {
                //UploadDirectoryPam
                string logFilePath = context.Server.MapPath("~/_errorlog.txt");
               

                using (Stream s = File.Open(logFilePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    s.Seek(0, SeekOrigin.End);
                    using (StreamWriter writer = new StreamWriter(s))
                    {
                        writer.WriteLine("**************************************************");
                        string message =   "test idle timeout bimal";
                        writer.WriteLine(message);
                        writer.WriteLine("**************************************************");
                        writer.Flush();
                    }
                }
            }
            catch (System.UnauthorizedAccessException unAuthEx)
            {
                System.Diagnostics.Debug.WriteLine("NOT ENOUGH PERMISSIONS TO WRITE TO LOG FILE: " + unAuthEx.ToString());
            }
            catch (Exception x)
            {
                System.Diagnostics.Debug.WriteLine("WriteLogEntry: " + x.ToString());
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            HttpResponse response = HttpContext.Current.Response;

            //set to no-cache
            Response.AppendHeader("Cache-Control", "no-cache");

            //sammit just write the OK 
            response.Write("OK");


            //WriteLogEntry("TEST", HttpContext.Current);
            //response.Equals("OK");
            //response.ContentType = "text/xml";

            return;
        }
    }
}
