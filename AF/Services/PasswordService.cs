using Microsoft.AspNetCore.Identity;

namespace AF.Services
{
    public class PasswordService
    {
        private readonly PasswordHasher<object> _passwordHasher = new PasswordHasher<object>();

        // Hashes the password
        public string HashPassword(string password)
        {
            var user = new object(); // Pass a dummy object
            return _passwordHasher.HashPassword(user, password);
        }

        // Verifies the provided password with the stored hashed password
        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var user = new object(); // Pass a dummy object
            var result = _passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}