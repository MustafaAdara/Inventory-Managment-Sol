using System.Linq.Expressions;

namespace MOFA.StockManagement.Domain.Patterns.Repositories
{
    public static class QueryableExtension
    {
        public static IOrderedQueryable<TSource>? OrderBy<TSource>(this IQueryable<TSource> query, string propertyName)
        {
            var entityType = typeof(TSource);

            // Create x=>x.PropName
            var propertyInfo = entityType.GetProperty(propertyName);
            if (propertyInfo?.DeclaringType != entityType)
            {
                propertyInfo = propertyInfo?.DeclaringType?.GetProperty(propertyName);
            }

            // If we try to order by a property that does not exist in the object return the list
            if (propertyInfo == null)
            {
                return (IOrderedQueryable<TSource>)query;
            }

            var arg = Expression.Parameter(entityType, "x");
            var property = Expression.MakeMemberAccess(arg, propertyInfo);
            var selector = Expression.Lambda(property, new ParameterExpression[] { arg });

            // Get System.Linq.Queryable.OrderBy() method.
            var method = typeof(Queryable)
                .GetMethods()
                .Where(m => m.Name == "OrderBy" && m.IsGenericMethodDefinition) // ensure selecting the right overload
                .Single(m => m.GetParameters().ToList().Count == 2);

            //The linq's OrderBy<TSource, TKey> has two generic types, which provided here
            var genericMethod = method.MakeGenericMethod(entityType, propertyInfo.PropertyType);

            /* Call query.OrderBy(selector), with query and selector: x=> x.PropName
              Note that we pass the selector as Expression to the method and we don't compile it.
              By doing so EF can extract "order by" columns and generate SQL for it. */
            return (IOrderedQueryable<TSource>?)genericMethod.Invoke(genericMethod, new object[] { query, selector });
        }
        public static IOrderedQueryable<TSource>? OrderByDescending<TSource>(this IQueryable<TSource> query, string propertyName)
        {
            var entityType = typeof(TSource);

            // Create x=>x.PropName
            var propertyInfo = entityType.GetProperty(propertyName);
            if (propertyInfo?.DeclaringType != entityType)
            {
                propertyInfo = propertyInfo?.DeclaringType?.GetProperty(propertyName);
            }

            // If we try to order by a property that does not exist in the object return the list
            if (propertyInfo == null)
            {
                return (IOrderedQueryable<TSource>)query;
            }

            var arg = Expression.Parameter(entityType, "x");
            var property = Expression.MakeMemberAccess(arg, propertyInfo);
            var selector = Expression.Lambda(property, new ParameterExpression[] { arg });

            // Get System.Linq.Queryable.OrderBy() method.
            var method = typeof(Queryable)
                .GetMethods()
                .Where(m => m.Name == "OrderByDescending" && m.IsGenericMethodDefinition) // ensure selecting the right overload
                .Single(m => m.GetParameters().ToList().Count == 2);

            //The linq's OrderBy<TSource, TKey> has two generic types, which provided here
            var genericMethod = method.MakeGenericMethod(entityType, propertyInfo.PropertyType);

            /* Call query.OrderBy(selector), with query and selector: x=> x.PropName
              Note that we pass the selector as Expression to the method and we don't compile it.
              By doing so EF can extract "order by" columns and generate SQL for it. */
            return (IOrderedQueryable<TSource>?)genericMethod.Invoke(genericMethod, new object[] { query, selector });
        }
    }
}
