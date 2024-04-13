using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    string current_state;

    const string PLAYER_IDLE1 = "Bunny_Idle";
    const string PLAYER_IDLE2 = "Bunny_Idle2";
    const string PLAYER_IDLE3 = "Bunny_Idle3";
    const string PLAYER_WALK = "Bunny_Walk2";
    const string PLAYER_WALK2 = "Bunny_Walk";

    public void SetState(string new_state){
        if(current_state == new_state) return;
        animator.Play(new_state);
        current_state = new_state;
    }

    public void AnimateMovement(Vector2 moveInput){
        if(moveInput.magnitude > 0){
            SetState(PLAYER_WALK);
            if(moveInput.x > 0){
                spriteRenderer.flipX = false;
            } else {
                spriteRenderer.flipX = true;
            }
        } else {
            if(current_state != PLAYER_WALK) return;
            PickRandomIdle();
        }
    }

    [SerializeField] float idleTimer = 0;
    [SerializeField] float idleTimeMax = 2.1f;
    void PickRandomIdle(){
        Debug.Log("Picking random idle");
        int RandomIdle = Random.Range(0, 3);
        if(RandomIdle == 0){
            SetState(PLAYER_IDLE1);
        } else if(RandomIdle == 1){
            SetState(PLAYER_IDLE2);
        } else {
            SetState(PLAYER_IDLE3);
        }
    }
    void Update(){
        if(current_state != PLAYER_WALK){
            if(idleTimer >= idleTimeMax){
                PickRandomIdle();
                idleTimer = 0;
            }
            idleTimer += Time.deltaTime;
        }
    }
}
