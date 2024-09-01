using System.Linq.Expressions;

namespace TicketingSystem.Domain.Specifications;

public interface ISpecification<T>
{
    bool IsSatisfiedBy(T obj);

    Expression<Func<T, bool>> ToExpression();
}
