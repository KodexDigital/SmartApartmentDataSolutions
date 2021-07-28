using Microsoft.AspNet.Identity;
using System;
using System.Web;

namespace SmartApp.WebForm
{
    public partial class AllUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (User.Identity.IsAuthenticated)
                    lblUserInfo.Text = string.Format($"Hello {User.Identity.GetUserName()}, Welcome!");
                else
                {
                    Response.Redirect("Login", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
        }

        protected void LblLknbtnLogout_Click(object sender, EventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut();
            Response.Redirect("/", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}