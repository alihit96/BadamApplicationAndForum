using BadamApplicationAndForum.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BadamApplicationAndForum.Data.Dtos
{
    public class AuthenticationResult
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public IEnumerable<DirectMessage> DirectMessages { get; set; }


        public AuthenticationResult(ApplicationUser user, string token)
        {
            Id = user.Id.ToString();
            FullName = user.FullName;
            UserName = user.UserName;
            UserType = user.UserType;
            Email = user.Email;
            Token = token;
            DirectMessages = user.DirectMessages;
        }
    }
}
