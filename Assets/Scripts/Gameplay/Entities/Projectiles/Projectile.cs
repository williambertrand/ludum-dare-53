using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;
    public float maxLifeTime;
    public Entity owner;

    public Rigidbody2D rigidbody2D;
    
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damagable = collision.gameObject.GetComponent<IDamageable>();
        
        if (damagable != null)
        {
            DamageData data = new DamageData();
            data.damageDealer = transform;
            data.target = collision.gameObject.transform;
            data.damageDealt = damage;
            damagable.TakeDamage(data);
        }
        Destroy(gameObject);
    }
}
