using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    [SerializeField]
    string collectibleType;

    [SerializeField]
    int value;

     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Character2DController controller = other.GetComponent<Character2DController>();
            if(controller != null)
            {
                //LLevar el contao de estrellas y mostrarlo y recolectar la llave
                controller.IncraseCollectible(collectibleType, value);

            }
            Destroy(gameObject);
        }
    }
}
