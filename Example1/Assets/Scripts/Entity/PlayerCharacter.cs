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
        public bool isGamePlaying = false;
        private SpriteRenderer _spriteRenderer;

        public int WeaponLevel
        {
            get => weaponLevel;
            set => weaponLevel = Mathf.Clamp(value, MINWeaponLevel, MAXWeaponLevel);
        }

        public override float CurrentHp
        {
            get => currentHp;
            set
            {
                StageCanvas.Instance.healthGauge.value = CurrentHp / maxHp;
                base.CurrentHp = value;
            }
        }

        private Vector3 defaultPosition;

        // default
        private void Reset()
        {
            maxHp = 100;
            CurrentHp = maxHp;
            attack = 10;
            moveSpeed = 5;
            attackSpeed = 1;
            bulletSpeed = 5;
        }

        private void Start()
        {
            CurrentHp = maxHp;
            defaultPosition = transform.position;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private float _attackTimer = 0;

        protected override void Update()
        {
            if (!isGamePlaying) return;

            base.Update();

            _attackTimer += Time.deltaTime;
            if (Input.GetMouseButton(0) && _attackTimer >= attackSpeed)
            {
                _attackTimer = 0;
                Attack();
            }

            if (Input.GetMouseButtonDown(1))
            {
                StageCanvas.Instance.SwitchCheatPanel();
            }
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Monster"))
            {
                var monster = other.gameObject.GetComponent<Monster.Monster>();
                TakeDamage(monster.attack * 0.5f);
                PoolManager.Instance.DestroyGameObject(monster.gameObject, monster.prefabType);
            }
            else if (other.gameObject.CompareTag("Cell"))
            {
                var cell = other.gameObject.GetComponent<Cell.Cell>();
                cell.TakeDamage(attack);
            }
            else if (other.gameObject.CompareTag("Wall"))
            {
                transform.position = defaultPosition;
            }
        }

        public override void Attack()
        {
            // Player 크기, 또는 소환시킬 위치에 따라 값이 변경됨
            // 각 탄의 간격을 0.125로 하였다
            // 0.125를 사용하기엔 가독성이 나빠져서 *8로 1로 만들어서 계산함
            // 계산한 만큼을 나눠줄 normalize변수를 이용

            const float n = 0.125f; // 단위 일반화 변수
            const int d = 2; // 위치의 공차
            var position = 1 - weaponLevel; // 초기 위치

            for (var i = 0; i < weaponLevel; i++)
            {
                var setPosition = new Vector3(position * n, 0, 0);
                var bulletGO = PoolManager.Instance.CreateGameObject(PrefabType.PlayerBullet);

                bulletGO.transform.position = transform.position + setPosition;
                var bullet = bulletGO.GetComponent<Bullet>();
                bullet.damage = attack;
                bullet.speed = bulletSpeed;

                position += d;
            }
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

        public override void TakeDamage(float damage)
        {
            if (buffs[(int)PlayerBuff.Nullity]) return;
            Buff(PlayerBuff.Nullity, 1.5f);
            base.TakeDamage(damage);
        }

        public void Heal(float amount)
        {
            CurrentHp = Math.Min(CurrentHp + amount, maxHp);
        }

        public void GetItem(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.WeaponUpgrade:
                    weaponLevel = Math.Min(weaponLevel + 1, MAXWeaponLevel);
                    break;
                case ItemType.NullityBuff:
                    Buff(PlayerBuff.Nullity, 3f);
                    break;
                case ItemType.HealPlayer:
                    Heal(20);
                    break;
                case ItemType.HealBody:
                    Stage.Instance.Heal(20);
                    break;
                default:
                    break;
            }
        }

        private static readonly Color HalfColor = new Color(1f, 1f, 1f, 0.5f);
        private Coroutine _nullityCoroutine;
        
        private void Buff(PlayerBuff playerBuff, float time)
        {
            if (buffs[(int)playerBuff])
            {
                StopCoroutine(_nullityCoroutine);
            }

            _nullityCoroutine = StartCoroutine(CoBuff(playerBuff, time));
        }
        
        private IEnumerator CoBuff(PlayerBuff playerBuff, float time)
        {
            buffs[(int)playerBuff] = true;
            _spriteRenderer.color = HalfColor;
            yield return new WaitForSeconds(time);
            _spriteRenderer.color = Color.white;
            buffs[(int)playerBuff] = false;
        }
    }
}