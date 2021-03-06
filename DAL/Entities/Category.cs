using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required, MinLength(3), MaxLength(30)]
        public string Name { get; set; }
    }
}
