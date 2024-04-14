using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScarfGrabber : MonoBehaviour
{
    Transform scarf;
    [SerializeField] Transform hat;
    [SerializeField] Transform hatSprite;
    [SerializeField] Transform glove;
    [SerializeField] Transform glovePoint;
    [SerializeField] SpriteRenderer spriteHandRenderer;
    [SerializeField] LineRenderer lineRenderer;

    [SerializeField] int LinkCount = 3;
    float LinkLength = 0.5f;
    [SerializeField] Sprite GrabSprite;
    [SerializeField] Sprite OpenSprite;

    [SerializeField] private Vector2 aimDirection;
    [SerializeField] LayerMask ColliderLayer;

    [SerializeField] private ScarfState state = ScarfState.None;
    enum ScarfState{
        None,
        Grabbing,
        Extending,
        Retracting,
        Done
    }
    // Start is called before the first frame update
    private void Awake() {
        scarf = transform;
    }

    [SerializeField] float distance;
    // Update is called once per frame
    void Update()
    {
        if(state == ScarfState.None) return;

        float maxLength = LinkCount * LinkLength;
        distance = Vector2.Distance(glove.position, hat.position);

        switch (state)
        {
            case ScarfState.Grabbing:
                if(distance > maxLength)
                    state = ScarfState.Retracting;
                break;
            case ScarfState.Extending:
                float extendSpeed = 3f;
                //Vector2.MoveTowards(transform.position, aimPosition, extendSpeed * Time.deltaTime);
                transform.Translate(aimDirection * extendSpeed * Time.deltaTime);
                if(distance > maxLength)
                    state = ScarfState.Retracting;
                break;
            case ScarfState.Retracting:
                float retractSpeed = 6f;
                //Vector2.MoveTowards(transform.position, hatSprite.position, retractSpeed * Time.deltaTime);
                Vector2 direction = (hatSprite.position - glove.position).normalized;
                transform.Translate(direction * retractSpeed * Time.deltaTime);
                spriteHandRenderer.sprite = OpenSprite;
                if(distance <= 0.45f)
                    state = ScarfState.Done;
                break;
            case ScarfState.Done:
                lineRenderer.enabled = false;
                spriteHandRenderer.enabled = false;
                state = ScarfState.None;
                return;
            default:
                return;
        }

        RenderScarf();    
    }

    void RenderScarf(){
        glove.localRotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.down, (hat.position - scarf.position).normalized));
        hatSprite.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, (scarf.position - hat.position).normalized));
        lineRenderer.SetPositions(new Vector3[]{glovePoint.position, hat.position});
    }

    void OnTriggerEnter2D(Collider2D other){
        Debug.Log($"other collide with :{other.name}");
        if(ColliderLayer.value == (ColliderLayer.value | (1 << other.gameObject.layer)))
        {
            Debug.Log("grab");
            state = ScarfState.Grabbing;

            spriteHandRenderer.sprite = GrabSprite;
        }
    }

    public void GrabScarf(Vector2 aimDir){
        if(state != ScarfState.None){
            state = ScarfState.Retracting;
            return;
        }
        state = ScarfState.Extending;
        scarf.transform.position = hatSprite.transform.position;

        lineRenderer.enabled = true;
        spriteHandRenderer.enabled = true;
        spriteHandRenderer.sprite = OpenSprite;
        aimDirection = aimDir;
    }
}
