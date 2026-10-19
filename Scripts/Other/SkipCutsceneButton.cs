using UnityEngine;
using UnityEngine.Playables;

namespace Other
{
    public class SkipCutsceneButton : MonoBehaviour
    {
        private PlayableDirector _director;
        public void Init(PlayableDirector director)
        {
            _director = director;
        }
        
        public void Skip()
        {
            _director.time = _director.duration;
            _director.Evaluate();
            _director.Stop();
        }
    }
}
