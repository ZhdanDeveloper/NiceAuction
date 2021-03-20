using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public ICollection<int> CategoriesIds { get; set; }
        public ICollection<int> BidsIds { get; set; }



    }
}
