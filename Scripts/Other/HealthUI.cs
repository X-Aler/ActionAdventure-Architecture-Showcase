using Controllers;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Other
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private Slider healthSlider;
        [SerializeField] private TMP_Text healthText;
        
        private HealthController _health;
        
        private void Awake()
        {
            _health = GetComponent<HealthController>();
            _health.OnHealthChanged += UpdateUI; 
        }
        
        private void UpdateUI(float current, float max)
        {
            var percent = (current / max);
            healthSlider.value = percent;
            healthText.text = (percent * 100f).ToString("F0") + "%";
        }

        private void OnDestroy()
        {
            _health.OnHealthChanged -= UpdateUI;
        }
    }
}