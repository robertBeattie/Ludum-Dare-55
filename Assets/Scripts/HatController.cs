using UnityEngine;

public class HatController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform playerTransform;
    [SerializeField] float stoppingDistance = 1.5f;
    [SerializeField] float speed = 5f;


    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
    }
    // Update is called once per frame
    void Update()
    {
        if(playerTransform == null)
        {
            return;
        }
        if(Vector2.Distance(transform.position, playerTransform.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        }

        if(Input.GetButtonDown("Jump")) {
            Debug.Log("Jumping " +  (player.activeInHierarchy ? "into" : "out of") + " hat" );
            player.SetActive(!player.activeInHierarchy);
        }
    }
}
