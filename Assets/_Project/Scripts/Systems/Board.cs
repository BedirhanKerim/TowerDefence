using _Project.Scripts.Entities;
using _Project.Scripts.Events;
using GenericEventBus;
using UnityEngine;

namespace _Project.Scripts.Systems
{
    public class Board : IBoard
    {
        private const int Rows = 8;
        private const int Columns = 4;
        private const int PlacementStartRow = 4;
        private const float Spacing = 2.5f;
        private const float CellY = 0.5f;

        private readonly Vector2[,] _cellCenters = new Vector2[Rows, Columns];

        private readonly GenericEventBus<IGameEvent> _eventBus;

        public Board(GameObject gridPrefab, GenericEventBus<IGameEvent> eventBus)
        {
            _eventBus = eventBus;
            CreateGrid(gridPrefab);
        }

        public Vector2 GetCenter(int row, int column)
        {
            return _cellCenters[row, column];
        }

        private void CreateGrid(GameObject gridPrefab)
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    float x = column * Spacing;
                    float z = row * Spacing;
                    _cellCenters[row, column] = new Vector2(x, z);
                    GameObject cell= Object.Instantiate(gridPrefab, new Vector3(x, CellY, z), Quaternion.identity);
                    if (row<PlacementStartRow)
                    {
                        cell.GetComponent<GridPlane>().SetCell(_eventBus, row, column);
                    }
                }
            }
        }
    }
}
