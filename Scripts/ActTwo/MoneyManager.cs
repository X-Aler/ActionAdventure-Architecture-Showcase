using TMPro;
using UnityEngine;

namespace ActTwo
{
    public class MoneyManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moneyText;
        
        public static MoneyManager Instance;
        
        private int _money;
        
        private void Awake()
        {
            if (Instance && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        
            AddMoney(10);
        }

        public void AddMoney(int money)
        {
            _money += money;
            UpdateText();
        }
        
        public void RemoveMoney(int money)
        {
            _money = Mathf.Max(0, _money - money);
            UpdateText();
        }

        public void NullMoney()
        {
            _money = 0;
            UpdateText();
        }

        public int GetMoney()
        {
            return _money;
        }

        private void UpdateText()
        {
            moneyText.text = $"{_money}р";
        }
    }
}
