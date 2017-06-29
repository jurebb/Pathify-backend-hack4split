using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Core1.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TheWorld.pwd
{
    //public class MyPasswordHasher : IPasswordHasher<WorldUser>
    public class MyPasswordHasher<TUser> : PasswordHasher<TUser> where TUser : class
    {

        public override string HashPassword(TUser user, string password)
        {
            using (SHA256 mySHA256 = SHA256.Create())
            {
                byte[] hash = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(password.ToString()));

                StringBuilder hashSB = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    hashSB.Append(hash[i].ToString("x2"));
                }
                return hashSB.ToString();
            }
        }

        public override PasswordVerificationResult VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword)
        {
            if (hashedPassword == HashPassword(user, providedPassword))
                return PasswordVerificationResult.Success;
            else
                return PasswordVerificationResult.Failed;
        }
    }

}
