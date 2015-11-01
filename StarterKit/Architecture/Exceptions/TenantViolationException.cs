using Mailgun.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace StarterKit.Architecture.Exceptions
{
    public class TenantViolationException : Exception
    {
        private string logFilePath = @"C:\sasLog\TenantViolationError.txt";

        public TenantViolationException() : base (App_GlobalResources.lang.importantException)
        {
            // this need to be adressed immediatly as it should never occur
            using (StreamWriter writer = new StreamWriter(this.logFilePath, true))
            {
                writer.WriteLine("Message :" + this.Message + "<br/>" + Environment.NewLine + "StackTrace :" + this.StackTrace +
                   "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
            }
        }
    }
}