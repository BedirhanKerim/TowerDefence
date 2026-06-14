using _Project.Scripts.Enums;

namespace _Project.Scripts.Events
{
    public readonly struct TowerPlacedEvent : IGameEvent
    {
        public readonly TowerType Type;
        public readonly int Remaining;

        public TowerPlacedEvent(TowerType type, int remaining)
        {
            Type = type;
            Remaining = remaining;
        }
    }
}
