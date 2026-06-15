using Lean.Pool;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Entities
{
    public class DamagePopup : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _text;
        [SerializeField] private float _lifetime = 1f;
        [SerializeField] private float _moveSpeed = 1.5f;
        [SerializeField] private Vector3 _moveDirection = new Vector3(0f, 1f, 1f);

        private float _elapsed;

        public void Init(int amount)
        {
            _text.text = amount.ToString();
            _text.alpha = 1f;
            _elapsed = 0f;
        }

        private void Update()
        {
            _elapsed += Time.deltaTime;
            transform.position += _moveDirection * (_moveSpeed * Time.deltaTime);
            _text.alpha = Mathf.Clamp01(1f - _elapsed / _lifetime);

            if (_elapsed >= _lifetime)
                LeanPool.Despawn(gameObject);
        }
    }
}
