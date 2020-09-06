using OnlineShoppingStore1.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShoppingStore1.Repository
{
    public class GenericUnitOfWork:IDisposable
    {
     

        private OnlineShoppingEntities DBEntity = new OnlineShoppingEntities();

        public IRepository<T_EntityType> GetRepositoryInstance<T_EntityType>() where T_EntityType : class
        {
            return new GenericRepository<T_EntityType>(DBEntity);
        }

        public void SaveChanges()
        {
            DBEntity.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
          if(! this.dispose)
            {
                if(disposing)
                {
                    DBEntity.Dispose();
                }
            }
            this.dispose = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool dispose = false;
    }
}