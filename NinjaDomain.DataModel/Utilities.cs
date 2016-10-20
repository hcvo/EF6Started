using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Data
{
    public static class Utilities
    {
        public static Expression<Func<T, bool>> BuildLamdaForFindByKey<T>(int id)
        {
            var item = Expression.Parameter(typeof(T), "entity");
            var prop = Expression.Property(item, typeof(T).Name + "Id");
            var value = Expression.Constant(id);
            var equal = Expression.Equal(prop, value);
            var lamda = Expression.Lambda<Func<T, bool>>(equal, item);

            return lamda;
        }
    }
}
