using _Project.Scripts.Entities;
using _Project.Scripts.Events;
using GenericEventBus;
using Lean.Pool;
using UnityEngine;
using VContainer;

namespace _Project.Scripts.Systems
{
    public class HitParticleSystem : MonoBehaviour
    {
        [SerializeField] private HitParticle _particlePrefab;
        [SerializeField] private float _despawnDelay = 1f;
        [SerializeField] private Vector3 _spawnOffset = new Vector3(0f, 2f, 0f);

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
            HitParticle particle = LeanPool.Spawn(_particlePrefab, damageDealtEvent.Position + _spawnOffset, _particlePrefab.transform.rotation);
            LeanPool.Despawn(particle.gameObject, _despawnDelay);
        }
    }
}
