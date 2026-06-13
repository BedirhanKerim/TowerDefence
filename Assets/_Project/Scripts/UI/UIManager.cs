using _Project.Scripts.Events;
using GenericEventBus;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Project.Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _startPanel;
        [SerializeField] private Button _startButton;

        private GenericEventBus<IGameEvent> _eventBus;

        [Inject]
        public void Construct(GenericEventBus<IGameEvent> eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            _startButton.onClick.AddListener(OnStartClicked);
        }

        private void OnDestroy()
        {
            _startButton.onClick.RemoveListener(OnStartClicked);
        }

        private void OnStartClicked()
        {
            _eventBus.Raise(new GameStartedEvent());
            _startPanel.SetActive(false);
        }
    }
}
