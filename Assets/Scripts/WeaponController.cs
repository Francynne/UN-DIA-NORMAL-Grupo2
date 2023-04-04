using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    Transform firePoint;

    [SerializeField]
    GameObject bulletPrefab;

    [SerializeField]
    float bulletLifeTime = 5.0F;

    [SerializeField]
    float fireTimesPerSecond = 3.0F;

    Animator animator;

    float currentTime = 0.0F;
    float nextFireTime = 0.0F;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

     void Update()
    {
        currentTime += Time.deltaTime;

        if (Input.GetButton("Fire1"))
        {
            if(currentTime > nextFireTime)
            {
                currentTime = 0.0F;
                nextFireTime = Time.deltaTime + (1.0F / fireTimesPerSecond);

                animator.SetTrigger("fire");
            }
        }
    }

    public void OnFire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        Destroy(bullet, bulletLifeTime);
    }
}
