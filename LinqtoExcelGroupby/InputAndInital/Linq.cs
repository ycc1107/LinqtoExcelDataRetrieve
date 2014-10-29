using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace LinqtoExcelGroupby.InputAndInital
{
    class Linq
    {
        public Expression<Func<T,R>> Expr<T,R>(Expression<Func<T,R>> f)
        {
            return f;
        }

        public Func<T, R> Func<T, R>(Func<T, R> f)
        {
            return f;
        }
    }
}
