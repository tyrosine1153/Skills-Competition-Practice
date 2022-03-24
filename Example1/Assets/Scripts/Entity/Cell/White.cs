using System;
using Random = UnityEngine.Random;

namespace Entity.Cell
{
    public class White : Cell
    {
        public override void Die()
        {
            var item = Stage.Instance.Spawn(PrefabType.Item, true);
            item.transform.position = transform.position;
            
            base.Die();
        }
    }
}