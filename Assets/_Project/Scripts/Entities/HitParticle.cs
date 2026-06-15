using Lean.Pool;
using UnityEngine;

namespace _Project.Scripts.Entities
{
    public class HitParticle : MonoBehaviour, IPoolable
    {
        [SerializeField] private ParticleSystem _particleSystem;

        public void OnSpawn()
        {
            _particleSystem.Play();
        }

        public void OnDespawn()
        {
        }
    }
}
