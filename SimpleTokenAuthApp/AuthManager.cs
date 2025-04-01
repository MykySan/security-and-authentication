namespace SimpleTokenAuthApp
{
    public class AuthenticationManager
    {
        private readonly List<User> users = new List<User>();
        private readonly TokenManager tokenManager = new TokenManager();
        public void Register()
        {
            Console.WriteLine("Enter username:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();

            if (users.Exists(u => u.Username == username))
            {
                Console.WriteLine("User already exists");
                return;
            }
            users.Add(new User { Username = username, Password = password });
            Console.WriteLine("Registration successful.");
        }

        public void Login()
        {
            Console.WriteLine("Enter you username:");
            string? username = Console.ReadLine();
            Console.WriteLine("Enter your password");
            string? password = Console.ReadLine();

            var user = users.Find(u => u.Username == username && u.Password == password);
            if (user == null)
            {
                Console.WriteLine("Invalid username or password");
                return;
            }
            user.Token = tokenManager.GenerateToken(username);
            Console.WriteLine($"Login successful. Your token: {user.Token}");
        }

        public User GetUserByToken(string token)
        {
            return users.Find(u => u.Token == token);
        }
    }
}