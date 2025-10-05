using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBooking.Domain.Enums;

namespace HotelBooking.Domain.Models
{
    public class UserModel
    {
        public UserModel(Guid userId, string userName, string passwordHash, string firstName, string lastName, string email, Role role)
        {
            UserId = userId;
            UserName = userName;
            PasswordHash = passwordHash;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Role = role;
        }
        public Guid UserId { get; }
        public string UserName { get; }
        public string PasswordHash { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public Role Role { get; }

    }
}
