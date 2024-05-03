using System;

namespace ComputerClub.Exceptions
{
    public class AuthException : ComputerClubException
    {
        public AuthException() : base()
        {
        }

        public AuthException(string message = "AuthException has accureted!") : base(message)
        {
        }

        public AuthException(string message, Exception innerException) : base(message, innerException)
        {
        }


    }
}
