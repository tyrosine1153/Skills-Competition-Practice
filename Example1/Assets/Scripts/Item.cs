public class Item : Entity.Entity
{
    public ItemType itemType;
    
    // default
    protected void Reset()
    {
        maxHp = 1;
        currentHp = maxHp;
    }
    
    public override void Move()
    {
        
    }

    public override void Die()
    {
    }
}