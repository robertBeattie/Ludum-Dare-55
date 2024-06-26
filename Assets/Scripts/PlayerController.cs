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
    [SerializeField] Transform hatSprite;

    [Header("Scarf Grab Ability")]
    [SerializeField] bool isScarfGrabAbilityEnabled = true;
    [SerializeField] Transform scarf;
    [SerializeField] ScarfGrabber scarfGrabber;
 
    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        animator.AnimateMovement(moveInput);
        rb.velocity = moveInput * speed;


        if(Input.GetMouseButtonDown(0) && isCardAbilityEnabled && cardTimer <= cardCooldown){
            if(cardSpawnPoint == null || hatSprite == null) return;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos - (Vector2)cardSpawnPoint.position).normalized;
            hatSprite.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, (mousePos - (Vector2) hatSprite.position).normalized));
            GameObject card = Instantiate(Random.Range(0,2) == 1 ? redCardPrefab : blackCardPrefab, cardSpawnPoint.position, Quaternion.identity);
            card.GetComponent<Rigidbody2D>().velocity = direction * cardSpeed;

            cardTimer = cardCooldown;
        }
        cardTimer -= Time.deltaTime;

        if(Input.GetMouseButtonDown(1) && isScarfGrabAbilityEnabled){
            if(scarf == null) return;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            scarfGrabber.GrabScarf((mousePos - (Vector2)hatSprite.position).normalized);
        }
    }

    
}
