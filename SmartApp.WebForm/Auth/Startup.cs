using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartup(typeof(SmartApp.WebForm.Auth.Startup))]

namespace SmartApp.WebForm.Auth
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions 
            {
                LoginPath = new PathString("/Login.aspx"),
                LogoutPath = new PathString("/Logout.aspx"),
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                ExpireTimeSpan = TimeSpan.FromMinutes(30),
                SlidingExpiration = true,
            });
        }
    }
}
