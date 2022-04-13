namespace Entity.Cell
{
    public class Red : Cell
    {
        public float painPoint;
        
        public override void Die()
        {
            Stage.Instance.Hurt(painPoint);
            PoolManager.Instance.DestroyGameObject(gameObject, prefabType);
        }
    }
}