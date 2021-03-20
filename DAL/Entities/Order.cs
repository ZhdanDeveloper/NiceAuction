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
        [Column(TypeName = "decimal(8,2)"), Required]
        public decimal OrderValue { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
        public int AuctionId { get; set; }
        public User User { get; set; }
        public Product Auction { get; set; }
    }
}
