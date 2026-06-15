using System.Collections.Generic;
using _Project.Scripts.Entities;
using Lean.Pool;
using UnityEngine;
using VContainer.Unity;

namespace _Project.Scripts.Systems
{
    public class TowerSystem : ITickable
    {
        private readonly EnemySystem _enemySystem;
        private readonly List<Tower> _towers = new List<Tower>();
        private readonly Dictionary<Tower, float> _fireTimers = new Dictionary<Tower, float>();

        public TowerSystem(EnemySystem enemySystem)
        {
            _enemySystem = enemySystem;
        }

        public void Register(Tower tower)
        {
            _towers.Add(tower);
            _fireTimers[tower] = 0f;
        }

        public void Tick()
        {
            for (int i = 0; i < _towers.Count; i++)
            {
                Tower tower = _towers[i];
                _fireTimers[tower] += Time.deltaTime;

                if (_fireTimers[tower] < tower.Data.Interval)
                    continue;

                Enemy target = FindTarget(tower);
                if (target == null)
                    continue;

                Fire(tower, target);
                _fireTimers[tower] = 0f;
            }
        }

        private Enemy FindTarget(Tower tower)
        {
            Enemy best = null;
            IReadOnlyList<Enemy> enemies = _enemySystem.Enemies;

            for (int i = 0; i < enemies.Count; i++)
            {
                Enemy enemy = enemies[i];

                if (!enemy.IsAlive)
                    continue;

                if (!tower.Data.TargetingStrategy.IsInRange(tower, enemy))
                    continue;
                if (best == null || enemy.Row < best.Row)// base e en yakın enemy seçilir
                    best = enemy;
            }

            return best;
        }

        private void Fire(Tower tower, Enemy target)
        {
            Bullet bullet = LeanPool.Spawn(tower.Data.ProjectilePrefab, tower.transform.position, Quaternion.identity);
            bullet.Init(target, tower.Data.Damage);
        }
    }
}
