using OnlineShoppingStore1.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace OnlineShoppingStore1.Repository
{
    public class GenericRepository<T_Entity> : IRepository<T_Entity> where T_Entity : class
    {
        DbSet<T_Entity> _dbSet;

        private OnlineShoppingEntities _DBEntity;













        public GenericRepository(OnlineShoppingEntities DBEntity)
        {
            _DBEntity = DBEntity;
            _dbSet = _DBEntity.Set<T_Entity>();
        }


        public IEnumerable<T_Entity> GetProduct()
        {
            return _dbSet.ToList();
        }



      



        public void Add(T_Entity entity)
        {
            _dbSet.Add(entity);
            _DBEntity.SaveChanges();
        }

        public int GetAllRecordCount()
        {
            return _dbSet.Count();
        }

        public IEnumerable<T_Entity> GetAllRecords()
        {
            return _dbSet.ToList();
        }

        public IQueryable<T_Entity> GetAllRecordsIQueryable()
        {
            return _dbSet;
        }

        public T_Entity GetFirstorDefault(int recordId)
        {
            return _dbSet.Find(recordId);
        }

        public T_Entity GetFirstorDefaultByParameter(Expression<Func<T_Entity, bool>> wherePredict)
        {
            return _dbSet.Where(wherePredict).FirstOrDefault();
        }

        public IEnumerable<T_Entity> GetListParameter(Expression<Func<T_Entity, bool>> wherePredict)
        {
            return _dbSet.Where(wherePredict).ToList();

        }

        

        public IEnumerable<T_Entity> GetResultBySqlprocedure(string query, params object[] parameters)
        {
            if(parameters !=null)
            {
                return _DBEntity.Database.SqlQuery<T_Entity>(query, parameters).ToList();
            }
            else
            {
                return _DBEntity.Database.SqlQuery<T_Entity>(query).ToList();
            }
        }

        public void InactiveAndDeleteMarkByWhereCaluse(Expression<Func<T_Entity, bool>> wherePredict, Action<T_Entity> ForEachPredict)
        {
            _dbSet.Where(wherePredict).ToList().ForEach(ForEachPredict);
        }

        public void Remove(T_Entity entity)
        {
            if (_DBEntity.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);
            _dbSet.Remove(entity);
        }

        public void RemovebyWhereCaluse(Expression<Func<T_Entity, bool>> wherePredict)
        {
            T_Entity entity = _dbSet.Where(wherePredict).FirstOrDefault();
            Remove(entity);
        }

        public void RemoveRangeBywhereCaluse(Expression<Func<T_Entity, bool>> wherePredict)
        {
            List<T_Entity> entity = _dbSet.Where(wherePredict).ToList();
            foreach(var ent in entity)
            {
                Remove(ent);
            }
        }

        public void UpadteByWhereCaluse(Expression<Func<T_Entity, bool>> wherePredict, Action<T_Entity> ForEachPredict)
        {
            _dbSet.Where(wherePredict).ToList().ForEach(ForEachPredict);
        }

        public void Update(T_Entity entity)
        {
            _dbSet.Attach(entity);
            _DBEntity.Entry(entity).State = EntityState.Modified;
            _DBEntity.SaveChanges();
        }

        public IEnumerable<T_Entity> GetRecordsToShow(int PageNo, int PageSize, int CurrentPage, Expression<Func<T_Entity, bool>> wherePredict, Expression<Func<T_Entity, int>> orderByPredict)
        {
           if(wherePredict !=null)
            {
                return _dbSet.OrderBy(orderByPredict).Where(wherePredict).ToList();
            }
            else
            {
                return _dbSet.OrderBy(orderByPredict).ToList();
            }
        }
    }

}