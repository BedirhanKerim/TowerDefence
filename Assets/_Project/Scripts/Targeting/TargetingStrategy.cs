using _Project.Scripts.Entities;
using UnityEngine;

namespace _Project.Scripts.Targeting
{
    public abstract class TargetingStrategy : ScriptableObject
    {
        public abstract bool IsInRange(Tower tower, Enemy enemy);
    }
}
