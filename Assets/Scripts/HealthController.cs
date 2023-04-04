using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    float maxiumHealth = 100.0F;

    float currentHealht = 0.0F;

    private void Start()
    {
        currentHealht = maxiumHealth;
    }

    public void TakeDamage(float value)
    {
        currentHealht -= Mathf.Abs(value);
        if(currentHealht <= 0.0F)
        {
            Destroy(gameObject);
        }
    }
}
