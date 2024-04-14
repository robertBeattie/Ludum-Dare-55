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

    [Header("Jump Into Hat Ability")]
    [SerializeField] bool isJumpIntoHatAbilityEnabled = true;
    [SerializeField] GameObject rainbowPrefab;
    [SerializeField] Transform hatTransform;
    [SerializeField] float rainbowSpeed = 5f;


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

        if(Input.GetMouseButtonDown(2) && isJumpIntoHatAbilityEnabled){
            if(hatTransform == null) return;
            GameObject rainbow = Instantiate(rainbowPrefab, transform.position, Quaternion.identity);

            // Calculate the direction towards the hat
            Vector3 direction = (hatTransform.position - transform.position).normalized;
            
            // Rotate the rainbow to align with the direction
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rainbow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Move the rainbow towards the hat
            rainbow.GetComponent<Rigidbody2D>().velocity = direction * rainbowSpeed;
            
            //Make Bunny Invisible

            // Destroy the rainbow after reaching the hat
            Destroy(rainbow, Vector3.Distance(transform.position, hatTransform.position) / rainbowSpeed);
        }
    }


}
