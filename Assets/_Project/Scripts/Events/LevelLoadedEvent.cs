using _Project.Scripts.Data;

namespace _Project.Scripts.Events
{
    public readonly struct LevelLoadedEvent : IGameEvent
    {
        public readonly LevelData Level;
        public readonly int LevelNumber;

        public LevelLoadedEvent(LevelData level, int levelNumber)
        {
            Level = level;
            LevelNumber = levelNumber;
        }
    }
}
