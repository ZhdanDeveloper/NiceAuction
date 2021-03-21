using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BLL.DTOs
{
    public class ProductDTO
    {
     
        public int Id { get; set; }
        [Required, MaxLength(50), MinLength(5)]
        public string Name { get; set; }
        [MaxLength(300)]
        public string Description { get; set; }
        [NotMapped, Required]
        public IFormFile Photo { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string UserId { get; set; }
        public ICollection<int> CategoriesIds { get; set; }




    }
}
