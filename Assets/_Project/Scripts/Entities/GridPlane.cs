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
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _eventBus.Raise(new CellClickedEvent(_row, _column));
            Debug.Log("gridtıklama!11!!"+_row+","+_column);
        }
    }
}
