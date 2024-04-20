using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        T Get(string id);

        void Create(T instance);

        void Update(T instance);

        void Delete(T instance);

        void Delete(string id);

        IEnumerable<T> GetAll();

        void SaveChanges();
    }
}
