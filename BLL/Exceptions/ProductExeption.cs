using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Exceptions
{
    class ProductException : Exception
    {
        public ProductException()
        { }

        public ProductException(string message)
            : base(message)
        { }

        public ProductException(string message, Exception innerException)
            : base(message, innerException)
        { }

    }
}
