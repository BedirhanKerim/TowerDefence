using _Project.Scripts.Data;

namespace _Project.Scripts.Events
{
    public readonly struct LevelStartedEvent : IGameEvent
    {
        public readonly LevelData Level;

        public LevelStartedEvent(LevelData level)
        {
            Level = level;
        }
    }
}
