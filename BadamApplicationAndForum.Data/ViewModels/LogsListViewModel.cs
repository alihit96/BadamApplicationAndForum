using System;
using System.ComponentModel.DataAnnotations;

namespace BadamApplicationAndForum.Data.Models
{
    public class LogsListViewModel
    {
        [Display(Name ="شناسه")]
        public string Id { get; set; }
        [Display(Name ="عملیات")]
        public string Description { get; set; }
    }
}
