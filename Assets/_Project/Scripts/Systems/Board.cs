using UnityEngine;

namespace _Project.Scripts.Systems
{
    public class Board : IBoard
    {
        private const int Rows = 8;
        private const int Columns = 4;
        private const int PlacementStartRow = 4;

        private readonly Vector2[,] _cellCenters = new Vector2[Rows, Columns];

        public void SetCenter(int row, int column, Vector2 center)
        {
            _cellCenters[row, column] = center;
        }
    }
}
