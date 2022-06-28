﻿using Microsoft.IdentityModel.Tokens;
using Project_two.DbContexts;
using Project_two.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Project_two.IJwtAuthentication
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly string key;
        AirlineContext db = new AirlineContext();

        public JwtAuthenticationManager(string key)
        {
            this.key = key;
        }

        public string Authenticate(string AdminEmail, string AdminPasskey)
        {
            if(!db.Admin.Any(u => u.adminEmailId == AdminEmail && u.adminPasskey == AdminPasskey))
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenkey = Encoding.UTF8.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, AdminEmail)
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenkey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);


        }
    }
}
