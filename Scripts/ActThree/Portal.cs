using System;
using Managers;
using UnityEngine;
using PrimeTween;

namespace ActThree
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private float targetSize;
        [SerializeField] private float showDistance;
        [SerializeField] private Transform teleportLocation;
        [SerializeField] private Transform player;

        private bool _hidden;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                TeleportManager.Instance.Teleport(teleportLocation);

                var newPortal = Instantiate(gameObject, player.position - player.forward, Quaternion.identity);
                Destroy(newPortal.GetComponent<Portal>());
                Tween.Scale(newPortal.transform, Vector3.one * targetSize, Vector3.zero, 5f).OnComplete(() => Destroy(newPortal));
            }
        }

        private void FixedUpdate()
        {
            if (Vector3.Distance(player.position, transform.position) > showDistance)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

        private void Show()
        {
            if (!_hidden) return;
            
            Tween.Scale(transform, Vector3.zero, Vector3.one * targetSize, 1f);
            _hidden = false;
        }
        
        private void Hide()
        {
            if (_hidden) return;
            
            Tween.Scale(transform, Vector3.one * targetSize,Vector3.zero, 1f);
            _hidden = true;
        }
    }
}
