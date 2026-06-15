using System;
using _Project.Scripts.Data;
using _Project.Scripts.Events;
using DG.Tweening;
using GenericEventBus;
using Lean.Pool;
using UnityEngine;

namespace _Project.Scripts.Entities
{
    public class Enemy : MonoBehaviour, ITargetable, IPoolable
    {
        private const float EnemyY = 0.5f;

        [SerializeField] private float _moveDuration = 0.15f;

        private EnemyData _data;
        private GenericEventBus<IGameEvent> _eventBus;
        private Tween _moveTween;
        private GameObject _visualInstance;
        private int _health;
        private int _row;
        private int _column;

        public int Row => _row;
        public int Column => _column;
        public float Speed => _data.Speed;
        public bool IsAlive => _health > 0;
        public Vector3 Position => transform.position;

        public event Action Destroyed;

        public void SetData(EnemyData data, GenericEventBus<IGameEvent> eventBus)
        {
            _data = data;
            _eventBus = eventBus;
            _health = data.Health;
            _visualInstance = LeanPool.Spawn(data.VisualPrefab, transform);
        }

        public void SetCell(int row, int column)
        {
            _row = row;
            _column = column;
        }

        public void SetPosition(Vector2 center)
        {
            transform.position = new Vector3(center.x, EnemyY, center.y);
        }

        public void MoveTo(Vector2 center)
        {
            _moveTween?.Kill();
            Vector3 target = new Vector3(center.x, EnemyY, center.y);
            _moveTween = transform.DOMove(target, _moveDuration).SetEase(Ease.Linear);
        }

        public void TakeDamage(int amount)
        {
            _health -= amount;
            _eventBus.Raise(new DamageDealtEvent(transform.position, amount));
        }

        public void OnSpawn()
        {
        }

        public void OnDespawn()
        {
            _moveTween?.Kill();
            Destroyed?.Invoke();
            Destroyed = null;

            if (_visualInstance != null)
            {
                LeanPool.Despawn(_visualInstance);
                _visualInstance = null;
            }
        }
    }
}
