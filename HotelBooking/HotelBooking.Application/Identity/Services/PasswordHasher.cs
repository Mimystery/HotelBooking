using HotelBooking.Application.Identity.Authentication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Application.Identity.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Generate(string password)
        {
            var hashPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(password);
            return hashPassword;
        }

        public bool Verify(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
        }
    }
}
