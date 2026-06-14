using System;
using UnityEngine;

namespace _Project.Scripts.Data
{
        [Serializable]
        public class TowerSpawnEntry
        {
            [SerializeField] private TowerData _towerData;
            [SerializeField] private int _count;

            public TowerData TowerData => _towerData;
            public int Count => _count;
        }
    
}