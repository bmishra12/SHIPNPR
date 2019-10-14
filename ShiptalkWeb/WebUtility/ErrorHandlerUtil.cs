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
    public class ErrorHandlerUtil
    {





        /// <summary> 
        /// Handles instances where an error occurs in the application. 
        /// </summary> 
        /// <param name="ex">The exception to report.</param> 
        /// <param name="context">The context in which the error occured.</param> 
        /// <param name="additionalInfo">Additional info that might help in diagnosing the problem.</param> 
        /// <remarks> 
        /// This method will never throw an exception. 
        /// It can be used safely anywhere in code. 
        /// </remarks> 
        public static void HandleError(Exception ex, HttpContext context, string additionalInfo)
        {
            if (ex == null || context == null)
            {
                return;
            }
           

            try
            {
                WriteLogEntry(ex, context, additionalInfo);
            }
            catch (Exception exLog)
            {
                System.Diagnostics.Debug.WriteLine("Unable to write error message to log file. Exception: " + exLog.ToString());
            }
        }


        /// <summary> 
        /// Handles instances where an error occurs in the application. 
        /// </summary> 
        /// <param name="ex">The exception to report.</param> 
        /// <param name="context">The context in which the error occured.</param> 
        /// <remarks> 
        /// This method will never throw an exception. 
        /// It can be used safely anywhere in code. 
        /// </remarks> 
        public static void HandleError(Exception ex, HttpContext context)
        {
            if (ex == null || context == null)
            {
                return;
            }

            string additionalInfo = string.Empty;
        

            try
            {
                WriteLogEntry(ex, context, additionalInfo);
            }
            catch (Exception exLog)
            {
                System.Diagnostics.Debug.WriteLine("Unable to write error message to log file. Exception: " + exLog.ToString());
            }
        }

       

        private static void WriteLogEntry(Exception ex, HttpContext context, string additionalInfo)
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
                        string message = CreateLogMessage(ex, context, additionalInfo);
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


        private static string CreateLogMessage(Exception ex, HttpContext context, string additionalInfo)
        {
            string userName = "Doesnt matter";
            string userBrowser = context.Request.UserAgent;
            string userIP = context.Request.UserHostAddress;
            string url = context.Request.Url.ToString();
            string physicalPath = context.Request.PhysicalPath;
            string time = DateTime.Now.ToString();

            string fmtString = "User: {0}\nUser IP: {1}\nUser Agent: {2}\nUrl: {3}\nPhysical Path: {4}\nDate: {5}\nAdditional Info: {6}\nError: {7}";
            return string.Format(fmtString, userName, userIP, userBrowser, url, physicalPath, time, additionalInfo, ex.ToString());
        }


    }
}
