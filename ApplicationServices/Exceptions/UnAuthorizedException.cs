using System;

namespace ApplicationServices.Exceptions
{
    public class UnAuthorizedException: Exception
    {
        public string Code { get; set; }
        public UnAuthorizedException()
        { }

        public UnAuthorizedException(string message, string code= "UnAuthorized")
            : base(message)
        {
            Code = code;
        }

        public UnAuthorizedException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}