using System;
using Controllers;
using Managers;
using UnityEngine;

namespace MainMenu
{
    public class SettingsButton : ButtonController
    {
        [SerializeField] private GameObject settings;
        
        private bool _active;

        private void Awake()
        {
            settings.SetActive(_active);
        }

        public override void Click()
        {
            settings.SetActive(_active);
            
            _active = !_active;
        }
    }
}
