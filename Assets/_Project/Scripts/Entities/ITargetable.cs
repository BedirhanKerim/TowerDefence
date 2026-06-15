using System;
using UnityEngine;

namespace _Project.Scripts.Entities
{
    public interface ITargetable
    {
        Vector3 Position { get; }
        event Action Destroyed;
        void TakeDamage(int amount);
    }
}
