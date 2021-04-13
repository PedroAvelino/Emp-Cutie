using System;
using UnityEngine;

public class Enemy : PoolableObject, IDamageable
{
    [SerializeField] int _life = 1;
    [SerializeField] float _speed;

    int _baseLife; // used to reset life of the player


    private void Awake()
    {
        OnAwake();
        _baseLife = _life;
    }

    protected virtual void OnAwake()
    {
        
    }

    public virtual void OnDeath()
    {
       ReturnToPool();
    }

    public override void PrepareForUse()
    {
        _life = _baseLife;
    }

    public void OnHit(int damage)
    {
        if( _life <= 0 ) return; // Check if already dead


        _life -= damage;
        if( _life <= 0 )
        {
            OnDeath();
        }
    }
}
