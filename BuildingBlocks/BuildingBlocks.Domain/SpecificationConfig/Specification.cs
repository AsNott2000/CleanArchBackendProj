using System.Linq.Expressions;
using BuildingBlocks.Domain.Enums;

namespace BuildingBlocks.Domain.SpecificationConfig;

public abstract class Specification<TEntity>
{
    protected Specification()
    {
    }
    
    protected Specification(Expression<Func<TEntity, bool>> criteria) => Criteria = criteria;

    //Bu expression, asıl filtreyi temsil eder.
    //Yani, u => u.IsActive && u.Role == "admin" gibi, query’ye Where olarak uygulanacak şart.
    public Expression<Func<TEntity, bool>>? Criteria { get; set; }
    //Entity Framework’de navigation property (ilişkili tablo) çekmek için.
    public List<string> IncludeStrings { get; set; } = [];
    public int Take { get; private set; }
    public int Skip { get; private set; }
    public bool IsPagingEnabled { get; private set; }
    //Entity Framework’de navigation property (ilişkili tablo) çekmek için.
    public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

    public Expression<Func<TEntity, object>>? OrderByExpression { get; private set; }

    public Expression<Func<TEntity, object>>? OrderByDescendingExpression { get; private set; }

    public Expression<Func<TEntity, object>>? ThenOrderByExpression { get; private set; }

    public Expression<Func<TEntity, object>>? ThenOrderByDescendingExpression { get; private set; }
    //Dinamik olarak, ilişkili tablo/navigasyon property eklemek için kullanılır. addInlcude kısımları.
    protected void AddInclude(Expression<Func<TEntity, object>> table) => IncludeExpressions.Add(table);

    protected void AddInclude(string table) => IncludeStrings.Add(table);

    //Hem birincil hem ikincil (veya daha fazla) sıralama kriteri tutmak. addorderby ve thenorderby.
    protected void AddOrderBy(Expression<Func<TEntity, object>> orderExpression, OrderType orderType)
    {
        if (orderType == OrderType.Descending)
        {
            OrderByDescendingExpression = orderExpression;
        }
        else
        {
            OrderByExpression = orderExpression;
        }
    }

    protected void AddThenOrderBy(Expression<Func<TEntity, object>> orderExpression, OrderType orderType)
    {
        if (orderType == OrderType.Descending)
        {
            ThenOrderByDescendingExpression = orderExpression;
        }
        else
        {
            ThenOrderByExpression = orderExpression;
        }
    }
    protected void AddPaging(int pageSize, int pageNumber)
    {
        //Sayfalama için (ör: “3. sayfanın 10 kaydı”) gerekli parametreleri ve kontrolü tutmak.
        Skip = (pageNumber - 1) * pageSize;
        Take = pageSize;
        IsPagingEnabled = true;
    }

    protected void AddCriteria(Expression<Func<TEntity, bool>> predict)
    {
        /*
            Amaç:
            Varsa mevcut kritere yeni bir filtre daha eklemek (AND ile zincirlemek).

            Nasıl?
            Eğer Criteria null’sa doğrudan ata, değilse, parametreleri uyumlu hale getirip iki şartı birleştir (veya, && ile).

            Kullanımı:
            Farklı şartları (mesela “aktif ve admin olan”) specification’a zincirlemek.
        */

        if (Criteria is null)
        {
            Criteria = predict;
        }
        else
        {
            var left = Criteria.Parameters[0];
            var visitor = new ReplaceParameterVisitor(predict.Parameters[0], left);
            var right = visitor.Visit(predict.Body);
            Criteria = Expression.Lambda<Func<TEntity, bool>>(Expression.AndAlso(Criteria.Body, right), left);
        }
    }

    private class ReplaceParameterVisitor(ParameterExpression oldParameter, ParameterExpression newParameter) : ExpressionVisitor
    {
        /*
            Amaç:
            LINQ expression’larını birleştirirken parametre uyuşmazlığı olmasın diye,
            yeni eklenen filtrenin parametrelerini, mevcut kriterin parametreleriyle aynı yapmak.
        */
        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (ReferenceEquals(node, oldParameter))
            {
                return newParameter;
            }

            return base.VisitParameter(node);
        }
    }
}
