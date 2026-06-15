using _Project.Scripts.Entities;
using UnityEngine;

namespace _Project.Scripts.Targeting
{
    [CreateAssetMenu(fileName = "AllTargeting", menuName = "BoardDefence/Targeting/All")]
    public class AllTargetingStrategy : TargetingStrategy
    {
        public override bool IsInRange(Tower tower, Enemy enemy)
        {
            int rowDistance = Mathf.Abs(enemy.Row - tower.Row);
            int columnDistance = Mathf.Abs(enemy.Column - tower.Column);
            int distance = Mathf.Max(rowDistance, columnDistance);
            return distance >= 1 && distance <= tower.Data.Range;
        }
    }
}
