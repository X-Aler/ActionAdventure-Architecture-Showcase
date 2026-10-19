using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using PrimeTween;

public class MenuThemeChanger : MonoBehaviour
{
    [SerializeField] private Image themeOne;
    [SerializeField] private Image themeTwo;
    [SerializeField] private Sprite[] sprites;

    [SerializeField] private float timeToChange;

    private int _currentTheme;

    private void Awake()
    {
        StartCoroutine(ThemeChange());
    }

    private IEnumerator ThemeChange()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToChange);
            
            themeOne.sprite = sprites[_currentTheme];
            themeTwo.sprite = sprites[(_currentTheme + 1) % sprites.Length];
            
            Tween.Alpha(themeOne, 1, 0f, timeToChange);
            Tween.Alpha(themeTwo,0, 1f, timeToChange);
            
            yield return new WaitForSeconds(timeToChange);
            _currentTheme = (_currentTheme + 1) % sprites.Length;
            
        }
    }
}