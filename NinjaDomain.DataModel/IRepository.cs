using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NinjaDomain.DataModel
{
    public interface IRepository<T>
    {
        IEnumerable<T> All();
        T FindByKey(int id);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        IEnumerable<T> AllInclude(params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> FindByInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);        
    }
}
