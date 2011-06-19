using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace AjModel
{
    public static class ExpressionExtensions
    {
        public static PropertyInfo ToPropertyInfo<T, R>(this Expression<Func<T, R>> expr)
        {
            return (PropertyInfo)((MemberExpression)((LambdaExpression)expr).Body).Member;
        }
    }
}
