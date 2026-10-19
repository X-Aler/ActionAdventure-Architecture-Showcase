using UnityEngine;

namespace ActTwo
{
    public enum Abilities
    {
        BombDrop,
        FlameBoy,
        HomingProjectile,
        Nevilette
    }
    public abstract class Ability : MonoBehaviour
    {
        public AbilityData data;
        public abstract void Shoot(Transform spawnPoint);

        public abstract void Special();
    }
}
