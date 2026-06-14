using _Project.Scripts.Data;

namespace _Project.Scripts.Events
{
    public readonly struct LevelLoadedEvent : IGameEvent
    {
        public readonly LevelData Level;

        public LevelLoadedEvent(LevelData level)
        {
            Level = level;
        }
    }
}
