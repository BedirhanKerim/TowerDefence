using System;
using _Project.Scripts.Data;
using _Project.Scripts.Events;
using GenericEventBus;
using VContainer.Unity;

namespace _Project.Scripts.Systems
{
    public class LevelManager : IStartable, IDisposable
    {
        private readonly LevelConfig _levelConfig;
        private readonly GenericEventBus<IGameEvent> _eventBus;

        private int _currentLevelIndex;

        public LevelManager(LevelConfig levelConfig, GenericEventBus<IGameEvent> eventBus)
        {
            _levelConfig = levelConfig;
            _eventBus = eventBus;
        }

        public void Start()
        {
            _eventBus.SubscribeTo<GameStartedEvent>(OnGameStarted);
            _eventBus.SubscribeTo<LevelCompletedEvent>(OnLevelCompleted);
        }

        public void Dispose()
        {
            _eventBus.UnsubscribeFrom<GameStartedEvent>(OnGameStarted);
        }

        private void OnGameStarted(ref GameStartedEvent gameStartedEvent)
        {
            LevelData level = _levelConfig.Levels[_currentLevelIndex];
            _eventBus.Raise(new LevelStartedEvent(level));
        }

        private void OnLevelCompleted(ref LevelCompletedEvent levelCompletedEvent)
        {
            _currentLevelIndex++;
        }
        
    }
}
