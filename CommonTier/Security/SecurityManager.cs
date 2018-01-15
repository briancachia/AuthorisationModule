using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CommonTier.Security
{
    public class SecurityManager
    {

        public SecurityManager() { }


        /// <summary>
        /// Use this method to generate a hash based on the provided parameters
        /// </summary>
        /// <param name="username">Username for the user</param>
        /// <param name="password">Password the user has inserted</param>
        public string GeneratePassword(string username, string password)
        {
            //prepare instance of SHA512Managed for hashing the bytes
            SHA512Managed shaHashManager = new SHA512Managed();

            //Prepare bytes to hash
            string toHash = username + password;
            byte[] toHashBytes = Encoding.UTF8.GetBytes(toHash);

            //hash the bytes now
            byte[] hashed = shaHashManager.ComputeHash(toHashBytes);

            //return newly generated password as base64 string
            return Convert.ToBase64String(hashed);
        }
    }
}
