using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardProjectile : MonoBehaviour
{
    [SerializeField] LayerMask ColliderLayer;
    void Start()
    {
        Destroy(gameObject, 5f);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with " + other.name);

        if(ColliderLayer.value == (ColliderLayer.value | (1 << other.gameObject.layer)))
        {
            if(other.gameObject.CompareTag("Enemy")){
                other.GetComponent<FlashEffect>().Flash();
            }
            Destroy(gameObject);
        }
    }
}
