using System;


namespace ComputerClub.Exceptions
{
    public class CRUDException : ComputerClubException
    {
        public CRUDException() : base()
        {
        }

        public CRUDException(string message = "CRUDException has occurred!") : base(message)
        {

        }

        public CRUDException(string message, Exception innerException) : base(message, innerException)
        {

        }

    }
}
