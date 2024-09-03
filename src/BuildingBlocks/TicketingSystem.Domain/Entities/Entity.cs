namespace TicketingSystem.Domain.Entities;

public abstract class Entity : IEntity
{
    public override string ToString()
    {
        return $"[ENTITY: {GetType().Name}] Keys = {string.Join(", ", GetKeys())}";
    }

    protected abstract object?[] GetKeys();
}

public abstract class Entity<TKey> : Entity, IEntity<TKey>
{
    public virtual TKey Id { get; private set; } = default!;

    protected Entity()
    {

    }

    protected Entity(TKey id)
    {
        Id = id;
    }

    protected override object?[] GetKeys()
    {
        return [Id];
    }

    public override string ToString()
    {
        return $"[ENTITY: {GetType().Name}] Id = {Id}";
    }
}
