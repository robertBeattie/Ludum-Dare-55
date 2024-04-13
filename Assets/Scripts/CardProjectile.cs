using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardProjectile : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 5f);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with " + other.name);
        
    }
}
