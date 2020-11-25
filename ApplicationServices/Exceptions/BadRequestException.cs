using System;

namespace ApplicationServices.Exceptions
{
    public class BadRequestException: Exception
    {
        public string Code { get; set; }
        public BadRequestException()
        { }

        public BadRequestException(string message, string code="BadRequest")
            : base(message)
        {
            Code = code;
        }

        public BadRequestException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
