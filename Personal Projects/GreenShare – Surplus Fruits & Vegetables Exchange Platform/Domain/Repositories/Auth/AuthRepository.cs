using Domain.Data;
using Domain.Interfaces.Auth;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Auth
{
    public class AuthRepository:IAuthRepository
    {

        private readonly AppDbContext _context;

        public AuthRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> EmailExistsAsync(string email)
            => await _context.Users.AnyAsync(x => x.Email == email);

        public async Task AddUserAsync(User user)
            => await _context.Users.AddAsync(user);

        public async Task<User?> GetByEmailAsync(string email)
            => await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

        public async Task<User?> GetByIdAsync(Guid id)
            => await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<User?> GetByConfirmationTokenAsync(string token)
            => await _context.Users.FirstOrDefaultAsync(x => x.EmailConfirmationToken == token);

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();
    }
}
