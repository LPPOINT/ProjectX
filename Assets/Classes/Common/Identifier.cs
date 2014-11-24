using System;
using System.Linq.Expressions;
using Random = UnityEngine.Random;

namespace Assets.Classes.Common
{
    public static class Identifier
    {
        public static string DefineString()
        {
            return Guid.NewGuid().ToString("N");
        }

        public static int DefineInt()
        {
            return Random.Range(int.MinValue, int.MaxValue);
        }

        public static string DefineStaticString<T>(Expression<Func<T>> memberExpression)
        {
            var expressionBody = (MemberExpression)memberExpression.Body;
            return expressionBody.Member.Name;
        }

    }
}
