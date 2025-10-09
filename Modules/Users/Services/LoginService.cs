

using AppApi.Data;
using AppApi.Exceptions;
using AppApi.Modules.Users.DTOs;
using AppApi.Modules.Users.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AppApi.Modules.Users.Services
{
    public class LoginService
    {

        private readonly AppDbContext _context;

        private readonly PasswordHasher<User> _passwordHasher;

        private readonly IConfiguration _config;


        public LoginService(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
            _config = config;
        }


        public async Task<object> Execute(LoginDTO payload)
        {
            User user = await _context.Users.SingleOrDefaultAsync(u => u.Email == payload.Email);
            if (user == null)
            {
                throw new GlobalError("Invalid credencials", 401);
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, payload.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new GlobalError("Invalid credencials", 401);
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name)
            }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_config["Jwt:ExpiresInMinutes"])),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);


            return new
            {
                email = user.Email,
                token = tokenString,
            };

        }

    }
}