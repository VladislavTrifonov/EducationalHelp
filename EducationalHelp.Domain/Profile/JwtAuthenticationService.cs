using EducationalHelp.Core.Entities;
using EducationalHelp.Data;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace EducationalHelp.Services.Profile
{
    public class JwtAuthenticationService : BaseAuthenticationService
    {
        public static string Issuer = "EducationalHelp-Auth_S";
        public static string Audience = "EducationalHelp-Auth_C";
        public static string Key = "sbfldjknjkjfnsdb15342n^%#*O&";
        public static int Lifetime = 2 * 24 * 60; // два дня
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }

        public JwtAuthenticationService(UserService userService) : base(userService) { }

        public string CreateAccessToken(UserCredentials credentials)
        {
            if (!CheckCredentials(credentials))
            {
                throw new ArgumentException(nameof(credentials));
            }

            var user = _userService.GetUserByName(credentials.Pseudonym);

            var jwt = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                notBefore: DateTime.UtcNow,
                claims: GetUserClaims(user),
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(Lifetime)),
                signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }


    }
}
