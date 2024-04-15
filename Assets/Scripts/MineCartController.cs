using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCartController : MonoBehaviour
{
    [SerializeField] List<Transform> path;
    [SerializeField] float speed = 7f;
    [SerializeField] float stoppingDistance = .2f;
    private int currentWaypointIndex = 0;

    [SerializeField] SpriteRenderer spriteRenderer;
    bool isSide = false;
    [SerializeField] Sprite spriteUp;
    [SerializeField] Sprite spriteSide;

    // Update is called once per frame
    void Update()
    {
        if(path == null || path.Count == 0)
        {
            return;
        }
        if(Vector2.Distance(transform.position, path[currentWaypointIndex].position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, path[currentWaypointIndex].position, speed * Time.deltaTime);
        }
        else
        {
            currentWaypointIndex++;
            if(currentWaypointIndex >= path.Count)
            {
                if(transform.childCount > 0){
                    transform.GetChild(0).GetComponent<HatController>().isGrabbed = false;
                    transform.GetChild(0).parent = null;
                }
                Destroy(gameObject);
            }else{
                spriteRenderer.sprite = isSide ? spriteUp : spriteSide;
                isSide = !isSide;
            }
        }
    }

    public void SetPath(List<Transform> path)
    {
        this.path = path;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Collision with " + other.tag);
        if(other.tag == "Hat") {
            other.transform.parent = transform;
            other.transform.position = transform.position;
            other.GetComponent<HatController>().isGrabbed = true;
        }
    }
}
