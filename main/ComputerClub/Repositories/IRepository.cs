using System.Collections.Generic;

namespace ComputerClub.Repositories
{
    interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Add(T item);
        void Update(T item);
        void Delete(T item);
    }
}
