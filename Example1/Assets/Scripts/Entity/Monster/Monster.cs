namespace Entity.Monster
{
    public abstract class Monster : Battler
    {
        public int score;
        public abstract void GoOut();
    }
}