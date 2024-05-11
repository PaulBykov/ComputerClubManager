using System;


namespace ComputerClub.Exceptions
{
    public class SessionException : ComputerClubException
    {
        public SessionException() : base()
        {
        }

        public SessionException(string message = "SessionException has occurred!") : base(message)
        {

        }

        public SessionException(string message, Exception innerException) : base(message, innerException)
        {

        }

    }
}
