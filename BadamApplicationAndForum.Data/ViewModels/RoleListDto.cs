using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace BadamApplicationAndForum.Data.ViewModels
{
    public class RoleListDto
    {
        public string Id { get; set; }
        [Display(Name = "نام نقش")]
        public string Name { get; set; }
        [Display(Name = "توضیحات")]
        public string Description { get; set; }
    }
}
