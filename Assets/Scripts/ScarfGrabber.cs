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
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] LineRenderer lineRenderer;

    [SerializeField] int LinkCount = 3;
    float LinkLength = 0.5f;
    bool isGrabbing = false;

    // Start is called before the first frame update
    private void Awake() {
        scarf = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGrabbing) return;

        float maxLength = LinkCount * LinkLength;
        float distance = Vector2.Distance(scarf.position, hat.position);

        if(distance <= maxLength){
            glove.localRotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.down, (hat.position - scarf.position).normalized));
            hatSprite.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, (scarf.position - hat.position).normalized));
            lineRenderer.SetPositions(new Vector3[]{glovePoint.position, hat.position});
        }else{
            lineRenderer.gameObject.SetActive(false);
            spriteRenderer.enabled = false;
            isGrabbing = false;
        }
        
        
    }

    public void GrabScarf(){
        isGrabbing = true;
        lineRenderer.gameObject.SetActive(true);
        spriteRenderer.enabled = true;
    }
}
