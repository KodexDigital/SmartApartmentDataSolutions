using Domain.Entities;
using Microsoft.AspNet.Identity;

namespace Application.Helper
{
    public class PasswordHasherHelper : PasswordHasher
    {
        public string PasswordHash(string password)
            => HashPassword(password);
        public PasswordVerificationResult VerifyPasswordHashed(string hashedPassword, string providedPassword) 
            => VerifyHashedPassword(hashedPassword, providedPassword);
    }
}
