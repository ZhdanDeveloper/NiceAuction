using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
    public class ReadProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public ICollection<int> CategoriesIds { get; set; }

    }
}
