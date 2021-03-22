using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTOs
{
    public class CreateCategoryDTO
    {
        [Required, MinLength(3), MaxLength(30)]
        public string Name { get; set; }
    }
}
