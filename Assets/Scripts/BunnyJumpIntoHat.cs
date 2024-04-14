using UnityEngine;

public class BunnyJumpIntoHat : MonoBehaviour
{
    public Transform hatTransform; // Reference to the hat's transform
    public GameObject rainbowPrefab; // Reference to the rainbow prefab
    public float rainbowSpeed = 5f; // Speed of the rainbow animation

    private bool isJumping = false; // Flag to track if the bunny is currently jumping

    void Update()
    {
        // Check for input to trigger the jump into the hat
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            JumpIntoHat();
        }
    }

    void JumpIntoHat()
    {
        isJumping = true; // Set jumping flag to true

        // Instantiate the rainbow prefab
        GameObject rainbow = Instantiate(rainbowPrefab, transform.position, Quaternion.identity);

        // Calculate the direction towards the hat
        Vector3 direction = (hatTransform.position - transform.position).normalized;

        // Move the rainbow towards the hat
        rainbow.GetComponent<Rigidbody2D>().velocity = direction * rainbowSpeed;

        // // Deactivate the bunny sprite renderer
        // GetComponent<SpriteRenderer>().enabled = false;

        // // Deactivate the bunny collider
        // GetComponent<Collider2D>().enabled = false;

        // // Deactivate the bunny Rigidbody2D
        // GetComponent<Rigidbody2D>().simulated = false;

        // // Deactivate the bunny jump script
        // enabled = false;

        // Destroy the rainbow after reaching the hat
        Destroy(rainbow, Vector3.Distance(transform.position, hatTransform.position) / rainbowSpeed);
    }

    public void ReappearNextToHat()
    {
        // Reactivate the bunny sprite renderer
        GetComponent<SpriteRenderer>().enabled = true;

        // Reactivate the bunny collider
        GetComponent<Collider2D>().enabled = true;

        // Reactivate the bunny Rigidbody2D
        GetComponent<Rigidbody2D>().simulated = true;

        // Reactivate the bunny jump script
        enabled = true;

        // Set the position of the bunny next to the hat
        transform.position = hatTransform.position + Vector3.right;
        
        // Reset jumping flag
        isJumping = false;
    }
}
