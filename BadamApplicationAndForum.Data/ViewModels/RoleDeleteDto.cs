using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace BadamApplicationAndForum.Data.ViewModels
{
    public class RoleDeleteDto
    {
        [Display(Name = "شناسه")]
        public string Id { get; set; }
        [Display(Name = "نام نقش")]
        public string RoleName { get; set; }
    }
}
