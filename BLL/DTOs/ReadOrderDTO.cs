using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
    public class ReadOrderDTO
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public string ProductName { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
    }
}
