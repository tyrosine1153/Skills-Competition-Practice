namespace Entity.Cell
{
    public class White : Cell
    {
        public override void Die()
        {
            var go = Stage.Instance.Spawn(PrefabType.Item, true);
            go.transform.position = transform.position;
            AudioManager.Instance.PlaySfx(SfxClip.SpawnItem);
            PoolManager.Instance.DestroyGameObject(gameObject, prefabType);
        }
    }
}