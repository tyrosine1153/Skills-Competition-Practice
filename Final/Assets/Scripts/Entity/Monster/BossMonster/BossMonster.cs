using System;
using System.Globalization;
using UI;
using UnityEngine;

namespace Entity.Monster.BossMonster
{
    public abstract class BossMonster : Monster
    {
        public override float CurrentHp
        {
            get => _currentHp;
            set
            {
                _currentHp = Mathf.Min(value, maxHp);
                StageCanvas.Instance.bossGauge.value = _currentHp / maxHp;
                StageCanvas.Instance.bossText.text = _currentHp.ToString(CultureInfo.CurrentCulture);
                
                if (_currentHp <= 0)
                {
                    Die();
                }
            }
        }

        protected virtual void OnEnable()
        {
            StageCanvas.Instance.bossGauge.gameObject.SetActive(true);
            CurrentHp = maxHp;
        }

        private void OnDisable()
        {
            try
            {
                StageCanvas.Instance.bossGauge.gameObject.SetActive(false);
            }
            catch (Exception) { /* ignored */ }
        }

        public override void Attack()
        {
        }

        public override void Die()
        {
            AudioManager.Instance.PlaySfx(SfxClip.BossDie);
            GameManager.Instance.GameEnd(true); // stage clear
        }

        public abstract void Pattern1(float rotation);  // bullet 1
        public abstract void Pattern2(float rotation);  // bullet 2
        public abstract void Pattern3();  // spawn
    }
}