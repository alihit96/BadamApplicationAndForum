using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BadamApplicationAndForum.Data.Dtos
{
    public class LoginDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string PusheId { get; set; }
    }
}
