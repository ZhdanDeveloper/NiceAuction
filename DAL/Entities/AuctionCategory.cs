using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Entities
{
   
    public class AuctionCategory
    {
        [Key]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int AuctionId { get; set; }
        public Category Category { get; set; }
        public Auction Auction { get; set; }
    }
}
