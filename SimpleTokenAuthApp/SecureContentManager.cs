using System;

namespace SimpleTokenAuthApp
{
    public class SecureContentManager
    {
        private readonly AuthenticationManager authManager;

        public SecureContentManager(AuthenticationManager authManager)
        {
            this.authManager = authManager;
        }

        public void AccessSecureContent()
        {
            Console.Write("Enter your token:");
            string? token = Console.ReadLine();

            var user = authManager.GetUserByToken(token);
            if (user != null)
            {
                Console.WriteLine($"Access granter to secure content for user: {user.Username}");
            }
            else
            {
                Console.WriteLine("Access denied. Invalid token.");
            }
        }
    }
}