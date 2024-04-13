using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    string current_state;

    const string PLAYER_IDLE = "Player_Idle";
    const string PLAYER_WALK = "Player_Walk";

    public void SetState(string new_state){
        if(current_state == new_state) return;
        animator.Play(new_state);
        current_state = new_state;
    }

    public void AnimateMovement(Vector2 moveInput){
        if(moveInput.magnitude > 0){
            SetState("Player_Walk");
            if(moveInput.x > 0){
                spriteRenderer.flipX = false;
            } else {
                spriteRenderer.flipX = true;
            }
        } else {
            SetState("Player_Idle");
        }
    }

}
