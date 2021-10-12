using BadamApplicationAndForum.Data.Dtos;
using BadamApplicationAndForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.Interfaces
{
    public interface IApplicationUser
    {
        Task CreateUser(ApplicationUser user);

        bool ExistsUser(string username);

        AuthenticationResult AuthenticateUser(LoginDto loginDto);

        ApplicationUser FindUserById(string Id);
        ApplicationUser FindUserByFullName(string FullName);
        IEnumerable<ApplicationUser> GetUsers();

    }
}
