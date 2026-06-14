using _Project.Scripts.Enums;

namespace _Project.Scripts.Events
{
    public readonly struct TowerTypeSelectedEvent : IGameEvent
    {
        public readonly TowerType Type;

        public TowerTypeSelectedEvent(TowerType type)
        {
            Type = type;
        }
    }
}
