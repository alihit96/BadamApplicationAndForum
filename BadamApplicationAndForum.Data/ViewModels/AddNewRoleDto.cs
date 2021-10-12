using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace BadamApplicationAndForum.Data.ViewModels
{
    public class AddNewRoleDto
    {
        [Display(Name = "نام نقش")]
        [Required(ErrorMessage = "فیلد اجباری است")]
        public string Name { get; set; }
    }
}
