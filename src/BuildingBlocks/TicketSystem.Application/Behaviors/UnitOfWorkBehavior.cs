using System.Transactions;
using MediatR;
using TicketingSystem.Domain.Repositories;

namespace TicketSystem.Application.Behaviors;

public class UnitOfWorkBehavior<TRequest, TResponse>(IUnitOfWork unitOfWork) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (IsNotCommand())
        {
            return await next();
        }

        using var transactionScope = new TransactionScope();
        var response = await next();
        await unitOfWork.SaveChangesAsync(cancellationToken);
        transactionScope.Complete();
        return response;
    }

    private static bool IsNotCommand()
    {
        return !typeof(TRequest).Name.EndsWith("Command");
    }
}