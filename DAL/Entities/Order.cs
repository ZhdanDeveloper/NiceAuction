using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Range(1, 10)]
        public int Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
