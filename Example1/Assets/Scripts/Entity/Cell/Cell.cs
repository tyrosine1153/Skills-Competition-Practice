namespace Entity.Cell
{
    public abstract class Cell : Entity
    {
        // default
        protected void Reset()
        {
            maxHp = 1;
            currentHp = maxHp;
        }
    }
}