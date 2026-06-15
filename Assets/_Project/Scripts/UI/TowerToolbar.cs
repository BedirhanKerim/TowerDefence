using System;
using _Project.Scripts.Data;
using _Project.Scripts.Enums;
using _Project.Scripts.Events;
using GenericEventBus;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Project.Scripts.UI
{
    public class TowerToolbar : MonoBehaviour
    {
        [Serializable]
        private class ToolbarButton
        {
            [SerializeField] private Button _button;
            [SerializeField] private TowerType _type;
            [SerializeField] private TextMeshProUGUI _textCount;
            [SerializeField] private GameObject _selectedImage;

            public Button Button => _button;
            public TowerType Type => _type;
            public TextMeshProUGUI TextCount => _textCount;
            public GameObject SelectedImage => _selectedImage;

        }

        [SerializeField] private ToolbarButton[] _buttons;
        [SerializeField] private TextMeshProUGUI _levelText;

        private GenericEventBus<IGameEvent> _eventBus;

        [Inject]
        public void Construct(GenericEventBus<IGameEvent> eventBus)
        {
            _eventBus = eventBus;
            _eventBus.SubscribeTo<LevelLoadedEvent>(OnLevelLoaded);
            _eventBus.SubscribeTo<TowerPlacedEvent>(OnTowerPlaced);
            _eventBus.SubscribeTo<TowerTypeSelectedEvent>(OnTowerTypeSelected);
        }

        private void OnDestroy()
        {
            _eventBus.UnsubscribeFrom<LevelLoadedEvent>(OnLevelLoaded);
            _eventBus.UnsubscribeFrom<TowerPlacedEvent>(OnTowerPlaced);
            _eventBus.UnsubscribeFrom<TowerTypeSelectedEvent>(OnTowerTypeSelected);
        }
//buton clicklerini event e abone ediyorum.
        private void Awake()
        {
            foreach (ToolbarButton toolbarButton in _buttons)
            {
                TowerType type = toolbarButton.Type;
                toolbarButton.Button.onClick.AddListener(() => _eventBus.Raise(new TowerTypeSelectedEvent(type)));
            }
        }
// level datası geldiginde tipler ile ilgili dataları toolbarda setliyorum
        private void OnLevelLoaded(ref LevelLoadedEvent levelLoadedEvent)
        {
            _levelText.text = $"Level {levelLoadedEvent.LevelNumber}";

            foreach (TowerSpawnEntry entry in levelLoadedEvent.Level.Towers)
            {
                SetCount(entry.TowerData.Type, entry.Count);
            }
        }
//tower yerleştirdigimde toolbardaki countları güncelliyorum
        private void OnTowerPlaced(ref TowerPlacedEvent towerPlacedEvent)
        {
            SetCount(towerPlacedEvent.Type, towerPlacedEvent.Remaining);
        }

        private void SetCount(TowerType type, int count)
        {
            foreach (ToolbarButton toolbarButton in _buttons)
            {
                if (toolbarButton.Type == type)
                {
                    toolbarButton.TextCount.text = count.ToString();
                    return;
                }
            }
        }
        // toolbarda tip seçimi yapıldıgında marker aç kapa için kullanıyorum
        private void OnTowerTypeSelected(ref TowerTypeSelectedEvent towerTypeSelectedEvent)
        {
            foreach (ToolbarButton toolbarButton in _buttons)
            {
                toolbarButton.SelectedImage.SetActive(toolbarButton.Type == towerTypeSelectedEvent.Type);
            }
        }
    }
}
