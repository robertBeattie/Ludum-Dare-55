using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;

    public void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag == "Player") {
            Instantiate(objectToSpawn, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
