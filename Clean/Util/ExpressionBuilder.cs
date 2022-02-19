using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Clean.Util
{
    public static class ExpressionBuilder
    {
        /*
         * Not safe for EF
         * Reuturn error:
         * System.Exception: The LINQ expression 'DbSet<Company>()
        .Where(c => True && c.CompanyId == 1)' could not be translated.
        Either rewrite the query in a form that can be translated,
        or switch to client evaluation explicitly by inserting a 
        call to 'AsEnumerable', 'AsAsyncEnumerable', 'ToList', or 'ToListAsync'
         */

        //public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> where1, Expression<Func<T, bool>> where2)
        //{
        //    InvocationExpression invocationExpression = Expression.Invoke(where2,
        //         where1.Parameters.Cast<Expression>());
        //    return Expression.Lambda<Func<T, bool>>(Expression.OrElse(where1.Body,
        //         invocationExpression), where1.Parameters);
        //}
        //public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> where1, Expression<Func<T, bool>> where2)
        //{
        //    InvocationExpression invocationExpression = Expression.Invoke(where2,
        //         where2.Parameters.Cast<Expression>());
        //    //return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(where1.Body,
        //    //     invocationExpression), where1.Parameters);
        //    return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(where1.Body,
        //          where2.Body), where1.Parameters);
        //}

        /*From
         https://stackoverflow.com/questions/457316/combining-two-expressions-expressionfunct-bool/457328#457328 the last piece of code of Marc Gravel
         */
        public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceExpressionVisitor(expr1.Parameters[0], parameter);
            var left = leftVisitor.Visit(expr1.Body);

            var rightVisitor = new ReplaceExpressionVisitor(expr2.Parameters[0], parameter);
            var right = rightVisitor.Visit(expr2.Body);

            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(left, right), parameter); ;
        }



        private class ReplaceExpressionVisitor
            : ExpressionVisitor
        {
            private readonly Expression _oldValue;
            private readonly Expression _newValue;

            public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
            {
                _oldValue = oldValue;
                _newValue = newValue;
            }

            public override Expression Visit(Expression node)
            {
                if (node == _oldValue)
                    return _newValue;
                return base.Visit(node);
            }
        }
    }
}
