using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using PrimeTween;
using UnityEngine.UI;

namespace ActTwo
{
    public class GachaController : MonoBehaviour
    {
        public static GachaController Instance;

        [FormerlySerializedAs("convertCourseText")]
        [SerializeField] private TextMeshProUGUI rollCostText;
        [FormerlySerializedAs("alaxChanceText")]
        [SerializeField] private TextMeshProUGUI bombDropChanceText;
        [SerializeField] private TextMeshProUGUI flameBoyChanceText;
        [FormerlySerializedAs("sanyaChanceText")]
        [SerializeField] private TextMeshProUGUI homingProjectileChanceText;

        [SerializeField] private int needToRoll = 3;

        [FormerlySerializedAs("alaxRollChance")]
        [SerializeField] private int bombDropRollChance = 3;
        [SerializeField] private int flameBoyRollChance = 5;
        [FormerlySerializedAs("sanyaRollChance")]
        [SerializeField] private int homingProjectileRollChance = 100;
        [SerializeField] private int neviletteRollChance;

        [SerializeField] private Image rolledImage;

        [FormerlySerializedAs("alaxImage")]
        [SerializeField] private Image bombDropImage;
        [SerializeField] private Image flameBoyImage;
        [SerializeField] private Image neviletteImage;

        [SerializeField] private Sprite[] rollingSprites;
        
        [FormerlySerializedAs("neuviletteSound")]
        [SerializeField] private AudioClip neviletteSound;
        [FormerlySerializedAs("alaxSound")]
        [SerializeField] private AudioClip bombDropSound;
        [SerializeField] private AudioClip flameBoySound;
        [FormerlySerializedAs("sanyaSound")]
        [SerializeField] private AudioClip homingProjectileSound;

        private bool _rolling;
        
        public bool canRollNevilette;

        private void Awake()
        {
            if (Instance && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            
            UpdateUI();
        }
        
        public void UpdateUI()
        {
            rollCostText.text = $"1x={needToRoll}р";

            bombDropChanceText.text = $"{bombDropRollChance}%";
            flameBoyChanceText.text = $"{flameBoyRollChance}%";
            homingProjectileChanceText.text = $"{homingProjectileRollChance}%";
        }

        public void Roll()
        {
            if (MoneyManager.Instance.GetMoney() < needToRoll || _rolling) return;
            _rolling = true;

            var randomValue = Random.Range(1, 101);

            AudioClip sound = null;

            if (randomValue <= neviletteRollChance && canRollNevilette)
            {
                rolledImage.sprite = rollingSprites[3];
                neviletteImage.sprite = rollingSprites[3];
                AbilityManager.Instance.SetAbility(Abilities.Nevilette);
                sound = neviletteSound;
            }
            else if (randomValue <= neviletteRollChance + bombDropRollChance)
            {
                rolledImage.sprite = rollingSprites[2];
                bombDropImage.sprite = rollingSprites[2];
                AbilityManager.Instance.SetAbility(Abilities.BombDrop);
                sound = bombDropSound;
            }
            else if (randomValue <= neviletteRollChance + bombDropRollChance + flameBoyRollChance)
            {
                rolledImage.sprite = rollingSprites[1];
                flameBoyImage.sprite = rollingSprites[1];
                AbilityManager.Instance.SetAbility(Abilities.FlameBoy);
                sound = flameBoySound;
            }
            else if (randomValue <= neviletteRollChance + bombDropRollChance + flameBoyRollChance + homingProjectileRollChance)
            {
                rolledImage.sprite = rollingSprites[0];
                AbilityManager.Instance.SetAbility(Abilities.HomingProjectile);
                sound = homingProjectileSound;
            }
            
            AudioManager.Instance.PlayOneShot(sound);

            rolledImage.gameObject.SetActive(true);

            Tween.Scale(rolledImage.transform, Vector3.one, Vector3.zero, 1f).OnComplete(() =>
            {
                rolledImage.gameObject.SetActive(false);
                _rolling = false;
            });

            MoneyManager.Instance.RemoveMoney(needToRoll);

            UpdateUI();
        }
    }
}