using System;
using UnityEngine;

namespace _Project.Scripts.Data
{
    [Serializable]
    public class EnemySpawnEntry
    {
        [SerializeField] private EnemyData _enemyData;
        [SerializeField] private int _count;

        public EnemyData EnemyData => _enemyData;
        public int Count => _count;
    }
}
