namespace TicketingSystem.Domain.Entities;

public interface IAuditedObject
{
    DateTimeOffset LastModificationTime { get; set; }
    DateTimeOffset CreationTime { get; set; }   
}
