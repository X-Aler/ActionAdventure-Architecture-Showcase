using System.Collections;
using ActTwo;
using Controllers;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

namespace Other
{
    public class BossStarter : MonoBehaviour
    {
        [SerializeField] private PlayableDirector bossCutscene;
        [SerializeField] private Boss boss;
        [SerializeField] private Color bossColor;
        [SerializeField] private string bossName;
        [SerializeField] private TextMeshProUGUI cutsceneNameText;
        [SerializeField] private TextMeshProUGUI nameText;
        
        [SerializeField] private AudioClip bossTheme;
        [SerializeField] private AudioClip mainTheme;
        
        private bool _touched;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player") || _touched) return;

            other.GetComponent<HealthController>().OnDeath += OnPlayerDeath;
            _touched = true;
            BossStart();
        }
        
        private void OnBossDeath()
        {
            ChangeTheme(mainTheme, bossTheme);
            
            boss.health.OnDeath -= OnBossDeath;
        }

        private void OnPlayerDeath()
        {
            ChangeTheme(mainTheme, bossTheme);
            _touched = false;
            boss.health.Heal(boss.health.maxHealth);
        }

        private void ChangeTheme(AudioClip newTheme, AudioClip oldTheme)
        {
            AudioManager.Instance.StopLoop(oldTheme);
            AudioManager.Instance.PlayLoop(newTheme);
        }

        private void BossStart()
        {
            cutsceneNameText.text = bossName;
            cutsceneNameText.color = bossColor;
            
            bossCutscene.Play();
        }

        public void BossInit()
        {
            nameText.text = bossName;
            
            boss.Initialize();
            
            ChangeTheme(bossTheme, mainTheme);

            boss.health.OnDeath += OnBossDeath;
        }
    }
}
