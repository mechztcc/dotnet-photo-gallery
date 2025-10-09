using AppApi.Data;
using AppApi.Modules.Users.Models;
using AppApi.Modules.Users.DTOs;
using Microsoft.EntityFrameworkCore;
using AppApi.Exceptions;
using Microsoft.AspNetCore.Identity;


namespace AppApi.Modules.Users.Services
{
    public class CreateUserService
    {
        private readonly AppDbContext _context;

        private readonly PasswordHasher<User> _passwordHasher;

        public CreateUserService(AppDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }


        public async Task<User> Execute(CreateUserDTO payload)
        {

            bool userExists = await _context.Users.AnyAsync(u => u.Email == payload.Email);

            if (userExists)
            {
                throw new GlobalError("Email already in use.", 409);
            }

            var user = new User
            {
                Name = payload.Name,
                Email = payload.Email,
            };

            user.Password = _passwordHasher.HashPassword(user, payload.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }

}
