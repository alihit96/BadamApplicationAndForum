using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.ViewModels
{
    public class MessageReplyCreateViewModel
    {
        public string Reply { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; }
        public int DirecMessageId { get; set; }
        public string ApplicationUserName { get; set; }
    }
}
