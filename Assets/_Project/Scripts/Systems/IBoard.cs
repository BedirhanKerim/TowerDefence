using UnityEngine;

namespace _Project.Scripts.Systems
{
    public interface IBoard
    {
        Vector2 GetCenter(int row, int column);
    }
}
