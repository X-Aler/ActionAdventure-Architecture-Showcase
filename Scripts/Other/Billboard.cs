using UnityEngine;

namespace Other
{
    public class Billboard : MonoBehaviour
    {
        private GameObject _player;

        private void Awake()
        {
            _player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            if (!_player) return;
            if (Vector3.Distance(transform.position, _player.transform.position) > 15f) return;
            
            var pos = _player.transform.position;
            pos.y = transform.position.y;
            transform.LookAt(pos);
        }
    }
}
