using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Turshia.Services
{
   
        public class HashService : IHashService
        {

            public string Hash(string stringToHash)

            {



                stringToHash = stringToHash + "myAppSalt131638712#";



                using (var sha256 = SHA256.Create())

                {

                    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(stringToHash));



                    var hash = BitConverter.ToString(hashedBytes).Replace("-", "");

                    return hash;



                }

            }

        }
    }

