using TicketingSystem.Domain.Events;

namespace TicketingSystem.Domain.Entities;

public abstract class AggregateRoot : Entity, IHasConcurrencyStamp
{
    private readonly List<IDomainEvent> _localEvents = new List<IDomainEvent>();
    private readonly List<IDomainEvent> _distributedEvents = new List<IDomainEvent>();

    public IReadOnlyCollection<IDomainEvent> LocalEvents => _localEvents.AsReadOnly();
    public IReadOnlyCollection<IDomainEvent> DistributedEvents => _distributedEvents.AsReadOnly();

    public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString("N");

    public virtual void ClearLocalEvents()
    {
        _localEvents.Clear();
    }

    public virtual void ClearDistributedEvents()
    {
        _distributedEvents.Clear();
    }

    protected virtual void AddLocalEvent(IDomainEvent domainEvent)
    {
        _localEvents.Add(domainEvent);
    }

    protected virtual void AddDistributedEvent(IDomainEvent domainEvent)
    {
        _distributedEvents.Add(domainEvent);
    }
}

public abstract class AggregateRoot<TKey> : Entity<TKey>, IHasConcurrencyStamp, IAggregateRoot
{
    private readonly List<IDomainEvent> _localEvents = new List<IDomainEvent>();
    private readonly List<IDomainEvent> _distributedEvents = new List<IDomainEvent>();

    public IReadOnlyCollection<IDomainEvent> LocalEvents => _localEvents.AsReadOnly();
    public IReadOnlyCollection<IDomainEvent> DistributedEvents => _distributedEvents.AsReadOnly();

    public string ConcurrencyStamp { get; set; }

    protected AggregateRoot()
    {
        ConcurrencyStamp = Guid.NewGuid().ToString("N");
    }

    protected AggregateRoot(TKey id) : base(id)
    {
        ConcurrencyStamp = Guid.NewGuid().ToString("N");
    }

    public virtual void ClearLocalEvents()
    {
        _localEvents.Clear();
    }

    public virtual void ClearDistributedEvents()
    {
        _distributedEvents.Clear();
    }

    protected virtual void AddLocalEvent(IDomainEvent domainEvent)
    {
        _localEvents.Add(domainEvent);
    }

    protected virtual void AddDistributedEvent(IDomainEvent domainEvent)
    {
        _distributedEvents.Add(domainEvent);
    }
}
