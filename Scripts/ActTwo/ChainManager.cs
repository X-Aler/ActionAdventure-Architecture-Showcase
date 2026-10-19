using System.Collections.Generic;
using UnityEngine;

namespace ActTwo
{
    public class ChainManager : MonoBehaviour
    {
        public static ChainManager Instance;
        
        private List<GameObject> _chains = new List<GameObject>();
        
        private void Awake()
        {
            if (Instance && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void AddChain(GameObject chain)
        {
            if (_chains.Contains(chain)) return;
            _chains.Add(chain);
        }

        public void RemoveChain(GameObject chain)
        {
            _chains.Remove(chain);
        }

        public int GetChains()
        {
            return _chains.Count;
        }
    }
}
