using UnityEngine;

namespace _Project.Scripts.Data
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "BoardDefence/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private LevelData[] _levels;

        public LevelData[] Levels => _levels;
    }
}
