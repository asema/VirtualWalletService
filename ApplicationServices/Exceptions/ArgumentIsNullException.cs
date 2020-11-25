using System;

namespace ApplicationServices.Exceptions
{
    public class ArgumentIsNullException : Exception
    {
        public string Code { get; set; }
        public ArgumentIsNullException()
        { }

        public ArgumentIsNullException(string message, string code="ArgumentNull")
            : base(message)
        {
            Code = code;
        }

        public ArgumentIsNullException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
