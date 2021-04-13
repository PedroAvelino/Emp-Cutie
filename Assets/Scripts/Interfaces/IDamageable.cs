using UnityEngine;

public interface IDamageable
{
    void OnHit( int damage);

    void OnDeath();
}