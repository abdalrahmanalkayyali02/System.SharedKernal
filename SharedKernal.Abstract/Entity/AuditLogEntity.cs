using SharedKernal.Abstract.Shared.Enum;

namespace SharedKernal.Abstract.Entity;

public class AuditLogEntity : BaseEntity
{
    public string EntityName { get; private set; }      // table name
    public Guid EntityId { get; private set; }          // record id
    public ActionType Action { get; private set; }          // (Update, Delete, Create)
    public string ChangedByUserId { get; private set; } // who made the change?
    public string Changes { get; private set; }         // old and new values (usually JSON)
    public string IpAddress { get; private set; }   // IP address

    public AuditLogEntity() { }

    public AuditLogEntity(string entityName, Guid entityId, ActionType action, string changedByUserId, string changes, string ipAddress)
    {
        EntityName = entityName;
        EntityId = entityId;
        Action = action;
        ChangedByUserId = changedByUserId;
        Changes = changes;
        IpAddress = ipAddress;
    }
}