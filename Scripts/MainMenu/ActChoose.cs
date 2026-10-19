using Managers;
using UnityEngine;
public class ActChoose : MonoBehaviour
{
    [SerializeField] private GameObject codeArea;
    [SerializeField] private string actName;
    [SerializeField] private string actCode;

    public void Choose()
    {
        codeArea.SetActive(true);
    }

    public void CodeEnter(string code)
    {
        if (code == actCode)
        {
            LoadingManager.Instance.Load(actName);
        }
    }
    
}
