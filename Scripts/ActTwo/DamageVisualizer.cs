using System.Collections;
using UnityEngine;

namespace ActTwo
{
    public static class DamageVisualizer
    {
        public static IEnumerator Visualize(SpriteRenderer renderer, float duration = 0.2f)
        {
            renderer.color = Color.red;
            yield return new WaitForSeconds(duration);
            renderer.color = Color.white;
        }
    }
}
