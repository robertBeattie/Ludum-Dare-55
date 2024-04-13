using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarfGrabber : MonoBehaviour
{
    Transform scarf;
    [SerializeField] Transform hat;
    [SerializeField] Transform glove;
    [SerializeField] LineRenderer lineRenderer;

    // Start is called before the first frame update
    private void Awake() {
        scarf = transform;
    }

    // Update is called once per frame
    void Update()
    {
        glove.localRotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.down, (hat.position - scarf.position).normalized));
        lineRenderer.SetPositions(new Vector3[]{scarf.position, hat.position});
    }
}
