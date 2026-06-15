using System.Collections;
using _Project.Scripts.Data;
using _Project.Scripts.Entities;
using _Project.Scripts.Events;
using GenericEventBus;
using Lean.Pool;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Systems
{
    public class EnemySpawner : MonoBehaviour
    {
        private const int Columns = 4;
        private const int SpawnRow = 7;

        [SerializeField] private Enemy _enemyPrefab;

        private IBoard _board;
        private EnemySystem _enemySystem;
        private GenericEventBus<IGameEvent> _eventBus;
        private LevelData _currentLevel;

        [Inject]
        public void Construct(IBoard board, EnemySystem enemySystem, GenericEventBus<IGameEvent> eventBus)
        {
            _board = board;
            _enemySystem = enemySystem;
            _eventBus = eventBus;
            _eventBus.SubscribeTo<LevelLoadedEvent>(OnLevelLoaded);
            _eventBus.SubscribeTo<GameStartedEvent>(OnGameStarted);
        }

        private void OnDestroy()
        {
            _eventBus.UnsubscribeFrom<LevelLoadedEvent>(OnLevelLoaded);
            _eventBus.UnsubscribeFrom<GameStartedEvent>(OnGameStarted);
        }

        private void OnLevelLoaded(ref LevelLoadedEvent levelLoadedEvent)
        {
            _currentLevel = levelLoadedEvent.Level;
        }

        private void OnGameStarted(ref GameStartedEvent gameStartedEvent)
        {
            if (_currentLevel == null)
                return;

            StartCoroutine(SpawnRoutine(_currentLevel));
        }

        private IEnumerator SpawnRoutine(LevelData level)
        {
            foreach (EnemySpawnEntry entry in level.Enemies)
            {
                for (int i = 0; i < entry.Count; i++)
                {
                    Spawn(entry.EnemyData);
                    yield return new WaitForSeconds(level.SpawnInterval);
                }
            }
        }

        private void Spawn(EnemyData data)
        {
            int column = Random.Range(0, Columns);
            Enemy enemy = LeanPool.Spawn(_enemyPrefab);
            enemy.SetData(data, _eventBus);
            enemy.SetCell(SpawnRow, column);
            enemy.SetPosition(_board.GetCenter(SpawnRow, column));
            _enemySystem.Register(enemy);
        }
    }
}
