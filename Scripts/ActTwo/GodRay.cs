using System.Collections;
using PrimeTween;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ActTwo
{
    public class GodRay : MonoBehaviour
    {
        private Vector3 _size;
        private GameObject _zone;
        private float _moveSpeed;
        private float _sizeTime;
        private float _rayTime;

        private bool _moving;

        private Vector3 _point;
        private Vector3 _direction;

        public void Initialize(Vector3 size, float sizeTime, GameObject zone, float damage, float attackSpeed, float moveSpeed, float rayTime, GameObject owner)
        {
            _size = size;
            _sizeTime = sizeTime;
            _zone = zone;
            _moveSpeed = moveSpeed;
            _rayTime = rayTime;

            var bullet = GetComponent<StaticBullet>();
            bullet.Initialize(damage, attackSpeed, owner);

            StartCoroutine(Activate());
        }

        private IEnumerator Activate()
        {
            Tween.Scale(transform, _size, _sizeTime);
            yield return new WaitForSeconds(_rayTime + _sizeTime);
            Tween.Scale(transform, Vector3.zero, _sizeTime).OnComplete(() => Destroy(gameObject, 0.1f));
        }

        private void Update()
        {
            if (!_moving)
            {
                _point = GetRandomPositionInZone(_zone);
                _point.y = transform.position.y;
            }
            
            var distance = Vector3.Distance(transform.position, _point);
            
            _moving = distance > 1;
            
            if (_moving)
            {
                _direction = (_point - transform.position).normalized;
                transform.Translate(_direction * _moveSpeed * Time.deltaTime);
            }
        } 
        
        private Vector3 GetRandomPositionInZone(GameObject zone)
        {
            var center = zone.transform.position;
            var size = zone.transform.localScale;

            var x = Random.Range(-size.x / 2, size.x / 2);
            var z = Random.Range(-size.z / 2, size.z / 2);

            return center + zone.transform.TransformDirection(new Vector3(x, 0, z));
        }
    }
}
