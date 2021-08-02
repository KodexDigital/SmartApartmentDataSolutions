using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;

namespace SmartApp.WebForm
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (User.Identity.IsAuthenticated)
                    lblStatusText.Text = string.Format("Hello {0}!!", User.Identity.GetUserName());
                else
                    lblStatusText.Visible = false;
            }
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);
            var user = userManager.Find(txtEmail.Text, txtPassword.Text);
            if (user != null)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                Context.GetOwinContext().Authentication.SignIn(new Microsoft.Owin.Security.AuthenticationProperties()
                { IsPersistent = false }, identity);

                Response.Redirect("AllUsers", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                lblStatusText.Text = "Invalid username or password.";
                lblStatusText.Visible = true;
            }
        }

        protected void SignOut(object sender, EventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut();
            Response.Redirect("Default");
            Response.Cookies.Clear();
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}