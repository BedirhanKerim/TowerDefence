using _Project.Scripts.Enums;
using UnityEngine;

namespace _Project.Scripts.Data
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "BoardDefence/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] private EnemyType _type;
        [SerializeField] private int _health;
        [SerializeField] private float _speed;
        [SerializeField] private GameObject _visualPrefab;

        public EnemyType Type => _type;
        public int Health => _health;
        public float Speed => _speed;
        public GameObject VisualPrefab => _visualPrefab;
    }
}
