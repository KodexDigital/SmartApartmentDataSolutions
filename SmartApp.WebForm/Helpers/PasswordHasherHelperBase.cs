using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SmartApp.WebForm.Helpers
{
    public abstract class PasswordHasherHelperBase : PasswordHasher
    {
        public abstract string PasswordHash(string password);
        public abstract PasswordVerificationResult VerifyPassword(string email, string providedPassword);
    }

    public class PasswordHasherHelper : PasswordHasherHelperBase
    {
        public override string PasswordHash(string password)
            => HashPassword(password);

        public override PasswordVerificationResult VerifyPassword(string email, string providedPassword)
        {
            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);
            var user = userManager.FindByEmail(email);

            return VerifyHashedPassword(user.PasswordHash, providedPassword);
        }
    }
}