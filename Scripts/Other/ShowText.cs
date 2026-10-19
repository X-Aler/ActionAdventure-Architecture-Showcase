using UnityEngine;

[CreateAssetMenu(fileName = "ShowText", menuName = "Scriptable Objects/ActsText")]
public class ShowText : ScriptableObject
{
    [TextArea(1, 10)] 
    public string[] textToShow;
    public AudioClip[] textSound;
}
