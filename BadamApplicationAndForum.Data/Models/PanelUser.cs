using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.Models
{
    public class PanelUser : IdentityUser
    {
        public IEnumerable<DirectMessage> DirectMessages { get; set; }
    }
}
