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
            //var hasher = new PasswordHasherHelper();

            var user = new IdentityUser() 
            { 
                UserName = txtEmail.Text, 
                Email = txtEmail.Text, 
                PhoneNumber = txtPhoneNumber.Text, 
                PasswordHash =  txtPassword.Text 
            };
            IdentityResult result = manager.Create(user, txtPassword.Text);
            if (result.Succeeded)
                lblStatusMessage.Text = string.Format("User {0} was created successfully!", user.UserName);
            else
                lblStatusMessage.Text = result.Errors.FirstOrDefault();
        }


        //private class PasswordHasherHelper : Microsoft.AspNet.Identity.PasswordHasher// .PasswordHasher<IdentityUser>
        //{
        //    public string PasswordHash(string password)
        //    {
        //        //var user = new AppUser
        //        //{
        //        //    PasswordHash = password
        //        //};
        //        return HashPassword(password);
        //    }

        //    public PasswordVerificationResult VerifyPassword(string email, string providedPassword)
        //    {
        //        //var user = new AppUser
        //        //{
        //        //    PasswordHash = hashedPassword
        //        //};
        //        var userStore = new UserStore<IdentityUser>();
        //        var userManager = new UserManager<IdentityUser>(userStore);
        //        var user = userManager.FindByEmail(email);

        //        return VerifyHashedPassword(user.PasswordHash, providedPassword);
        //    }
        //}
    }
}