using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ProjectTest.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectTest.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<UserEntity> _userManager;
        public TokenService(UserManager<UserEntity> userManager)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.TokenKey));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }
        public async Task<string> CreateToken(UserEntity User)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, User.Id.ToString())
            };
            var roles = await _userManager.GetRolesAsync(User);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                SigningCredentials = creds,
                Expires = DateTime.Now.AddDays(7),
                Subject = new ClaimsIdentity(claims),
                NotBefore = DateTime.Now
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
