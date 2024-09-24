using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TicketingSystem.Domain.Entities;

namespace TicketingSystem.Persistence.EntityFrameworkCore;
public class BaseDbContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var softDeleteEntities = typeof(ISoftDelete).Assembly.GetTypes()
                .Where(type => typeof(ISoftDelete).IsAssignableFrom(type)
                    && type is { IsClass: true, IsAbstract: false });

        foreach (var softDeleteEntity in softDeleteEntities)
        {
            modelBuilder.Entity(softDeleteEntity).HasQueryFilter(
                  GenerateQueryFilterLambda(softDeleteEntity));
        }
    }

    private LambdaExpression GenerateQueryFilterLambda(Type type)
    {
        var parameter = Expression.Parameter(type, "w");
        var falseConstantValue = Expression.Constant(false);
        var propertyAccess = Expression.PropertyOrField(parameter, nameof(ISoftDelete.IsDeleted));
        var equalExpression = Expression.Equal(propertyAccess, falseConstantValue);
        var lambda = Expression.Lambda(equalExpression, parameter);

        return lambda;
    }
}
