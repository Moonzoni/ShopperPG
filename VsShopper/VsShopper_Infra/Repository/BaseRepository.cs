using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VsShopper_Infra.Entity;
using VsShopper_Infra.Interface;

namespace VsShopper_Infra.Repository
{

   public class BaseRepository<T>:IBaseRepository<T> where T: class
    {
        protected readonly VsShopperContext _vsShopperContext;

        public BaseRepository(VsShopperContext vsShopperContext)
        {
            _vsShopperContext = vsShopperContext;
        }

        public virtual T Add(T entity)
        {
            var newEntity = _vsShopperContext.Set<T>().Add(entity);
            _vsShopperContext.SaveChanges();
            return newEntity.Entity;
        }

        public virtual void Delete (int id)
        {
            var data = Get(id);
            if ( data != null)
            {
                _vsShopperContext.Set<T>().Remove(data);
                _vsShopperContext.SaveChanges();
            }  
        }
        public virtual T Get (int id)
        {
            return _vsShopperContext.Set<T>().Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            
            return _vsShopperContext.Set<T>().ToList();
        }

        public virtual T Update (T entity)
        {
            _vsShopperContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _vsShopperContext.SaveChanges();
            return entity;
        }

    } 

    
}
