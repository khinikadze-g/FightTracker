using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Core.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set;} = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;

        private User() { }

        private  User(string firstName, string lastName, string email, string password)
        {
            Id= Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = password;
        }

        public static User Create(string firstName, string lastName, string email, string password)
        {
            return new User(firstName, lastName, email, password);
        }
    }
}
