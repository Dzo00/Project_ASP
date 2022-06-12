using Project_ASP.DataAccess;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using BCrypt.Net;

namespace Project_ASP.Api.Core
{
    public class JwtManager
    {
        private readonly ProjectContext _context;
        private readonly JwtSettings _settings;

        public JwtManager(ProjectContext context, JwtSettings settings)
        {
            _settings = settings;
            _context = context;
        }

        public string GenerateToken(string email, string password)
        {
            var user = _context.Users.Include(x => x.Role).ThenInclude(x=>x.Permissions).FirstOrDefault(x => x.Email == email);

            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }

            var valid = BCrypt.Net.BCrypt.Verify(password, user.Password);

            if(!valid)
            {
                throw new UnauthorizedAccessException();
            }


            var actor = new JwtUser
            {
                Id = user.Id,
                PermissionIds = user.Role.Permissions.Select(x => x.PermissionId).ToList(),
                Identity = user.Email,
                Email = user.Email
            };

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iss, _settings.Issuer),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64, _settings.Issuer),
                new Claim("UserId", actor.Id.ToString(), ClaimValueTypes.String, _settings.Issuer),
                new Claim("Permissions", JsonConvert.SerializeObject(actor.PermissionIds)),
                new Claim("Email", user.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: "Any",
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(_settings.Minutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
