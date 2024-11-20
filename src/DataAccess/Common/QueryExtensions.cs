namespace thinkschool.OnlineExam.DataAccess.Common;
public static class QueryExtensions
{
    // Build Filter Expression
    public static IQueryable<TEntity> BuildFilterExpression<TEntity, TFilter>(this IQueryable<TEntity> query, TFilter filterObj)
    {

        var filterProps = filterObj!.GetType().GetProperties()
            .Where(p => p.GetValue(filterObj) != null && p.PropertyType.IsGenericType);

        ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "p");
        Expression? finalExpression = null;

        foreach (var filterProp in filterProps)
        {
            var prop = typeof(TEntity).GetProperty(filterProp.Name);
            if (prop == null) continue;

            var fieldFilters = filterProp.GetValue(filterObj) as dynamic;
            if (fieldFilters == null || fieldFilters!.Filters == null) continue;

            LogicalOperator combineWith = fieldFilters!.CombineWith;
            Expression? innerExpression = null;

            foreach (var filter in fieldFilters.Filters)
            {
                Expression comparisonExpression = BuildComparisonExp(parameter, prop, filter.Value, filter.ComparisonOperator);
                innerExpression = innerExpression!.BuildCombineExp(comparisonExpression, combineWith);
            }

            finalExpression = finalExpression!.BuildCombineExp(innerExpression!, LogicalOperator.And);
        }

        if (finalExpression != null)
        {
            var lambda = Expression.Lambda<Func<TEntity, bool>>(finalExpression, parameter);
            query = query.Where(lambda);
        }

        return query;
    }

    private static Expression BuildComparisonExp(ParameterExpression parameter, PropertyInfo prop, object value, ComparisonOperator comparisonOperator)
    {
        var member = Expression.Property(parameter, prop);
        Expression constant = Expression.Constant(value);
        if (Nullable.GetUnderlyingType(prop.PropertyType) != null)
        {
            constant = Expression.Convert(constant, prop.PropertyType);
        }

        return comparisonOperator switch
        {
            ComparisonOperator.Equals => Expression.Equal(member, constant),
            ComparisonOperator.NotEquals => Expression.NotEqual(member, constant),
            ComparisonOperator.GreaterThan => Expression.GreaterThan(member, constant),
            ComparisonOperator.LessThan => Expression.LessThan(member, constant),
            ComparisonOperator.GreaterThanOrEqual => Expression.GreaterThanOrEqual(member, constant),
            ComparisonOperator.LessThanOrEqual => Expression.LessThanOrEqual(member, constant),
            ComparisonOperator.Contains => Expression.Call(member, typeof(string).GetMethod("Contains", new[] { typeof(string) }), constant),
            _ => throw new NotSupportedException($"Comparison operator '{comparisonOperator}' is not supported")
        };
    }

    private static Expression BuildCombineExp(this Expression left, Expression right, LogicalOperator combineWith)
    {
        if (left == null) return right;
        if (right == null) return left;

        return combineWith switch
        {
            LogicalOperator.And => Expression.AndAlso(left, right),
            LogicalOperator.Or => Expression.OrElse(left, right),
            _ => throw new NotSupportedException($"Logical operator '{combineWith}' is not supported")
        };
    }

    //Build Sorting Expression
    public static IQueryable<TEntity> BuildSortingExp<TEntity>(this IQueryable<TEntity> query, string sortBy, bool isDescending = false)
    {
        var propInfo = typeof(TEntity).GetProperties().FirstOrDefault(p => p.Name.Equals(sortBy, StringComparison.InvariantCultureIgnoreCase));
        if (propInfo == null)
        {
            return query;
        }

        ParameterExpression p = Expression.Parameter(typeof(TEntity), "p");
        var memberAccess = Expression.Property(p, propInfo);
        var lmbda = Expression.Lambda<Func<TEntity, object>>(Expression.Convert(memberAccess, typeof(object)), p);
        if (isDescending)
        {
            query = query.OrderByDescending(lmbda);
        }
        else
        {
            query = query.OrderBy(lmbda);
        }
        return query;
    }

    //Build Include Expression
    public static IQueryable<TEntity> BuildIncludeExp<TEntity>(this IQueryable<TEntity> query, Type includes) where TEntity : class
    {
        var propToInclude = includes.GetProperties().Where(p => p.GetGetMethod()!.IsVirtual).ToList();
        foreach (var prop in propToInclude)
        {
            query = query.Include(prop.Name);
        }
        return query;
    }
    //Build Pagging Expression
    public static (IQueryable<TEntity> Query, int TotalCount) BuildPagingExp<TEntity>(this IQueryable<TEntity> query, int pageNum, int pageSize)
    {
        if (pageNum <= 0)
        {
            return (query, query.Count());
        }
        return (query.Skip((pageNum - 1) * pageSize).Take(pageSize), query.Count());
    }
}
