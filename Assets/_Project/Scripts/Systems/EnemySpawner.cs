using System.Collections;
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
        [SerializeField] private int _spawnCount = 5;
        [SerializeField] private float _spawnInterval = 1f;

        private IBoard _board;
        private EnemySystem _enemySystem;
        private GenericEventBus<IGameEvent> _eventBus;

        [Inject]
        public void Construct(IBoard board, EnemySystem enemySystem, GenericEventBus<IGameEvent> eventBus)
        {
            Debug.Log("constructed");
            _board = board;
            _enemySystem = enemySystem;
            _eventBus = eventBus;
            _eventBus.SubscribeTo<GameStartedEvent>(OnGameStarted);
        }

        private void OnDestroy()
        {
            _eventBus.UnsubscribeFrom<GameStartedEvent>(OnGameStarted);
        }

        private void OnGameStarted(ref GameStartedEvent gameStartedEvent)
        {
            Debug.Log("start");
            StartCoroutine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine()
        {
            for (int i = 0; i < _spawnCount; i++)
            {
                Spawn();
                yield return new WaitForSeconds(_spawnInterval);
            }
        }

        private void Spawn()
        {
            int column = Random.Range(0, Columns);
            Enemy enemy = Instantiate(_enemyPrefab);
            enemy.SetCell(SpawnRow, column);
            enemy.SetPosition(_board.GetCenter(SpawnRow, column));
            _enemySystem.Register(enemy);
        }
    }
}
