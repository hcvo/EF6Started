using Microsoft.EntityFrameworkCore;
using SharedKernel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NinjaDomain.DataModel
{
    /// <note>
    /// Limitation or repo pattern is losing the richness of the dbcontext
    /// </note>
    /// <typeparam name="T">Name of an entity, a concrete class</typeparam>
    public class GenericRepository<T> : IRepository<T> where T :class
    {
        private DbContext _context;
        private DbSet<T> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public IEnumerable<T> All()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public IEnumerable<T> AllInclude(params Expression<Func<T, object>>[] includeProperties)
        {
            return GetAllIncluding(includeProperties).ToList();
        }

        public T FindByKey(int id)
        {
            var lamda = Utilities.BuildLamdaForFindByKey<T>(id);

            return _dbSet.AsNoTracking().SingleOrDefault(lamda);
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }

        public IEnumerable<T> FindByInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = GetAllIncluding(includeProperties);

            return query.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            var entity = FindByKey(id);
            _dbSet.Remove(entity);
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        private IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = _dbSet.AsNoTracking();

            return includeProperties.Aggregate(queryable, (current, includeProperty) => current.Include(includeProperty)); 
        }
    }
}
