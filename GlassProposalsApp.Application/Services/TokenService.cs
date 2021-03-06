﻿using GlassProposalsApp.API.Interfaces;
using GlassProposalsApp.Data.Enumerations;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GlassProposalsApp.API.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Generate(string email, int securityLevel, Guid userId)
        {
            var identity = GetIdentity(email, securityLevel, userId);

            var jwt = new JwtSecurityToken(
                    issuer: _configuration.GetSection("Authentication:Issuer").Value,
                    audience: _configuration.GetSection("Authentication:Audience").Value,
                    notBefore: DateTime.UtcNow,
                    claims: identity.Claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(int.Parse(_configuration.GetSection("Authentication:LifeTime").Value))),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection("Authentication:Key").Value)),
                                                                                                                            SecurityAlgorithms.HmacSha256));

            var encodedJwt = "Bearer " + new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }

        private static ClaimsIdentity GetIdentity(string email, int securityLevel, Guid userId)
        {
            var claims = new List<Claim>
                {
                   new Claim("Email", email),
                   new Claim("UserId", userId.ToString()),
                   new Claim("SecurityLevel", securityLevel.ToString()),
                };

            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}
