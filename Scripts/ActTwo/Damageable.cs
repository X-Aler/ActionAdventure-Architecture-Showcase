
using Controllers;

namespace ActTwo
{
    public interface IDamageable
    {
        public void Damage(HealthController healthController, float damage);
    }
}