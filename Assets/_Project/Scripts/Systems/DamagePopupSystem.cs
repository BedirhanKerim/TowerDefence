using _Project.Scripts.Entities;
using _Project.Scripts.Events;
using GenericEventBus;
using Lean.Pool;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Systems
{
    public class DamagePopupSystem : MonoBehaviour
    {
        [SerializeField] private DamagePopup _popupPrefab;
        [SerializeField] private Vector3 _spawnOffset = new Vector3(0f, 1f, 0f);

        private GenericEventBus<IGameEvent> _eventBus;

        [Inject]
        public void Construct(GenericEventBus<IGameEvent> eventBus)
        {
            _eventBus = eventBus;
            _eventBus.SubscribeTo<DamageDealtEvent>(OnDamageDealt);
        }

        private void OnDestroy()
        {
            _eventBus.UnsubscribeFrom<DamageDealtEvent>(OnDamageDealt);
        }

        private void OnDamageDealt(ref DamageDealtEvent damageDealtEvent)
        {
            DamagePopup popup = LeanPool.Spawn(_popupPrefab, damageDealtEvent.Position + _spawnOffset, _popupPrefab.transform.rotation);
            popup.Init(damageDealtEvent.Amount);
        }
    }
}
