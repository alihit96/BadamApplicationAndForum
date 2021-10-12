using System.ComponentModel.DataAnnotations;


namespace BadamApplicationAndForum.Data.ViewModels
{
    public class RegulationListViewModel
    {
        public int Id { get; set; }
        [Display(Name= "نام آیین نامه")]
        public string Name { get; set; }
        [Display(Name= "لینک فایل pdf")]
        public string Link { get; set; }
    }
}
