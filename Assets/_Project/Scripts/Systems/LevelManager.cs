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
            _eventBus.SubscribeTo<LevelCompletedEvent>(OnLevelCompleted);
            BroadcastCurrentLevel();
        }

        public void Dispose()
        {
            _eventBus.UnsubscribeFrom<LevelCompletedEvent>(OnLevelCompleted);
        }

        private void OnLevelCompleted(ref LevelCompletedEvent levelCompletedEvent)
        {
            _currentLevelIndex++;
            BroadcastCurrentLevel();
        }

        private void BroadcastCurrentLevel()
        {
            if (_currentLevelIndex >= _levelConfig.Levels.Length)
                return;

            LevelData level = _levelConfig.Levels[_currentLevelIndex];
            _eventBus.Raise(new LevelLoadedEvent(level));
        }
        
    }
}
