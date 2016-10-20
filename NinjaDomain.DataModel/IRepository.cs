using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaDomain.DataModel
{
    public interface IRepository<T>
    {
        IEnumerable<T> All();
        T Find(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);        
    }
}
