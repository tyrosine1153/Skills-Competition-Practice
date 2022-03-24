namespace Entity.Monster
{
    public class Virus : Monster
    {
        // default
        private void Reset()
        {
            maxHp = 100;
            hp = maxHp;
            attack = 10;
            moveSpeed = 10;
            attackSpeed = 10;
            bulletSpeed = 10;
            score = 10;
        }
        
        public override void Move()
        {
        }

        public override void Die()
        {
        }

        public override void TakeDamage(int damage)
        {
        }

        public override void Attack()
        {
        }

        public override void GoOut()
        {
        }
    }
}