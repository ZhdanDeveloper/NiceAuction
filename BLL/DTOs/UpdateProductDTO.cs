using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BLL.DTOs
{
    public class UpdateProductDTO
    {
        
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
        [Required, Range(0, 99999999)]
        public decimal Price { get; set; }
        public string UserId { get; set; }
    }
}
