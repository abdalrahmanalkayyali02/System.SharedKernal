namespace SharedKernal.Abstract.Entity;

public abstract class BaseEntity 
{
    public Guid Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; } =  DateTime.UtcNow;
    public DateTime? UpdatedAt { get; protected set; } = null;
    public bool IsDeleted { get; protected set; } = false;
    public DateTime? DeletedAt { get; protected set; }
        
        
     
    protected BaseEntity()
    {
        Id = Guid.CreateVersion7();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = null;
        IsDeleted = false;
        DeletedAt = null;
    }

    public void UpdateTimestamp()
    {
        UpdatedAt = DateTime.UtcNow;
    }

    public void SoftDelete()
    {
        if (IsDeleted) return; 

        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        UpdateTimestamp();
    }

    public void Restore()
    {
        IsDeleted = false;
        DeletedAt = null;
        UpdateTimestamp();
    }
}