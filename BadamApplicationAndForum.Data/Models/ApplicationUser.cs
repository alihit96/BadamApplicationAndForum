using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BadamApplicationAndForum.Data.Models
{
    public class ApplicationUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PusheId { get; set; }
        public virtual IEnumerable<DirectMessage> DirectMessages { get; set; }
    }
}
