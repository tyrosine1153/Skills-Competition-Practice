namespace Entity.Monster
{
    public class CoronaVirusVariety : BossMonster
    {
        // default
        private void Reset()
        {
            maxHp = 100;
            currentHp = maxHp;
            attack = 10;
            moveSpeed = 2;
            attackSpeed = 2;
            bulletSpeed = 2;
            score = 10;
        }

        #region Action

        public override void Move()
        {
        }

        public override void Attack()
        {
        }

        public override void GoOut()
        {
        }

        public override void SummonMonster()
        {
        }

        public override void AttackMultiple()
        {
        }

        #endregion
    }
}