using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] PlayerAnimator animator;
    [SerializeField] float speed = 5f;

    [Header("Card Ability")]
    [SerializeField] bool isCardAbilityEnabled = true;
    [SerializeField] float cardTimer = 0;
    [SerializeField] float cardCooldown = 0.5f;
    [SerializeField] Transform cardSpawnPoint;
    [SerializeField] GameObject redCardPrefab;
    [SerializeField] GameObject blackCardPrefab;
    [SerializeField] float cardSpeed = 10f;

    [Header("Scarf Grab Ability")]
    [SerializeField] bool isScarfGrabAbilityEnabled = true;
    [SerializeField] Transform scarf;

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        animator.AnimateMovement(moveInput);
        rb.velocity = moveInput * speed;


        if(Input.GetMouseButtonDown(0) && isCardAbilityEnabled && cardTimer <= cardCooldown){
            if(cardSpawnPoint == null) return;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos - (Vector2)cardSpawnPoint.position).normalized;
            
            GameObject card = Instantiate(Random.Range(0,2) == 1 ? redCardPrefab : blackCardPrefab, cardSpawnPoint.position, Quaternion.identity);
            card.GetComponent<Rigidbody2D>().velocity = direction * cardSpeed;

            cardTimer = cardCooldown;
        }
        cardTimer -= Time.deltaTime;

        if(Input.GetMouseButtonDown(1) && isScarfGrabAbilityEnabled){
            if(scarf == null) return;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            scarf.position = mousePos;
        }

        if(Input.GetButtonDown("Jump")){
            Debug.Log("Jumping into Hat");
        }
    }
}
