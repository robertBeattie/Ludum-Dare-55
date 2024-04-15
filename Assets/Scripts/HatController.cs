using UnityEngine;
using Cinemachine;
public class HatController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform playerTransform;
    [SerializeField] float stoppingDistance = 1.5f;
    [SerializeField] float speed = 5f;

    [HideInInspector] public bool isGrabbed = false;
    [SerializeField] CinemachineVirtualCamera vcam;
    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump") && playerTransform != null) {
            Debug.Log("Jumping " +  (player.activeInHierarchy ? "into" : "out of") + " hat" );
            player.SetActive(!player.activeInHierarchy);
            player.transform.position = transform.position;
            vcam.Follow = player.activeInHierarchy ? player.transform : transform;
        }

        if(playerTransform == null || player.activeInHierarchy == false || isGrabbed)
        {
            return;
        }
        if(Vector2.Distance(transform.position, playerTransform.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        }

        
    }
}
