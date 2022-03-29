namespace Entity.Monster
{
    public class Germ : Monster
    {
        // default
        private void Reset()
        {
            maxHp = 100;
            currentHp = maxHp;
            attack = 10;
            moveSpeed = 1;
            attackSpeed = 1;
            bulletSpeed = 1;
            score = 10;
        }

        public override void Move()
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