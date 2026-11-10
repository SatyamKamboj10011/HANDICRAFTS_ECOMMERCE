using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Form_Loading.Models
{
    public class User
    {
        public string Name { get; }
        public string Email { get; }
        internal string Salt { get; }
        internal string Hash { get; }

        internal User(string name, string email, string salt, string hash)
        {
            Name = name;
            Email = email;
            Salt = salt;
            Hash = hash;
        }
    }

    /// <summary>
    /// Registers and signs in customers. Passwords are never stored:
    /// each one is salted and hashed with PBKDF2. Accounts are saved
    /// to the user's AppData folder so they survive a restart.
    /// </summary>
    public static class UserStore
    {
        public const int MinPasswordLength = 6;
        private const int Iterations = 10000;

        private static readonly string filePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "HandcraftHeaven", "users.dat");

        private static readonly List<User> users = Load();

        public static User Current { get; private set; }

        public static event EventHandler CurrentChanged;

        public static bool IsSignedIn
        {
            get { return Current != null; }
        }

        /// <summary>Signs in with a name or email. Returns false if the details are wrong.</summary>
        public static bool SignIn(string nameOrEmail, string password)
        {
            string key = (nameOrEmail ?? "").Trim();
            User user = users.FirstOrDefault(u =>
                string.Equals(u.Name, key, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(u.Email, key, StringComparison.OrdinalIgnoreCase));

            if (user == null || !Verify(password ?? "", user.Salt, user.Hash))
                return false;

            SetCurrent(user);
            return true;
        }

        public static void SignOut()
        {
            SetCurrent(null);
        }

        /// <summary>Validates and creates an account. Returns an error message, or null on success.</summary>
        public static string Register(string name, string email, string password)
        {
            name = (name ?? "").Trim();
            email = (email ?? "").Trim();
            password = password ?? "";

            if (name.Length < 2)
                return "Please enter your name.";
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return "Please enter a valid email address.";
            if (password.Length < MinPasswordLength)
                return "Password must be at least " + MinPasswordLength + " characters.";
            if (users.Any(u => string.Equals(u.Email, email, StringComparison.OrdinalIgnoreCase)))
                return "An account with this email already exists.";
            if (users.Any(u => string.Equals(u.Name, name, StringComparison.OrdinalIgnoreCase)))
                return "That name is already taken. Try adding your surname.";

            string salt = NewSalt();
            User user = new User(name, email, salt, HashPassword(password, salt));
            users.Add(user);
            Save();
            SetCurrent(user);
            return null;
        }

        private static void SetCurrent(User user)
        {
            Current = user;
            CurrentChanged?.Invoke(null, EventArgs.Empty);
        }

        // ---- Password hashing -------------------------------------------

        private static string NewSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }

        private static string HashPassword(string password, string salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), Iterations))
                return Convert.ToBase64String(pbkdf2.GetBytes(32));
        }

        private static bool Verify(string password, string salt, string expectedHash)
        {
            byte[] a = Convert.FromBase64String(HashPassword(password, salt));
            byte[] b = Convert.FromBase64String(expectedHash);
            // Constant-time comparison
            int diff = a.Length ^ b.Length;
            for (int i = 0; i < Math.Min(a.Length, b.Length); i++)
                diff |= a[i] ^ b[i];
            return diff == 0;
        }

        // ---- Storage ------------------------------------------------------

        private static List<User> Load()
        {
            var list = new List<User>();
            try
            {
                if (File.Exists(filePath))
                {
                    foreach (string line in File.ReadAllLines(filePath))
                    {
                        string[] parts = line.Split('\t');
                        if (parts.Length == 4)
                            list.Add(new User(parts[0], parts[1], parts[2], parts[3]));
                    }
                }
            }
            catch (IOException) { /* start with no saved accounts */ }
            catch (UnauthorizedAccessException) { }

            // Demo account so the shop can be tried straight away
            if (!list.Any(u => u.Name == "demo"))
            {
                string salt = NewSalt();
                list.Add(new User("demo", "demo@handcraftheaven.nz", salt, HashPassword("demo123", salt)));
            }
            return list;
        }

        private static void Save()
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                File.WriteAllLines(filePath, users
                    .Where(u => u.Name != "demo")
                    .Select(u => string.Join("\t", u.Name, u.Email, u.Salt, u.Hash)));
            }
            catch (IOException) { /* account still works for this session */ }
            catch (UnauthorizedAccessException) { }
        }
    }
}
