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
            // Todo: 아이템 랜덤 생성
            // Todo: Delete cell
        }
    }
}