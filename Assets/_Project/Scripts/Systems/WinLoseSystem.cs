using System;
using _Project.Scripts.Data;
using _Project.Scripts.Events;
using GenericEventBus;
using VContainer.Unity;

namespace _Project.Scripts.Systems
{
    public class WinLoseSystem : IStartable, IDisposable
    {
        private readonly GenericEventBus<IGameEvent> _eventBus;

        private int _total;
        private int _defeated;
        private bool _finished;

        public WinLoseSystem(GenericEventBus<IGameEvent> eventBus)
        {
            _eventBus = eventBus;
        }

        public void Start()
        {
            _eventBus.SubscribeTo<LevelLoadedEvent>(OnLevelLoaded);
            _eventBus.SubscribeTo<EnemyDiedEvent>(OnEnemyDied);
            _eventBus.SubscribeTo<EnemyReachedBaseEvent>(OnEnemyReachedBase);
        }

        public void Dispose()
        {
            _eventBus.UnsubscribeFrom<LevelLoadedEvent>(OnLevelLoaded);
            _eventBus.UnsubscribeFrom<EnemyDiedEvent>(OnEnemyDied);
            _eventBus.UnsubscribeFrom<EnemyReachedBaseEvent>(OnEnemyReachedBase);
        }

        private void OnLevelLoaded(ref LevelLoadedEvent levelLoadedEvent)
        {
            _total = 0;
            foreach (EnemySpawnEntry entry in levelLoadedEvent.Level.Enemies)
                _total += entry.Count;

            _defeated = 0;
            _finished = false;
        }

        private void OnEnemyDied(ref EnemyDiedEvent enemyDiedEvent)
        {
            if (_finished)
                return;

            _defeated++;

            if (_total > 0 && _defeated >= _total)
            {
                _finished = true;
                _eventBus.Raise(new GameWonEvent());
            }
        }

        private void OnEnemyReachedBase(ref EnemyReachedBaseEvent enemyReachedBaseEvent)
        {
            if (_finished)
                return;

            _finished = true;
            _eventBus.Raise(new GameFailedEvent());
        }
    }
}
