using System;


namespace ComputerClub.Repositories
{
    interface IChangesNotifier<T> where T : class
    {
        public event EventHandler DatabaseChanges;
    }
}
