using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Managers;
using UnityEngine;

namespace ActOne
{
    public class DamageOverTimeController : MonoBehaviour
    {
        [SerializeField] private AudioClip damageSound;
        [SerializeField] private float damage;
        [SerializeField] private HealthController healthController;
        public void Enter()
        {
            AudioManager.Instance.PlayOneShot(damageSound);
            healthController.TakeDamage(damage);
        }
        
    }
}
