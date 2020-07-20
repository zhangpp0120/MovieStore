﻿using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using MovieStore.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MovieStore.Infrastructure.Services
{
    // Pbkdf2    microsoft
    // Aarogn                 3rd party
    // BCrypt    3rd party     hashmethod
    public class CryptoService : ICryptoService
    {
        public string GenreateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }

        public string HashPassword(string password, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                                                     password: password,
                                                                     salt: Convert.FromBase64String(salt),
                                                                     prf: KeyDerivationPrf.HMACSHA512,
                                                                     iterationCount: 10000,
                                                                     numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}
