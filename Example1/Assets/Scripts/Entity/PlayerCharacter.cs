using System;
using System.Collections;
using UI;
using UnityEngine;

namespace Entity
{
    public class PlayerCharacter : Battler
    {
        public bool[] buffs = new bool[Enum.GetValues(typeof(PlayerBuff)).Length];
        private const int MAXWeaponLevel = 5;
        public int weaponLevel = 1;

        // default
        private void Reset()
        {
            maxHp = 100;
            hp = maxHp;
            attack = 10;
            moveSpeed = 5;
            attackSpeed = 1;
            bulletSpeed = 5;
        }

        private void Update()
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                Move();
            }

            if (Input.GetMouseButton(0))
            {
                Attack();
            }

            if (Input.GetMouseButtonDown(1))
            {
                InGameCanvas.Instance.SwitchCheatPanel();
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Monster"))
            {
                var monster = other.gameObject.GetComponent<Monster.Monster>();
                TakeDamage(monster.attack);
            }
            else if (other.gameObject.CompareTag("Cell"))
            {
            }
            else if (other.gameObject.CompareTag("Item"))
            {
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Bullet"))
            {
                var bullet = other.gameObject.GetComponent<Bullet>();
                TakeDamage(bullet.attack);
            }
        }

        public override void Attack()
        {
            // 대충 총알 발사
        }

        public override void Move()
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed *
                                Time.deltaTime);
        }

        public override void Die()
        {
            GameManager.Instance.GameOver();
        }

        public override void TakeDamage(int damage)
        {
            if (buffs[(int)PlayerBuff.Nullity]) return;

            hp -= damage;
            if (hp < 0)
            {
                Die();
            }
        }

        public void Heal(int amount)
        {
            hp = Math.Min(hp + amount, maxHp);
        }

        public void GetItem(Item item)
        {
            switch (item)
            {
                case Item.WeaponUpgrade:
                    weaponLevel = Math.Min(weaponLevel + 1, MAXWeaponLevel);
                    break;
                case Item.NullityBuff:
                    StartCoroutine(Buff(PlayerBuff.Nullity, 3f));
                    break;
                case Item.HealPlayer:
                    Heal(20);
                    break;
                case Item.HealBody:
                    GameManager.Instance.currentStage.Heal(20);
                    break;
                default:
                    break;
            }
        }

        private IEnumerator Buff(PlayerBuff playerBuff, float time)
        {
            buffs[(int)playerBuff] = true;
            yield return new WaitForSeconds(time);
            buffs[(int)playerBuff] = false;
        }
    }
}