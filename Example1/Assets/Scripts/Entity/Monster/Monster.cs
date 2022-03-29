namespace Entity.Monster
{
    public abstract class Monster : Battler
    {
        public int score;
        public abstract void GoOut();
        
        public override void Die()
        {
            GameManager.Instance.AddScore(score);
            // Todo: Delete monster
        }
    }
}