using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.ViewModels
{
    public class UserDirectMessagesViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; }
        public string ApplicationUserName { get; set; }
    }
}
