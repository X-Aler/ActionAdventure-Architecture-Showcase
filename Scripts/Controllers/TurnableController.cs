using Other;
using UnityEngine;
using PrimeTween;

namespace Controllers
{
    public class TurnableController : MonoBehaviour
    {
        public bool cutsceneActive = false;
        public void Turn(Animator animator, TurnObjectData[] turnObjectsData)
        {
            animator.SetTrigger("Turn");

            foreach (var data in turnObjectsData)
            {
               var tween = Tween.Scale(data.target.transform, data.targetScale, 5f);
                
                if (data.destroyAfter)
                    tween.OnComplete(() => Destroy(data.target));

                if (data.isSpecialTurn)
                {
                    cutsceneActive = true;
                }
            }
        }
        
    }
}
