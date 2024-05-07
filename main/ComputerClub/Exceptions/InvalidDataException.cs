using System;


namespace ComputerClub.Exceptions
{
    class InvalidDataException : ComputerClubException
    {
        public InvalidDataException() : base()
        {
        }

        public InvalidDataException(string message = "InvalidDataException has accureted!") : base(message)
        {
        }

        public InvalidDataException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
