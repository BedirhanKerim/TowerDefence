using _Project.Scripts.Entities;
using UnityEngine;

namespace _Project.Scripts.Targeting
{
    [CreateAssetMenu(fileName = "ForwardTargeting", menuName = "BoardDefence/Targeting/Forward")]
    public class ForwardTargetingStrategy : TargetingStrategy
    {
        public override bool IsInRange(Tower tower, Enemy enemy)
        {
            bool sameColumn = enemy.Column == tower.Column;
            int rowsAhead = enemy.Row - tower.Row;
            return sameColumn && rowsAhead > 0 && rowsAhead <= tower.Data.Range;
        }
    }
}
