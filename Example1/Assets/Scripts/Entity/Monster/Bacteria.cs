namespace Entity.Monster
{
    public class Bacteria : Monster
    {
        // default
        public override void Reset()
        {
            prefabType = PrefabType.Bacteria;
            maxHp = 5;
            CurrentHp = maxHp;
            attack = 10;
            moveSpeed = 0.5f;
            attackSpeed = 1;
            bulletSpeed = 1;
            score = 10;
        }

        public override void Attack()
        {
        }
    }
}
