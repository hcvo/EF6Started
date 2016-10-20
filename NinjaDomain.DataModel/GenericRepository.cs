using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Delete(int id)
        {
            var entity = Find(id);
            _dbSet.Remove(entity);
        }

        public T Find(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
