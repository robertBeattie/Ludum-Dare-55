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
    [SerializeField] float speed = 5f;
    [SerializeField] float stoppingDistance = 1.5f;
    // Sight 
    [SerializeField] float sightDistance = 7f;
    [SerializeField] LayerMask layerMask; // obstacles

    Transform _transform;
    private void Awake() {
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(_transform.position, player.position);

        if(distance > sightDistance && playerLost)
        {
            return;
        }

        Vector2 direction = (player.position - _transform.position).normalized;
        Debug.DrawRay(_transform.position, direction * Mathf.Min(distance, sightDistance), Color.red);
        if(Physics2D.Raycast(_transform.position, direction, Mathf.Min(distance, sightDistance), layerMask))
        {
            // hit obstacle or wall
            float lastDistance = Vector2.Distance(_transform.position, lastPlayerPosition);
            if(lastDistance > .1f){
                MoveTowardsLastPlayerPosition();
            } else {
                playerLost = true;
            }
            return;
        }

        lastPlayerPosition = player.position;
        playerLost = false;

        if(distance > stoppingDistance){
            MoveTowardsPlayer();
        }else{
            Debug.Log("Player is in range to Catch!");
        }
        
    }

    void MoveTowardsPlayer(){
        _transform.position = Vector2.MoveTowards(_transform.position, player.position, speed * Time.deltaTime);
    }

    void MoveTowardsLastPlayerPosition(){
        _transform.position = Vector2.MoveTowards(_transform.position, lastPlayerPosition, speed * Time.deltaTime);
    }


    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, stoppingDistance);

        // Gizmos.color = Color.blue;
        // Gizmos.DrawLine(transform.position, player.position);

        // Gizmos.color = Color.cyan;
        // Gizmos.DrawRay(transform.position, (player.position - transform.position).normalized * sightDistance);

        // Gizmos.color = Color.yellow;
        // Gizmos.DrawLine(transform.position, lastPlayerPosition);

        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(lastPlayerPosition, .1f);
    }
}
