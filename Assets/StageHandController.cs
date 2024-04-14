using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StageHandController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector2 lastPlayerPosition;
    bool playerLost = true;
    //Movement
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed = 5f;
    [SerializeField] float stoppingDistance = 1.5f;
    // Sight 
    [SerializeField] float sightDistance = 7f;
    [SerializeField] LayerMask layerMask; // obstacles

    Transform _transform;
    private void Awake() {
        _transform = transform;
    }

    int i = 0;
    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(_transform.position, player.position);

        if(distance > sightDistance && playerLost)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        Vector2 direction = (player.position - _transform.position).normalized;
        Debug.DrawRay(_transform.position, direction * Mathf.Min(distance, sightDistance), Color.red);
        if(Physics2D.Raycast(_transform.position, direction, Mathf.Min(distance, sightDistance), layerMask) || distance > sightDistance)
        {
            // hit obstacle or wall
            LostPlayer();
            return;
        }



        lastPlayerPosition = player.position;
        playerLost = false;

        if(distance > stoppingDistance){
            MoveTowards(direction);
        }else{
            MoveTowards(Vector2.zero);
            Debug.Log("Player is in range to Catch!");
        }
        
    }

    void FixedUpdate(){
        int directions = 8;
        float angle = i * 360/directions;
        Vector2 rayDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        Debug.DrawRay(transform.position, rayDirection * 2f, Color.magenta);
        i++;
        if(i >= directions) 
        {
            i = 0;
        }
    }

    void MoveTowards(Vector2 direction){
        //_transform.position = Vector2.MoveTowards(_transform.position, player.position, speed * Time.deltaTime);
        //_transform.position = Vector2.MoveTowards(_transform.position, lastPlayerPosition, speed * Time.deltaTime);'
        if(direction == Vector2.zero){
            rb.velocity = Vector2.zero;
            return;
        }
        direction = MoveAwayFromWalls(direction);
        rb.velocity = direction * speed;
    }

    // Ray cast out radialy 
    // if hit wall move the direction vector away from the wall porportional to the distance to the wall
    Vector2 MoveAwayFromWalls(Vector2 direction)
    {
        Debug.DrawRay(transform.position, direction * 3.5f, Color.yellow);
        float rayDistance = 2.0f; // Set this to the maximum distance you want to check for walls
        for (int i = 0; i < 8; i++)
        {
            float angle = i * 45;
            Vector2 rayDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, rayDistance);
            Debug.DrawRay(transform.position, rayDirection * rayDistance/2f, Color.green);
            if (hit.collider != null)
            {
                // We hit a wall
                float distanceToWall = hit.distance;
                Vector2 awayFromWall = (Vector2)transform.position - hit.point; // Vector pointing away from the wall
                awayFromWall.Normalize(); // Make the vector a unit vector

                // Adjust the direction vector based on the distance to the wall
                // The closer we are to the wall, the more we adjust the direction vector
                direction += awayFromWall * (rayDistance - distanceToWall) / rayDistance;
                direction.Normalize(); // Make the direction vector a unit vector
            }
        }
        Debug.DrawRay(transform.position, direction * 3, Color.blue);
        return direction;
    }
    void LostPlayer(){
        float lastDistance = Vector2.Distance(_transform.position, lastPlayerPosition);
        if(lastDistance > .1f){
            MoveTowards((lastPlayerPosition - (Vector2)_transform.position).normalized);
        } else {
            playerLost = true;
            MoveTowards(Vector2.zero);
        }
    }
    private void OnDrawGizmosSelected() {
        // Gizmos.color = Color.red;
        // Gizmos.DrawWireSphere(transform.position, sightDistance);

        // Gizmos.color = Color.green;
        // Gizmos.DrawWireSphere(transform.position, stoppingDistance);

        // // Gizmos.color = Color.blue;
        // // Gizmos.DrawLine(transform.position, player.position);

        // // Gizmos.color = Color.cyan;
        // // Gizmos.DrawRay(transform.position, (player.position - transform.position).normalized * sightDistance);

        // // Gizmos.color = Color.yellow;
        // // Gizmos.DrawLine(transform.position, lastPlayerPosition);

        // Gizmos.color = Color.magenta;
        // Gizmos.DrawSphere(lastPlayerPosition, .1f);
    }
}
