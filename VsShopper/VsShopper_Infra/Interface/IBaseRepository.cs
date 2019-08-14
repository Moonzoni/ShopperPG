using System;
using System.Collections.Generic;
using System.Text;

namespace VsShopper_Infra.Interface
{
    public interface IBaseRepository<T> where T: class
    {
        T Add(T entity);
        T Update(T entity);
        void Delete(int id);
        T Get(int id);
        IEnumerable<T> GetAll();
    }
}
