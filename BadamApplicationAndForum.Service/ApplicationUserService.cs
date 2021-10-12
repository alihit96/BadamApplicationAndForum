using BadamApplicationAndForum.Data.Contexts;
using BadamApplicationAndForum.Data.Dtos;
using BadamApplicationAndForum.Data.Interfaces;
using BadamApplicationAndForum.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Service
{
    public class ApplicationUserService : IApplicationUser
    {
        private readonly ApplicationDatabaseContext _context;
        private readonly IConfiguration _appSettings;

        public ApplicationUserService(ApplicationDatabaseContext context, IConfiguration configuration)
        {
            _context = context;
            _appSettings = configuration;
        }

        public AuthenticationResult AuthenticateUser(LoginDto loginDto)
        {
            var user = _context.ApplicationUsers.FirstOrDefault(p => p.UserName == loginDto.UserName && p.Password == loginDto.Password);
            if (user == null) return null;
            var token = generateJwtToken(user);
            user.PusheId = loginDto.PusheId;
            _context.ApplicationUsers.Update(user);
            _context.SaveChanges();

            return new AuthenticationResult(user, token);
        }

        public async Task CreateUser(ApplicationUser user)
        { 
           _context.ApplicationUsers.Add(user);
           await _context.SaveChangesAsync();
        }

        public bool ExistsUser(string username)
        {
            var user = _context.ApplicationUsers.FirstOrDefault(p => p.UserName == username);
            if (user != null)
            {
                return true;
            }
            return false;
        }

        public ApplicationUser FindUserByFullName(string FullName)
        {
            return _context.ApplicationUsers.Where(u => u.FullName == FullName).FirstOrDefault();
        }

        public ApplicationUser FindUserById(string Id)
        {
            var user = _context.ApplicationUsers
                .Include(u=>u.DirectMessages)
                .ThenInclude(u=>u.PanelUser)
                .Include(u=>u.DirectMessages)
                .ThenInclude(d=>d.MessageReplies)
                .FirstOrDefault(p => p.Id.ToString() == Id);
            return user;
        }

        public IEnumerable<ApplicationUser> GetUsers()
        {
            return _context.ApplicationUsers;
        }

        private string generateJwtToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSettings["JwtConfig:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                { 
                    new Claim("id", user.Id.ToString()),
                    new Claim("userName", user.UserName),
                    new Claim("email", user.Email),
                }),
                Expires = DateTime.Now.AddMonths(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _appSettings["JwtConfig:audience"],
                Issuer = _appSettings["JwtConfig:issuer"]
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
