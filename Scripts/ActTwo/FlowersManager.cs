using System.Collections.Generic;
using UnityEngine;

namespace ActTwo
{
    public class FlowersManager : MonoBehaviour
    {
        public static FlowersManager Instance;

        private HashSet<Flowers> _collected = new();

        private void Awake() => Instance = this;

        public void Collect(Flowers flowerType)
        {
            _collected.Add(flowerType);

            if (_collected.Count == 3)
            {
                GachaController.Instance.canRollNevilette = true;
            }
        }

        public bool HasFlower(Flowers flowerType)
        {
            return _collected.Contains(flowerType);
        }
    }
}