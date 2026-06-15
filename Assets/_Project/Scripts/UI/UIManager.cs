using _Project.Scripts.Events;
using _Project.Scripts.Systems;
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
        [SerializeField] private GameObject _winPanel;
        [SerializeField] private GameObject _failPanel;
        [SerializeField] private Button _winNextButton;
        [SerializeField] private Button _failReplayButton;

        private GenericEventBus<IGameEvent> _eventBus;
        private ISceneLoader _sceneLoader;

        [Inject]
        public void Construct(GenericEventBus<IGameEvent> eventBus, ISceneLoader sceneLoader)
        {
            _eventBus = eventBus;
            _sceneLoader = sceneLoader;
            _eventBus.SubscribeTo<GameWonEvent>(OnGameWon);
            _eventBus.SubscribeTo<GameFailedEvent>(OnGameFailed);
        }

        private void Awake()
        {
            _startButton.onClick.AddListener(OnStartClicked);
            _winNextButton.onClick.AddListener(OnReloadClicked);
            _failReplayButton.onClick.AddListener(OnReloadClicked);
        }

        private void OnDestroy()
        {
            _startButton.onClick.RemoveListener(OnStartClicked);
            _winNextButton.onClick.RemoveListener(OnReloadClicked);
            _failReplayButton.onClick.RemoveListener(OnReloadClicked);
            _eventBus.UnsubscribeFrom<GameWonEvent>(OnGameWon);
            _eventBus.UnsubscribeFrom<GameFailedEvent>(OnGameFailed);
        }

        private void OnStartClicked()
        {
            _eventBus.Raise(new GameStartedEvent());
            _startPanel.SetActive(false);
        }

        private void OnReloadClicked()
        {
            _sceneLoader.Reload();
        }

        private void OnGameWon(ref GameWonEvent gameWonEvent)
        {
            Time.timeScale = 0f;
            _winPanel.SetActive(true);
        }

        private void OnGameFailed(ref GameFailedEvent gameFailedEvent)
        {
            Time.timeScale = 0f;
            _failPanel.SetActive(true);
        }
    }
}
