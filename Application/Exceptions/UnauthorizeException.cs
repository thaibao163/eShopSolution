using System.Globalization;

namespace Application.Exceptions
{
    public class UnauthorizeException : Exception
    {
        public UnauthorizeException() : base()
        {
        }

        public UnauthorizeException(string message) : base(message)
        {
        }

        public UnauthorizeException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}