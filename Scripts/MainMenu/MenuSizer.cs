using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;

public class MenuSizer : MonoBehaviour
{
    [SerializeField] private List<SizeItem> items;

    [SerializeField] private float targetSize;
    
    [SerializeField] private float waitTime;

    private void Awake()
    {
        CursorController.Unlock();
        StartCoroutine(ChangeSize());
    }

    private IEnumerator ChangeSize()
    {
        while (true)
        {
            foreach (var i in items )
            {
                Tween.Scale(i.target.transform, Vector3.one, new Vector3(targetSize, targetSize, targetSize), i.time);
            }
            
            yield return new WaitForSeconds(waitTime);
            
            foreach (var i in items )
            {
                Tween.Scale(i.target.transform, new Vector3(targetSize, targetSize, targetSize), Vector3.one, i.time);
            }
            
            yield return new WaitForSeconds(waitTime);
        }
    }
}