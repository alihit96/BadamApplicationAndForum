using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Helpers
{
    public enum Roles
    {
        [Display(Name = "مدیر")]
        Admin,
        [Display(Name = "اپراتور")]
        Operator
    }
}
