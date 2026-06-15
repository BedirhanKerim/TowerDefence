using System.Collections.Generic;
using _Project.Scripts.Data;
using _Project.Scripts.Entities;
using _Project.Scripts.Enums;
using _Project.Scripts.Events;
using GenericEventBus;
using Lean.Pool;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Systems
{
    public class TowerPlacementSystem : MonoBehaviour
    {
        [SerializeField] private Tower _towerPrefab;

        private IBoard _board;
        private TowerSystem _towerSystem;
        private GenericEventBus<IGameEvent> _eventBus;

        private bool _hasSelection;
        private TowerType _selectedType;
        private readonly Dictionary<TowerType, int> _remaining = new Dictionary<TowerType, int>();
        private readonly Dictionary<TowerType, TowerData> _dataByType = new Dictionary<TowerType, TowerData>();
        private readonly HashSet<(int, int)> _occupied = new HashSet<(int, int)>();

        [Inject]
        public void Construct(IBoard board, TowerSystem towerSystem, GenericEventBus<IGameEvent> eventBus)
        {
            _board = board;
            _towerSystem = towerSystem;
            _eventBus = eventBus;
            _eventBus.SubscribeTo<LevelLoadedEvent>(OnLevelLoaded);
            _eventBus.SubscribeTo<TowerTypeSelectedEvent>(OnTowerTypeSelected);
            _eventBus.SubscribeTo<CellClickedEvent>(OnCellClicked);
        }

        private void OnDestroy()
        {
            _eventBus.UnsubscribeFrom<LevelLoadedEvent>(OnLevelLoaded);
            _eventBus.UnsubscribeFrom<TowerTypeSelectedEvent>(OnTowerTypeSelected);
            _eventBus.UnsubscribeFrom<CellClickedEvent>(OnCellClicked);
        }

        private void OnLevelLoaded(ref LevelLoadedEvent levelLoadedEvent)
        {
            _remaining.Clear();
            _dataByType.Clear();
            _occupied.Clear();
            _hasSelection = false;

            foreach (TowerSpawnEntry entry in levelLoadedEvent.Level.Towers)
            {
                _remaining[entry.TowerData.Type] = entry.Count;
                _dataByType[entry.TowerData.Type] = entry.TowerData;
            }
        }
//toolbardaki seçimimin yolladıgı eventi dinliyorum.
        private void OnTowerTypeSelected(ref TowerTypeSelectedEvent towerTypeSelectedEvent)
        {
            _selectedType = towerTypeSelectedEvent.Type;
            _hasSelection = true;
        }
//seçim yaptıysam, envanterim yeterliyse ve tıkladıgım grid boş ise tower ı yerleştiriyorum
        private void OnCellClicked(ref CellClickedEvent cellClickedEvent)
        {
            if (!_hasSelection)
                return;

            if (!_remaining.TryGetValue(_selectedType, out int count) || count <= 0)
                return;

            (int, int) cell = (cellClickedEvent.Row, cellClickedEvent.Column);
            if (_occupied.Contains(cell))
                return;

            PlaceTower(cellClickedEvent.Row, cellClickedEvent.Column);
            _occupied.Add(cell);

            int remaining = count - 1;
            _remaining[_selectedType] = remaining;
            _eventBus.Raise(new TowerPlacedEvent(_selectedType, remaining));
        }

        private void PlaceTower(int row, int column)
        {
            TowerData data = _dataByType[_selectedType];
            Tower tower = LeanPool.Spawn(_towerPrefab);
            tower.SetData(data);
            tower.SetCell(row, column);
            tower.SetPosition(_board.GetCenter(row, column));
            _towerSystem.Register(tower);
        }
    }
}
