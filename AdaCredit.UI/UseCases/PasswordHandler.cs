using static BCrypt.Net.BCrypt;

namespace AdaCredit.UI.UseCases
{
    class PasswordHandler
    {
        public static string Encrypt(string password) 
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            return hashedPassword;
        }

        public static bool Decrypt(string password, string hashedPassword)
        {
            bool passwordsMatch = Verify(password, hashedPassword);
            return passwordsMatch;
        }
    }
}