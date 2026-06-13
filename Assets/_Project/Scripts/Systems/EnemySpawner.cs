using System.Collections;
using _Project.Scripts.Data;
using _Project.Scripts.Entities;
using _Project.Scripts.Events;
using GenericEventBus;
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

        [Inject]
        public void Construct(IBoard board, EnemySystem enemySystem, GenericEventBus<IGameEvent> eventBus)
        {
            _board = board;
            _enemySystem = enemySystem;
            _eventBus = eventBus;
            _eventBus.SubscribeTo<LevelStartedEvent>(OnLevelStarted);
        }

        private void OnDestroy()
        {
            _eventBus.UnsubscribeFrom<LevelStartedEvent>(OnLevelStarted);
        }

        private void OnLevelStarted(ref LevelStartedEvent levelStartedEvent)
        {
            StartCoroutine(SpawnRoutine(levelStartedEvent.Level));
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
            Enemy enemy = Instantiate(_enemyPrefab);
            enemy.SetData(data);
            enemy.SetCell(SpawnRow, column);
            enemy.SetPosition(_board.GetCenter(SpawnRow, column));
            _enemySystem.Register(enemy);
        }
    }
}
