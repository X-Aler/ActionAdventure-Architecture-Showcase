using System;
using System.Collections.Generic;
using UnityEngine;

namespace ActTwo
{
    public class Attraction : MonoBehaviour
    {
        private float _attractStrength;
        private float _attractDistance;
        private GameObject _owner;

        public event Action<GameObject> AttractedEnd;
        
        private List<GameObject> _attractingObjects = new List<GameObject>();

        public void Initialize(float attractStrength, float attractDistance, GameObject owner)
        {
            _attractStrength = attractStrength;
            _attractDistance = attractDistance;
            _owner = owner;
        }

        private void Update()
        {
            if (_attractingObjects.Count == 0) return;

            for (var i = _attractingObjects.Count - 1; i >= 0; i--)
            {
                var attractObject = _attractingObjects[i];
                if (!attractObject)
                {
                    _attractingObjects.RemoveAt(i);
                    continue;
                }

                var distance = Vector3.Distance(transform.position, attractObject.transform.position);

                if (distance > _attractDistance)
                {
                    Attract(attractObject);
                }
                else
                {
                    _attractingObjects.RemoveAt(i);
                    
                    Attracted(attractObject);
                }
            }
        }

        private void Attracted(GameObject attractObject)
        {
            AttractedEnd?.Invoke(attractObject);
        }


        private void Attract(GameObject target)
        {
            target.transform.position = Vector3.MoveTowards(target.transform.position, transform.position, _attractStrength * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (IsOwnerPart(other.gameObject)) return;

            if (_attractingObjects.Contains(other.gameObject)) return;
    
            _attractingObjects.Add(other.gameObject);

            if (other.TryGetComponent(out Enemy enemy))
                enemy.stunned = true;
        }

        private bool IsOwnerPart(GameObject obj)
        {
            return obj == _owner || obj.transform.IsChildOf(_owner.transform);
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (IsOwnerPart(other.gameObject)) return;

            if (other.TryGetComponent(out Enemy enemy))
                enemy.stunned = false;
        }
    }
}