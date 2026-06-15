using _Project.Scripts.Events;
using GenericEventBus;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts.Entities
{
    public class GridPlane : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameObject _cellAvailableMark;

        private int _row;
        private int _column;
        private GenericEventBus<IGameEvent> _eventBus;

        public void SetCell(GenericEventBus<IGameEvent> eventBus, int row, int column)
        {
            _eventBus = eventBus;
            _row = row;
            _column = column;
            GetComponent<Collider>().enabled = true;
            _cellAvailableMark.SetActive(true);
            _eventBus.SubscribeTo<TowerPlacedEvent>(OnTowerPlacedEvent);
            _eventBus.SubscribeTo<GameStartedEvent>(OnGameStarted);
        }

        private void OnDestroy()
        {
            if (_eventBus != null)
            {
                _eventBus.UnsubscribeFrom<TowerPlacedEvent>(OnTowerPlacedEvent);
                _eventBus.UnsubscribeFrom<GameStartedEvent>(OnGameStarted);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _eventBus.Raise(new CellClickedEvent(_row, _column));
            Debug.Log("gridtıklama!!   "+_row+","+_column);
        }

        private void OnTowerPlacedEvent(ref TowerPlacedEvent towerPlacedEvent)
        {
            if (towerPlacedEvent.Coordinates == new Vector2Int(_row, _column))
            {
                _cellAvailableMark.SetActive(false);

            }
        }

        private void OnGameStarted(ref GameStartedEvent gameStartedEvent)
        {
            _cellAvailableMark.SetActive(false);
        }
    }
}
