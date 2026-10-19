using Controllers;
using Managers;
using UnityEngine;

namespace MainMenu
{
    public class PlayButton : ButtonController
    {
        [SerializeField] private GameObject actChooser;
        public override void Click()
        {
            actChooser.SetActive(true);
        }
    }
}
