using System.ComponentModel.DataAnnotations;


namespace BadamApplicationAndForum.Data.ViewModels
{
    public class RegulationDetailsViewModel
    {
        public int Id { get; set; }
        [Display(Name= "نام")]
        public string Name { get; set; }
        [Display(Name= "لینک فایل pdf")]
        public string Link { get; set; }
    }
}
