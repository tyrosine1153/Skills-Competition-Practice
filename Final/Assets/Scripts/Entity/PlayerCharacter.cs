using System;
using System.Collections;
using UI;
using UnityEngine;

namespace Entity
{
    public class PlayerCharacter : Battler
    {
        public bool canMove;

        public int ItemGetCount { get; private set; }
        
        private int _bulletLevel;
        public int BulletLevel
        {
            get => _bulletLevel;
            set
            {
                _bulletLevel = Mathf.Clamp(value, 1, 5);
                StageCanvas.Instance.OnBulletLevelChanged(_bulletLevel);
            }
        }

        public override float CurrentHp
        {
            get => _currentHp;
            set
            {
                if(!canMove) return;
                _currentHp = Mathf.Clamp(value, 0, maxHp);
                StageCanvas.Instance.healthGauge.value = _currentHp / maxHp;
                if (_currentHp <= 0)
                {
                    Die();
                }
            }
        }

        protected override void Start()
        {
            base.Start();
            _defaultPosition = transform.position;
            _buffCoroutines = new Coroutine[Enum.GetValues(typeof(BuffType)).Length];
            buffActive = new bool[Enum.GetValues(typeof(BuffType)).Length];
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _attackTimer = 0f;
            CurrentHp = maxHp;
            BulletLevel = 1;
            ItemGetCount = 0;
        }

        private float _attackTimer;
        protected override void Update()
        {
            _attackTimer += Time.deltaTime;
            if (Input.GetMouseButtonDown(1))
            {
                StageCanvas.Instance.SwitchCheatPanel();
            }
            
            if(!canMove) return;
            
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                Move();
            }

            if (Input.GetMouseButton(0) && _attackTimer >= attackSpeed)
            {
                _attackTimer = 0;
                Attack();
            }
        }

        protected override void Move()
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed * Time.deltaTime);
        }

        public override void Attack()
        {
            const float n = 0.125f;
            const int d = 2;
            var position = 1 - BulletLevel;

            AudioManager.Instance.PlaySfx(SfxClip.PlayerAttack);
            for (var i = 0; i < BulletLevel; i++)
            {
                var setPosition = new Vector3(position * n, 0, 0);
                var go = PoolManager.Instance.CreateGameObject(PrefabType.PlayerBullet);
                go.transform.position = transform.position + setPosition;
                go.transform.rotation = Quaternion.identity;
                var bullet = go.GetComponent<Bullet.Bullet>();
                bullet.attackDamage = attackDamage;
                bullet.direction = Vector3.up;
                bullet.moveSpeed = bulletSpeed;

                position += d;
            }
        }

        public void SpecialAttack()
        {
            for (var i = 0; i < 40; i++)
            {
                var go = PoolManager.Instance.CreateGameObject(PrefabType.PlayerBullet);
                go.transform.position = transform.position;
                go.transform.rotation = Quaternion.identity;
                go.transform.Rotate(new Vector3(0, 0, i * 9));
                var bullet = go.GetComponent<Bullet.Bullet>();
                bullet.attackDamage = attackDamage;
                bullet.direction = Vector3.down;
                bullet.moveSpeed = bulletSpeed;
            }
        }

        public override void Die()
        {
            AudioManager.Instance.PlaySfx(SfxClip.PlayerDie);
            GameManager.Instance.GameEnd(false); // game over
        }
        
        private Vector3 _defaultPosition;
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (!canMove) return;
            
            if (other.CompareTag("Wall"))
            {
                transform.position = _defaultPosition;
            }
            else if (other.CompareTag("Item"))
            {
                var item = other.GetComponent<Item>();
                GetItem(item.itemType);
                PoolManager.Instance.DestroyGameObject(item.gameObject, item.prefabType);
            }
            else if (other.CompareTag("Monster"))
            {
                var monster = other.GetComponent<Monster.Monster>();
                TakeDamage(monster.attackDamage * 0.5f);
                if (monster.prefabType != PrefabType.CoronaVirus 
                    && monster.prefabType != PrefabType.CoronaVirusVariety)
                {
                    PoolManager.Instance.DestroyGameObject(monster.gameObject, monster.prefabType);
                }

            }
            else if (other.CompareTag("Cell"))
            {
                var cell = other.GetComponent<Cell.Cell>();
                cell.Die();
            }
        }

        public bool cheatNullityBuff;
        public override void TakeDamage(float amount)
        {
            if(cheatNullityBuff) return;
            if(buffActive[(int)BuffType.Nullity]) return;
            base.TakeDamage(amount);
            Buff(BuffType.Nullity, 1.5f);
        }
        
        public void Heal(float amount)
        {
            CurrentHp += amount;
        }
        
        public void GetItem(ItemType itemType)
        {
            ItemGetCount++;
            AudioManager.Instance.PlaySfx(SfxClip.UseItem);
            StageCanvas.Instance.OnGetItem(itemType);
            switch (itemType)
            {
                case ItemType.WeaponLevelUp:
                    BulletLevel++;
                    break;
                case ItemType.NullityBuff:
                    Buff(BuffType.Nullity, 3.0f);
                    break;
                case ItemType.PlayerHeal:
                    Heal(20);
                    break;
                case ItemType.StageHeal:
                    Stage.Instance.Heal(20);
                    break;
                case ItemType.GetScore:
                    GameManager.Instance.AddScore(500);
                    break;
                case ItemType.Boom:
                    SpecialAttack();
                    break;
                default:
                    break;
            }
        }
        
        public bool[] buffActive;
        private Coroutine[] _buffCoroutines;
        private const float BuffExitTime = 0.5f;
        private SpriteRenderer _spriteRenderer;
        private static readonly Color NullityColor = new Color(1, 1, 1, 0.2f);

        public void Buff(BuffType buffType, float time)
        {
            if (buffActive[(int) buffType])
            {
                StopCoroutine(_buffCoroutines[(int) buffType]);
                _buffCoroutines[(int) buffType] = null;
            }

            _buffCoroutines[(int) buffType] = StartCoroutine(CoBuff(buffType, time));
        }

        private IEnumerator CoBuff(BuffType buffType, float time)
        {
            buffActive[(int) buffType] = true;
            _spriteRenderer.color = NullityColor;
            yield return new WaitForSeconds(time - BuffExitTime);
            _spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(BuffExitTime);
            buffActive[(int) buffType] = false;
        }
    }
}