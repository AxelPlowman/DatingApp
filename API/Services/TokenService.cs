
using System.IdentityModel.Tokens.Jwt; // is a nuget package
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {

        private readonly SymmetricSecurityKey _key; // getting SymmetricSecurityKey from System.IdentityModel.Tokens.Jwt
        public TokenService(IConfiguration config)
        {
            // encode config["TokenKey] into byte array and set it as a new SymmetricSecurityKey as _key
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>() {
                // claim says that the users username is the same as the NameId inside the token
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName) // just one claim for now
            };

            // this is what we use to sign the token with
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7), // typically this would be much shorter (the length of the token validity)
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}