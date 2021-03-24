using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace BLL.DTOs
{
    public class CreateProductDTO
    {  
        [Required, MaxLength(50), MinLength(5)]
        public string Name { get; set; }
        [MaxLength(300)]
        public string Description { get; set; }
        [NotMapped, Required]
        public IFormFile Photo { get; set; }
        [Required,Range(3,99999999)]     
        public decimal Price { get; set; }
        public string UserId { get; set; }
        [Required]
        public ICollection<int> CategoriesIds { get; set; }

    }
}
