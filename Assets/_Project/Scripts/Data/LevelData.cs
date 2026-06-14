using UnityEngine;

namespace _Project.Scripts.Data
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "BoardDefence/LevelData")]
    public class LevelData : ScriptableObject
    {
        //bu level için spawnlanacak enemyler
        [SerializeField] private EnemySpawnEntry[] _enemies;
        [SerializeField] private float _spawnInterval = 1f;

        public EnemySpawnEntry[] Enemies => _enemies;
        public float SpawnInterval => _spawnInterval;
        //bu level için spawnlanacak towerlar
        [SerializeField] private TowerSpawnEntry[] _towers;
        public TowerSpawnEntry[] Towers => _towers;
    }
}
