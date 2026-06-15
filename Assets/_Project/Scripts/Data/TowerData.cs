using _Project.Scripts.Entities;
using _Project.Scripts.Enums;
using _Project.Scripts.Targeting;
using UnityEngine;

namespace _Project.Scripts.Data
{
    [CreateAssetMenu(fileName = "TowerData", menuName = "BoardDefence/TowerData")]
    public class TowerData : ScriptableObject
    {
        [SerializeField] private TowerType _type;
        [SerializeField] private int _damage;
        [SerializeField] private int _range;
        [SerializeField] private float _interval;
        [SerializeField] private TargetingStrategy _targetingStrategy;
        [SerializeField] private GameObject _visualPrefab;
        
        [SerializeField] private Bullet _projectilePrefab;

        public TowerType Type => _type;
        public int Damage => _damage;
        public int Range => _range;
        public float Interval => _interval;
        public TargetingStrategy TargetingStrategy => _targetingStrategy;
        public GameObject VisualPrefab => _visualPrefab;
        public Bullet ProjectilePrefab => _projectilePrefab;
    }
}
