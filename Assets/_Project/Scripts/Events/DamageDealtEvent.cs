using UnityEngine;

namespace _Project.Scripts.Events
{
    public readonly struct DamageDealtEvent : IGameEvent
    {
        public readonly Vector3 Position;
        public readonly int Amount;

        public DamageDealtEvent(Vector3 position, int amount)
        {
            Position = position;
            Amount = amount;
        }
    }
}
