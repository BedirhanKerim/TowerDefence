using System.Collections.Generic;
using _Project.Scripts.Entities;
using _Project.Scripts.Events;
using GenericEventBus;
using Lean.Pool;
using UnityEngine;
using VContainer.Unity;

namespace _Project.Scripts.Systems
{
    public class EnemySystem : ITickable
    {
        private const int BaseRow = 0;

        private readonly IBoard _board;
        private readonly GenericEventBus<IGameEvent> _eventBus;
        private readonly List<Enemy> _enemies = new List<Enemy>();
        private readonly Dictionary<Enemy, float> _stepTimers = new Dictionary<Enemy, float>();

        public IReadOnlyList<Enemy> Enemies => _enemies;

        public EnemySystem(IBoard board, GenericEventBus<IGameEvent> eventBus)
        {
            _board = board;
            _eventBus = eventBus;
        }

        public void Register(Enemy enemy)
        {
            _enemies.Add(enemy);
            _stepTimers[enemy] = 0f;
        }

        public void Tick()
        {
            for (int i = _enemies.Count - 1; i >= 0; i--)
            {
                Enemy enemy = _enemies[i];

                if (!enemy.IsAlive)
                {
                    _eventBus.Raise(new EnemyDiedEvent());
                    RemoveTracking(enemy, i);
                    enemy.Die();
                    continue;
                }

                _stepTimers[enemy] += Time.deltaTime;

                if (_stepTimers[enemy] < 1f / enemy.Speed)
                    continue;

                _stepTimers[enemy] = 0f;
                StepDown(enemy, i);
            }
        }

        private void StepDown(Enemy enemy, int index)
        {
            int toRow = enemy.Row - 1;
            int column = enemy.Column;

            if (toRow < BaseRow)
            {
                _eventBus.Raise(new EnemyReachedBaseEvent());
                RemoveTracking(enemy, index);
                LeanPool.Despawn(enemy.gameObject);
                return;
            }

            enemy.SetCell(toRow, column);
            enemy.MoveTo(_board.GetCenter(toRow, column));
        }

        private void RemoveTracking(Enemy enemy, int index)
        {
            _enemies.RemoveAt(index);
            _stepTimers.Remove(enemy);
        }
    }
}
