using System.Collections.Generic;
using System.Linq;
using Authorization_JWT_Token.Model;

namespace Authorization_JWT_Token.Repositories
{
    public static class UserRepository
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>();
            users.Add(new() { Id = 1, Username = "Jinx", Password = "jinxpassword", Role = "admin" });
            users.Add(new() { Id = 2, Username = "Jayce", Password = "jaycepassword", Role = "manager" });
            users.Add(new() { Id = 3, Username = "Vi", Password = "vipassword", Role = "employee" });

            return users.Where(u => u.Username.ToLower() == username.ToLower() && u.Password == password).FirstOrDefault();
        }
    }
}