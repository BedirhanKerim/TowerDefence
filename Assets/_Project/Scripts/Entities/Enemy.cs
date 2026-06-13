using _Project.Scripts.Data;
using UnityEngine;

namespace _Project.Scripts.Entities
{
    public class Enemy : MonoBehaviour
    {
        private const float EnemyY = 0.5f;

        private EnemyData _data;
        private int _health;
        private int _row;
        private int _column;

        public int Row => _row;
        public int Column => _column;
        public float Speed => _data.Speed;
        public bool IsAlive => _health > 0;

        public void SetData(EnemyData data)
        {
            _data = data;
            _health = data.Health;
            Instantiate(data.VisualPrefab, transform);
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
    }
}
