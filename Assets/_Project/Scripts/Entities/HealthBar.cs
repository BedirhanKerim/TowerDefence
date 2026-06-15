using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Entities
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _fill;

        public void SetFill(float ratio)
        {
            _fill.fillAmount = Mathf.Clamp01(ratio);
        }
    }
}
