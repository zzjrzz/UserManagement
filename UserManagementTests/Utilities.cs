using System.Collections.Generic;
using UserManagement.Models;

namespace UserManagementTests
{
    public static class Utilities
    {
        public static void InitializeDbForTests(ApiDbContext db)
        {
            db.Users.AddRange(GetSeedingUsers());
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(ApiDbContext db)
        {
            db.Users.RemoveRange(db.Users);
            InitializeDbForTests(db);
        }

        private static IEnumerable<User> GetSeedingUsers()
        {
            return new List<User>()
            {
                new User {Name = "Jeremy", Email = "jeremy@example.com", Salary = 2000, Expenses = 0},
                new User {Name = "Jess", Email = "jess@example.com", Salary = 2000, Expenses = 0},
                new User {Name = "David", Email = "david@example.com", Salary = 2000, Expenses = 0},
                new User {Name = "Ariel", Email = "ariel@example.com", Salary = 2000, Expenses = 0},
            };
        }

    }
}