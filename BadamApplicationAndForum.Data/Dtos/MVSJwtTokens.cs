using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.Dtos
{
    public class MVSJwtTokens
    {
        public const string AuthScheme = "Identity.Application" + "," + JwtBearerDefaults.AuthenticationScheme;
    }
}
