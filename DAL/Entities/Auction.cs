using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entities
{
    public class Auction
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50), MinLength(5)]
        public string Name { get; set; }
        [MaxLength(300)]
        public string Description { get; set; }
        public string PhotoPath { get; set; }
        [Column(TypeName = "decimal(8,2)"), Required]
        public decimal StartBid { get; set; }
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime FinishDate { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<AuctionCategory> AuctionCategories { get; set; }
    }
}
