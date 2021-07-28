using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;

namespace SmartApp.WebForm
{
    public partial class UserRegistration : System.Web.UI.Page
    {
        protected void BtnRegister_Click(object sender, EventArgs e)
        {
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);
            
            var user = new IdentityUser() 
            { 
                UserName = txtEmail.Text, 
                Email = txtEmail.Text, 
                PhoneNumber = txtPhoneNumber.Text, 
                PasswordHash = txtPassword.Text 
            };
            IdentityResult result = manager.Create(user, txtPassword.Text);
            if (result.Succeeded)
                lblStatusMessage.Text = string.Format("User {0} was created successfully!", user.UserName);
            else
                lblStatusMessage.Text = result.Errors.FirstOrDefault();
        }
    }
}