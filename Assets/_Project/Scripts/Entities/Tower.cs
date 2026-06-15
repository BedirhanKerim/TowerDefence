using _Project.Scripts.Data;
using Lean.Pool;
using UnityEngine;

namespace _Project.Scripts.Entities
{
    public class Tower : MonoBehaviour, IPoolable
    {
        private const float TowerY = 0.5f;

        private TowerData _data;
        private GameObject _visualInstance;
        private int _row;
        private int _column;

        public int Row => _row;
        public int Column => _column;
        public TowerData Data => _data;

        public void SetData(TowerData data)
        {
            _data = data;
            _visualInstance = LeanPool.Spawn(data.VisualPrefab, transform);
        }

        public void SetCell(int row, int column)
        {
            _row = row;
            _column = column;
        }

        public void SetPosition(Vector2 center)
        {
            transform.position = new Vector3(center.x, TowerY, center.y);
        }

        public void OnSpawn()
        {
        }

        public void OnDespawn()
        {
            if (_visualInstance != null)
            {
                LeanPool.Despawn(_visualInstance);
                _visualInstance = null;
            }
        }
    }
}
