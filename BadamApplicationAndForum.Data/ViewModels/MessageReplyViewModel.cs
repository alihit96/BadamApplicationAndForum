using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.ViewModels
{
    public class MessageReplyViewModel
    {
        public int Id { get; set; }
        public string Reply { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; }
        public string PanelUserName { get; set; }
        public string ApplicationUserName { get; set; }
    }
}
