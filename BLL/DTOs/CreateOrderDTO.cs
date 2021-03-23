using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTOs
{
    public class CreateOrderDTO
    {
        [Range(1,10)]
        public int Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
    }
}
