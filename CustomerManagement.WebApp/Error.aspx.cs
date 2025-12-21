using System;

namespace CustomerManagement.WebApp
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Optionally, you can read HttpContext.Current.Items["LastError"]
            // and log or display additional info for debugging (not to end users).
        }
    }
}
