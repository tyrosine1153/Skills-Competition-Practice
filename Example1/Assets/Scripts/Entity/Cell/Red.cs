using System;

namespace Entity.Cell
{
    public class Red : Cell
    {
        private const int PainPoint = 10;
        
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