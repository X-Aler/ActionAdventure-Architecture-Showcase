using System;
using TMPro;
using UnityEngine;

namespace ActTwo
{
    public class PivoButton : MonoBehaviour
    {
        [SerializeField] private PivoSize size;

        private TextMeshProUGUI _buttonText;

        private void Awake()
        {
            _buttonText = GetComponentInChildren<TextMeshProUGUI>();
            PivoShopManager.Instance.SetUI(_buttonText, size);
        }

        public void OnClick()
        {
            PivoShopManager.Instance.Buy(size);
        }
    }
}
