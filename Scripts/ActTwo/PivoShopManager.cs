using System.Linq;
using Controllers;
using Managers;
using TMPro;
using UnityEngine;

namespace ActTwo
{
    public enum PivoSize
    {
        Small,
        Medium,
        Big
    }

    public class PivoShopManager : MonoBehaviour
    {
        public static PivoShopManager Instance;

        [SerializeField] private HealthController playerHealth;
        [SerializeField] private AudioClip drinkSound;

        [SerializeField] public PivoData[] pivoDates;

        private void Awake()
        {
            if (Instance && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void Buy(PivoSize size)
        {
            var currentMoney = MoneyManager.Instance.GetMoney();

            var data = GetData(size);

            if (currentMoney < data.price) return;
            
            var amount = playerHealth.maxHealth * (data.healPercent / 100f);
            playerHealth.Heal(amount);
                
            MoneyManager.Instance.RemoveMoney(data.price);
            AudioManager.Instance.PlayOneShot(drinkSound);
        }

        public void SetUI(TextMeshProUGUI buttonText, PivoSize size)
        {
            var data = GetData(size);
            buttonText.text = $"{data.pivoAmount}л ({data.healPercent}%) {data.price}р";
        }

        private PivoData GetData(PivoSize size)
        {
            return pivoDates.FirstOrDefault(pivoDate => pivoDate.size == size);
        }
    }
}