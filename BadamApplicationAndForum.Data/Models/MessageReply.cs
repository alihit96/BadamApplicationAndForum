using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.Models
{
    public class MessageReply
    {
        public int Id { get; set; }
        public string Reply { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; }
        public virtual DirectMessage DirectMessage { get; set; }
        public PanelUser PanelUser { get; set; }
    }
}
