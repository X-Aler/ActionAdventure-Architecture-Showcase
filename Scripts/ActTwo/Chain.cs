using System;
using ActTwo;
using UnityEngine;

public class Chain : MonoBehaviour
{
    private void Start()
    {
        ChainManager.Instance.AddChain(gameObject);
    }

    private void OnDestroy()
    {
        ChainManager.Instance.RemoveChain(gameObject);
    }
}
