using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadamApplicationAndForum.Data.ViewModels
{
    public class ProfessorCreateViewModel
    {
        [Required(ErrorMessage ="نباید خالی باشد")]
        [Display(Name= "نام و نام خانوادگی")]
        public string Name { get; set; }
        [Display(Name= "تصویر")]
        public IFormFile ImageUrl { get; set; }
        [Required(ErrorMessage = "نباید خالی باشد")]
        [Display(Name= "ایمیل")]
        public string Email { get; set; }
        [Required(ErrorMessage = "نباید خالی باشد")]
        [Display(Name = "تلفن")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "نباید خالی باشد")]
        [Display(Name= "سطح علمی")]
        public string EducationLevel { get; set; }
        [Required(ErrorMessage = "نباید خالی باشد")]
        [Display(Name= "رشته تحصیلی")]
        public string Field { get; set; }
        [Required(ErrorMessage = "نباید خالی باشد")]
        [Display(Name= "گروه")]
        public string Group { get; set; }
        [Display(Name= "لینک پروفایل Scopus")]
        public string ScopusLink { get; set; }
        [Display(Name= "لینک پروفایل Scholar")]
        public string ScholarLink { get; set; }
        [Display(Name= "لینک پروفایل Orcid")]
        public string OrcidLink { get; set; }
        [Display(Name= "لینک پروفایل Publons")]
        public string PublonsLink { get; set; }
        [Display(Name= "لینک پروفایل WOS")]
        public string WosLink { get; set; }
        [Display(Name= "نام استاندارد")]
        public string NormalName { get; set; }
        [Display(Name= "علاقه مندی ها")]
        public string ProfessorInterests { get; set; }
        [Display(Name = "مسئولیت سازمانی")]
        public string OragnizationalObligation { get; set; } = "ندارد";
        public string About { get; set; }

    }
}
