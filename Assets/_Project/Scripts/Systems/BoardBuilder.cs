using UnityEngine;
using VContainer;

namespace _Project.Scripts.Systems
{
    public class BoardBuilder : MonoBehaviour
    {
        private const int Rows = 8;
        private const int Columns = 4;
        private const float Spacing = 2.5f;
        private const float CellY = 0.5f;

        [SerializeField] private GameObject _gridPrefab;

        private IBoard _board;

        [Inject]
        public void Construct(IBoard board)
        {
            _board = board;
        }

        private void Start()
        {
            CreateGridBoard();
        }

        private void CreateGridBoard()
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    float x = column * Spacing;
                    float z = row * Spacing;
                    Instantiate(_gridPrefab, new Vector3(x, CellY, z), Quaternion.identity);
                    _board.SetCenter(row, column, new Vector2(x, z));
                }
            }
        }
    }
}
