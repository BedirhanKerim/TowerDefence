using Lean.Pool;
using UnityEngine;

namespace _Project.Scripts.Entities
{
    public class Bullet : MonoBehaviour, IPoolable
    {
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _arcHeight = 1.5f;
        [SerializeField] private float _scaleMultiplier = 1.5f;

        private ITargetable _target;
        private int _damage;
        private Vector3 _startPosition;
        private Vector3 _baseScale;
        private float _flightDistance;
        private float _progress;

        private void Awake()
        {
            _baseScale = transform.localScale;
        }

        public void Init(ITargetable target, int damage)
        {
            _target = target;
            _damage = damage;
            _startPosition = transform.position;
            _flightDistance = Mathf.Max(0.1f, Vector3.Distance(_startPosition, target.Position));
            _progress = 0f;
            _target.Destroyed += OnTargetDestroyed;
        }

        public void OnSpawn()
        {
            transform.localScale = _baseScale;
        }

        public void OnDespawn()
        {
            if (_target != null)
                _target.Destroyed -= OnTargetDestroyed;

            _target = null;
        }

        private void OnTargetDestroyed()
        {
            LeanPool.Despawn(gameObject);
        }

        private void Update()
        {
            _progress += _speed / _flightDistance * Time.deltaTime;

            float arc = Mathf.Sin(Mathf.Clamp01(_progress) * Mathf.PI);

            Vector3 groundPosition = Vector3.Lerp(_startPosition, _target.Position, _progress);
            groundPosition.y += _arcHeight * arc;
            transform.position = groundPosition;
            transform.localScale = _baseScale * (1f + (_scaleMultiplier - 1f) * arc);

            if (_progress >= 1f)
            {
                _target.TakeDamage(_damage);
                LeanPool.Despawn(gameObject);
            }
        }
    }
}
