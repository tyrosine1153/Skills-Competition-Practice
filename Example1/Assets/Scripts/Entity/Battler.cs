namespace Entity
{
    public abstract class Battler : Entity
    {
        public float attack;
        public float attackSpeed;
        public float bulletSpeed;

        public abstract void Attack();
    }
}