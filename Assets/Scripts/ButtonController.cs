using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


[Serializable]
public class MyEvent : UnityEvent<string,GameObject> {}

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

    public MyEvent OnButtonPressed;
    public MyEvent OnButtonReleased;

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other) {

        if(ColliderLayer.value != (ColliderLayer.value | (1 << other.gameObject.layer))) {
            return;
        }
        if(other == null) {
            //Debug.Log("OTHER IS NULL");
            return;
        }
        if(other.GetComponent<ScarfGrabber>() != null  ||  other.tag == "PlayerCollider") {
            //Debug.Log("COLLISION!");
            OnButtonPressed.Invoke("ButtonPressed",gameObject);
            spriteRenderer.sprite = spriteDown;
        } else {
            //Debug.Log("NOT PLAYER OR SCARF GRABBER");
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        
        if(ColliderLayer.value != (ColliderLayer.value | (1 << other.gameObject.layer))) {
            return;
        }
        if(other == null) {
            //Debug.Log("OTHER IS NULL");
            return;
        }
        if(other.GetComponent<ScarfGrabber>() != null || other.tag == "PlayerCollider") {
            //Debug.Log("COLLISION ENDED!");
            OnButtonReleased.Invoke("ButtonReleased",gameObject);
            spriteRenderer.sprite = spriteUp;
        } else {
            //Debug.Log("NOT PLAYER OR SCARF GRABBER ENDED");
        }
    }
}
