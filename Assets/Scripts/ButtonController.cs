using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    [SerializeField] GameObject player;
    [SerializeField] LayerMask ColliderLayer;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite spriteUp;
    [SerializeField] Sprite spriteDown;

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other) {

        if(ColliderLayer.value != (ColliderLayer.value | (1 << other.gameObject.layer))) {
            return;
        }

        if(other.transform.parent.gameObject.Equals(player) || other.gameObject.GetType().IsSubclassOf(typeof(ScarfGrabber)) ) {
            Debug.Log("COLLISION!");
            spriteRenderer.sprite = spriteDown;
        } else {
            Debug.Log("NOT PLAYER OR SCARF GRABBER");
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        
        if(ColliderLayer.value != (ColliderLayer.value | (1 << other.gameObject.layer))) {
            return;
        }
        
        if(other.transform.parent.gameObject.Equals(player) || other.gameObject.GetType().IsSubclassOf(typeof(ScarfGrabber)) ) {
            Debug.Log("COLLISION ENDED!");
            spriteRenderer.sprite = spriteUp;
        } else {
            Debug.Log("NOT PLAYER OR SCARF GRABBER ENDED");
        }
    }
}
