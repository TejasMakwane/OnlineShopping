using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace OnlineShoppingStore1.Repository
{
    public interface IRepository<T_Entity> where T_Entity:class
    {
        IEnumerable<T_Entity> GetProduct();
        IEnumerable<T_Entity> GetAllRecords();
        IQueryable<T_Entity> GetAllRecordsIQueryable();
        int GetAllRecordCount();
        void Add(T_Entity entity);
        void Update(T_Entity entity);
        void UpadteByWhereCaluse(Expression<Func<T_Entity, bool>> wherePredict, Action<T_Entity> ForEachPredict);
        T_Entity GetFirstorDefault(int recordId);
        void Remove(T_Entity entity);
        void RemovebyWhereCaluse(Expression<Func<T_Entity, bool>> wherePredict);
        void RemoveRangeBywhereCaluse(Expression<Func<T_Entity, bool>> wherePredict);
        void InactiveAndDeleteMarkByWhereCaluse(Expression<Func<T_Entity, bool>> wherePredict, Action<T_Entity> ForEachPredict);
        T_Entity GetFirstorDefaultByParameter(Expression<Func<T_Entity, bool>> wherePredict);
        IEnumerable<T_Entity> GetListParameter(Expression<Func<T_Entity, bool>> wherePredict);
        IEnumerable<T_Entity> GetResultBySqlprocedure(string query, params object[] parameters);
        IEnumerable<T_Entity> GetRecordsToShow(int PageNo, int PageSize, int CurrentPage, Expression<Func<T_Entity, bool>> wherePredict, Expression<Func<T_Entity, int>> orderByPredict);
    }
}