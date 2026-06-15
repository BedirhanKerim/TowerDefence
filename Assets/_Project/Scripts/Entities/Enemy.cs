using System;
using _Project.Scripts.Data;
using Lean.Pool;
using UnityEngine;

namespace _Project.Scripts.Entities
{
    public class Enemy : MonoBehaviour, ITargetable, IPoolable
    {
        private const float EnemyY = 0.5f;

        private EnemyData _data;
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

        public void SetData(EnemyData data)
        {
            _data = data;
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

        public void TakeDamage(int amount)
        {
            _health -= amount;
        }

        public void OnSpawn()
        {
        }

        public void OnDespawn()
        {
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
