using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
    public class AuctionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhotoPath { get; set; }
        public decimal StartBid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string UserId { get; set; }
    }
}
