using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BLL.Exceptions
{
    public class AuctionException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public AuctionException()
        { }

        public AuctionException(string message, HttpStatusCode StatusCode)
            : base(message)
        {  this.StatusCode = StatusCode; }

        public AuctionException(string message, Exception innerException)
            : base(message, innerException)
        { }

    }
}
