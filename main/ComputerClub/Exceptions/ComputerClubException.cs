using System;

namespace ComputerClub.Exceptions
{
    public abstract class ComputerClubException : Exception
    {
        public ComputerClubException() : base()
        {
        }

        public ComputerClubException(string message = "ComputerClubException has accureted!") : base(message)
        {
        }

        public ComputerClubException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
