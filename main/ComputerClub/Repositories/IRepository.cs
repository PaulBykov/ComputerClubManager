using Microsoft.EntityFrameworkCore;
using System;

namespace ComputerClub.Repositories
{
    interface IRepository<T> : IDisposable where T : class
    {
        T Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Save();
    }
}
