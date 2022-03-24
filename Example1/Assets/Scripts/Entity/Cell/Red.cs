namespace Entity.Cell
{
    public class Red : Cell
    {
        private const float PainPoint = 10;

        public override void Die()
        {
            Stage.Instance.Hurt(PainPoint);
            base.Die();
        }
    }
}