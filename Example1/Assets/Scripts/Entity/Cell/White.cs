namespace Entity.Cell
{
    public class White : Cell
    {
        // default
        private void Reset()
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

        public override void TakeDamage(int damage)
        {
        }
    }
}