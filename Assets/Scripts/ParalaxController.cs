using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxController : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D target;

    [SerializeField]
    Vector2 speed;

    Vector2 offset;

    Material material;

     void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

     void Update()
    {
        offset = (target.velocity.x * 0.01F) * speed * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}
