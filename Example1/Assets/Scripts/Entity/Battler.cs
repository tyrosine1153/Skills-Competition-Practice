namespace Entity
{
    public abstract class Battler : Entity
    {
        public int attack;
        public int moveSpeed;
        public int attackSpeed;
        public int bulletSpeed;

        public abstract void Attack();
    }
}