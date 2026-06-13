using UnityEngine;

namespace _Project.Scripts.Entities
{
    public class Enemy : MonoBehaviour
    {
        private const float EnemyY = 0.5f;

        [SerializeField] private int _health;
        [SerializeField] private float _speed;

        private int _row;
        private int _column;

        public int Row => _row;
        public int Column => _column;
        public float Speed => _speed;
        public bool IsAlive => _health > 0;

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
