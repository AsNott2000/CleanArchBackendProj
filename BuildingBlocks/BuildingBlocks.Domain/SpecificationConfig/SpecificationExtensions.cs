namespace BuildingBlocks.Domain.SpecificationConfig;

public static class SpecificationExtensions
{
    public static IQueryable<TEntity> Specify<TEntity>(this IQueryable<TEntity> query, Specification<TEntity> specification)
    {
        var queryable = query;
        //Eğer bir filtre (criteria) varsa, bu koşulu sorguya uygula. Örn: “User aktif mi?” veya “Fiyat > 100” gibi dinamik bir filtre.
        if (specification.Criteria is not null)
        {
            queryable = queryable.Where(specification.Criteria);
        }
        //Eğer bir sıralama istiyorsan (OrderBy), onu uygula. Sonrasında, varsa ikinci sıralama (ThenBy/ThenByDescending) uygula.
        if (specification.OrderByExpression is not null)
        {
            var orderedQuery = queryable.OrderBy(specification.OrderByExpression);
            if (specification.ThenOrderByExpression is not null) queryable = orderedQuery.ThenBy(specification.ThenOrderByExpression);
            else if (specification.ThenOrderByDescendingExpression is not null) queryable = orderedQuery.ThenByDescending(specification.ThenOrderByDescendingExpression);
            else queryable = orderedQuery;
        }
        //Eğer “tersten” (descending) sıralama istiyorsan, onu uygula.
        if (specification.OrderByDescendingExpression is not null)
        {
            var orderedQuery = queryable.OrderBy(specification.OrderByDescendingExpression);
            if (specification.ThenOrderByExpression is not null) queryable = orderedQuery.ThenBy(specification.ThenOrderByExpression);
            else if (specification.ThenOrderByDescendingExpression is not null) queryable = orderedQuery.ThenByDescending(specification.ThenOrderByDescendingExpression);
            else queryable = orderedQuery;
        }
        //Bu kısım, tekrar ters sıralama yapmak için. (Biraz redundant gibi duruyor, belki kodun başka yerinden kalma.)
        if (specification.OrderByDescendingExpression is not null)
        {
            queryable = queryable.OrderByDescending(specification.OrderByDescendingExpression);
        }

        /*
        Eğer sayfalama isteniyorsa (paging), sadece ilgili sayfanın verilerini getir
        if (specification.IsPagingEnabled)
        {
            queryable = queryable.Skip(specification.Skip).Take(specification.Take);
        }
        */

        return queryable;
    }
}