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
        private const int MINWeaponLevel = 1;
        public int weaponLevel = MINWeaponLevel;
        public int WeaponLevel
        {
            get => weaponLevel;
            set => weaponLevel = Mathf.Clamp(value, MINWeaponLevel, MAXWeaponLevel);
        }
        
        // default
        private void Reset()
        {
            maxHp = 100;
            currentHp = maxHp;
            attack = 10;
            moveSpeed = 5;
            attackSpeed = 1;
            bulletSpeed = 5;
        }

        private void Start()
        {
            currentHp = maxHp;
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
                TakeDamage((int)(monster.attack * 0.5f));
            }
            else if (other.gameObject.CompareTag("Cell"))
            {
                var cell = other.gameObject.GetComponent<Cell.Cell>();
                cell.TakeDamage(attack);
            }
            else if (other.gameObject.CompareTag("Item"))
            {
                var item = other.gameObject.GetComponent<Item>();
                GetItem(item.itemType);
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
            // Todo : 대충 총알 발사
        }

        public override void Move()
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"))
                                * moveSpeed * Time.deltaTime);
        }

        public override void Die()
        {
            GameManager.Instance.GameOver();
        }

        public override void TakeDamage(int damage)
        {
            if(buffs[(int)PlayerBuff.Nullity]) return;
            StartCoroutine(Buff(PlayerBuff.Nullity, 1.5f));
            base.TakeDamage(damage);
        }

        public void Heal(int amount)
        {
            currentHp = Math.Min(currentHp + amount, maxHp);
        }

        public void GetItem(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.WeaponUpgrade:
                    weaponLevel = Math.Min(weaponLevel + 1, MAXWeaponLevel);
                    break;
                case ItemType.NullityBuff:
                    StartCoroutine(Buff(PlayerBuff.Nullity, 3f));
                    break;
                case ItemType.HealPlayer:
                    Heal(20);
                    break;
                case ItemType.HealBody:
                    GameManager.Instance.CurrentStage.Heal(20);
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
