using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace BadamApplicationAndForum.Data.ViewModels
{
    public class ProfessorDetailViewModel
    {
        public int Id { get; set; }
        [Display(Name = "نام و نام خانوادگی")]
        public string Name { get; set; }
        [Display(Name = "آدرس تصویر")]
        public string ImageUrl { get; set; }
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        [Display(Name = "تلفن")]
        public string Phone { get; set; }
        [Display(Name = "سطح علمی")]
        public string EducationLevel { get; set; }
        [Display(Name = "رشته تحصیلی")]
        public string Field { get; set; }
        [Display(Name = "گروه")]
        public string Group { get; set; }
        [Display(Name = "لینک پروفایل Scopus")]
        public string ScopusLink { get; set; }
        [Display(Name = "لینک پروفایل Scholar")]
        public string ScholarLink { get; set; }
        [Display(Name = "لینک پروفایل Orcid")]
        public string OrcidLink { get; set; }
        [Display(Name = "لینک پروفایل Publons")]
        public string PublonsLink { get; set; }
        [Display(Name = "لینک پروفایل WOS")]
        public string WosLink { get; set; }
        [Display(Name = "نام استاندارد")]
        public string NormalName { get; set; }
        [Display(Name = "علاقه مندی ها")]
        public string ProfessorInterests { get; set; }
        [Display(Name = "مسئولیت سازمانی")]
        public string OragnizationalObligation { get; set; }
        [Display(Name= "درباره استاد")]
        public string About { get; set; }

    }
}
