using System.Collections;
using UI;
using UnityEngine;

namespace Entity.Monster
{
    public abstract class BossMonster : Monster
    {
        public override void Die()
        {
            base.Die();
            GameManager.Instance.GameClear();
        }

        public override float CurrentHp
        {
            get => currentHp;
            set
            {
                StageCanvas.Instance.bossGauge.value = currentHp / maxHp;
                base.CurrentHp = value;
            }
        }

        private void OnEnable()
        {
            StageCanvas.Instance.bossGauge.gameObject.SetActive(true);
            CurrentHp = maxHp;
        }

        private void OnDisable()
        {
            StageCanvas.Instance.bossGauge.gameObject.SetActive(false);
        }

        public override void Move()
        {
        }
    }
}