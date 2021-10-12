using System.ComponentModel.DataAnnotations;


namespace BadamApplicationAndForum.Data.ViewModels
{
    public class RegulationEditViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "مقدار اجباری")]
        [Display(Name = "نام آیین نامه")]
        public string Name { get; set; }
        [Display(Name = "لینک فایل pdf")]
        [Required(ErrorMessage = "مقدار اجباری")]
        public string Link { get; set; }
    }
}
