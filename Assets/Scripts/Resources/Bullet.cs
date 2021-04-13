using System;
using UnityEngine;

public class Bullet : PoolableObject
{
    [SerializeField] float _travelSpeed = 5f;

    [SerializeField] LayerMask collisionMask;

    public Vector2 travelDirection;
    Rigidbody2D rb;
    [SerializeField] int _damage = 1; //We can change this later

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        TravelBullet();
    }

    private void TravelBullet()
    {
        if( travelDirection == Vector2.zero ) return;

        rb.velocity = travelDirection * _travelSpeed * Time.deltaTime;
    }

    public override void PrepareForPool()
    {
        travelDirection = Vector2.zero;
        rb.velocity = Vector2.zero;
    }

    private void OnBecameInvisible()
    {
        ReturnToPool();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        var otherLayer = 1 << other.gameObject.layer;
        if( otherLayer != collisionMask ) return;

        IDamageable hit = other.gameObject.GetComponent(typeof(IDamageable)) as IDamageable;
        if( hit == null ) return;

        hit.OnHit( _damage);
        ReturnToPool();
    }
}
