using System;
using System.Security.Cryptography;

namespace SecureDataApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var admin = new User { Username = "AdminUser", Role = "Admin"};
            var user = new User { Username = "BasicUser", Role = "User"};

            var storage = new SecureStorage();
            byte[] encryptionKey;
            byte[] initializationVector;

            using (var aes = Aes.Create())
            {
                aes.GenerateKey();
                aes.GenerateIV();
                encryptionKey = aes.Key;
                initializationVector = aes.IV;

                storage.StoreData("Sensitive Information", aes.Key, aes.IV);
            }

            try
            {
                string adminData = storage.RetrieveData(admin, encryptionKey, initializationVector);
                Console.WriteLine($"Admin Access: {adminData}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Admin Error: {ex.Message}");
            }

            try
            {
                string userData = storage.RetrieveData(user, encryptionKey, initializationVector);
                Console.WriteLine($"User Access: {userData}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"User Error: {ex.Message}");
            }
        }
    }
}