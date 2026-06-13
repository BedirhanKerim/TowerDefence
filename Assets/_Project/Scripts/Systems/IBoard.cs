using UnityEngine;

namespace _Project.Scripts.Systems
{
    public interface IBoard
    {
        void SetCenter(int row, int column, Vector2 center);
    }
}
