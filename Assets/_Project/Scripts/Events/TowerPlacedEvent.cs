using _Project.Scripts.Enums;
using UnityEngine;

namespace _Project.Scripts.Events
{
    public readonly struct TowerPlacedEvent : IGameEvent
    {
        public readonly TowerType Type;
        public readonly int Remaining;
        public readonly Vector2Int Coordinates;

        public TowerPlacedEvent(TowerType type, int remaining, Vector2Int coordinates)
        {
            Type = type;
            Remaining = remaining;
            Coordinates = coordinates;
        }
    }
}
