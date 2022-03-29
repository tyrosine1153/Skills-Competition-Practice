using System;

namespace Entity.Cell
{
    public class Red : Cell
    {
        private const int PainPoint = 10;
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
            GameManager.Instance.CurrentStage.CurrentPain += PainPoint;
            // Todo: Delete cell
        }
    }
}