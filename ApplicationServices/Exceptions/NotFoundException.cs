using System;

namespace ApplicationServices.Exceptions
{
    public class NotFoundException: Exception
    {
        public string Code { get; set; }
        public NotFoundException()
        { }

        public NotFoundException(string message, string code="Notfound")
            : base(message)
        {
            Code = code;
        }

        public NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
