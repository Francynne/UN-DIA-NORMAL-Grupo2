using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuletController : MonoBehaviour
{
    [SerializeField]
    float speed = 25.0F;

    [SerializeField]
    float damage = 35.0F;

    [SerializeField]
    GameObject impactEffect;

    [SerializeField]
    float impactLifeTime = 0.40F;

    Rigidbody2D rb;

     void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

     void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        HealthController healthController = other.GetComponent<HealthController>();
        if(healthController != null)
        {
            healthController.TakeDamage(damage);
        }

        GameObject impact = Instantiate(impactEffect, transform.position, Quaternion.identity);
        Destroy(impact, impactLifeTime);
        Destroy(gameObject);
    }
}
