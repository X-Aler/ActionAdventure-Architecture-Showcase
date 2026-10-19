using UnityEngine;
namespace ActThree
{
    public class FlyArrow : MonoBehaviour
    {
        public Arrows arrow;

        private float _speed;
        private Transform _target;
        private Vector3 _direction;

        public void Init(float speed, Transform target)
        {
            _speed = speed;
            _target = target;
            _direction = (_target.position - transform.position).normalized;
        }

        private void Update()
        {
            if (_direction == Vector3.zero) return;
            transform.Translate(_direction * _speed * Time.deltaTime, Space.World);
        }
    }
}
