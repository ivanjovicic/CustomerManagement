using System;
using System.Web;
using System.Web.UI;

namespace CustomerManagement.WebApp
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();

            System.Diagnostics.Debug.WriteLine("UNHANDLED ERROR: " + exception);

            HttpContext.Current.Items["LastError"] = exception;
         
            Server.ClearError();
            Response.Redirect("~/Error.aspx");
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}