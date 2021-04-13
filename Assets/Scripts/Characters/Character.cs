using System;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected int _life;
    [SerializeField] protected float _speed = 5f;

    [SerializeField] PoolableObject bulletPrefab;

    [SerializeField] Transform _bulletSpawn;

    protected PlayerType player;

    Rigidbody2D _rb;

    Vector2 shootDir = Vector2.zero;

    [SerializeField] float shootInterval = .2f;
    float lastShot;
    private void Awake()
    {
        OnAwake();
        InitializeControls();
    }


    protected virtual void OnAwake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        lastShot += Time.deltaTime;
        if( Input.GetKey(KeyCode.Z) )
        {
            if( lastShot >= shootInterval )
            {
                ShootBullet();
                lastShot = 0f;
            }
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if( _rb == null ) return;


        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        var dir = new Vector2( h, v ).normalized;
        


        _rb.velocity = dir * _speed * Time.deltaTime;

        if( dir != Vector2.zero )
        {
            MoveShootSpawn( new Vector2( h, v) );
            shootDir = dir;
        }
    }

    private void MoveShootSpawn( Vector2 direction )
    {
        float movePosition = 0.7f;

        if( direction == Vector2.zero )
        {
            _bulletSpawn.localPosition = new Vector2( 0f , -movePosition ); 
            return;
        }

        _bulletSpawn.localPosition = direction * movePosition;
    }

    void ShootBullet()
    {
        if( bulletPrefab == null ||  _bulletSpawn == null ) return;

        Bullet bullet = Pooler.GetObject(bulletPrefab, _bulletSpawn.position, Quaternion.identity) as Bullet;
        if( bullet == null ) return;

        if( shootDir == Vector2.zero)
        {
            bullet.travelDirection = Vector2.down;
            return;
        }

        bullet.travelDirection = shootDir;
    }

    private void InitializeControls()
    {
        //TODO: Initialize Player inputs
    }
}

public enum PlayerType
{
    Player1,
    Player2
}
