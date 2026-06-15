using System;
using _Project.Scripts.Data;
using _Project.Scripts.Events;
using _Project.Scripts.Services;
using GenericEventBus;
using VContainer.Unity;

namespace _Project.Scripts.Systems
{
    public class LevelManager : IStartable, IDisposable
    {
        private readonly LevelConfig _levelConfig;
        private readonly GenericEventBus<IGameEvent> _eventBus;
        private readonly ISaveService _saveService;

        private int _currentLevelIndex;

        public LevelManager(LevelConfig levelConfig, GenericEventBus<IGameEvent> eventBus, ISaveService saveService)
        {
            _levelConfig = levelConfig;
            _eventBus = eventBus;
            _saveService = saveService;
        }

        public void Start()
        {
            _currentLevelIndex = _saveService.Load().CurrentLevelIndex;
            if (_currentLevelIndex < 0 || _currentLevelIndex >= _levelConfig.Levels.Length)
                _currentLevelIndex = 0;

            _eventBus.SubscribeTo<GameWonEvent>(OnGameWon);
            BroadcastCurrentLevel();
        }

        public void Dispose()
        {
            _eventBus.UnsubscribeFrom<GameWonEvent>(OnGameWon);
        }

        private void OnGameWon(ref GameWonEvent gameWonEvent)
        {
            _currentLevelIndex++;
            _saveService.Save(new SaveData { CurrentLevelIndex = _currentLevelIndex });
        }

        private void BroadcastCurrentLevel()
        {
            LevelData level = _levelConfig.Levels[_currentLevelIndex];
            _eventBus.Raise(new LevelLoadedEvent(level, _currentLevelIndex + 1));
        }
    }
}
