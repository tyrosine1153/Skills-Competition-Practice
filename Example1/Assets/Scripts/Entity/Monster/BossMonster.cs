namespace Entity.Monster
{
    public abstract class BossMonster : Monster
    {
        public abstract void SummonMonster();
        public abstract void AttackMultiple();
        
        public override void Die()
        {
            base.Die();
            GameManager.Instance.GameClear();
        }
    }
}