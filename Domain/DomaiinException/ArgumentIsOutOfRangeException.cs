using System;

namespace Domain.DomaiinException
{
    public class ArgumentIsOutOfRangeException : Exception
    {
        public string Code { get; set; }
        public ArgumentIsOutOfRangeException()
        { }

        public ArgumentIsOutOfRangeException(string message, string code = "ArgumentOutOfRange")
            : base(message)
        {
            Code = code;
        }

        public ArgumentIsOutOfRangeException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
