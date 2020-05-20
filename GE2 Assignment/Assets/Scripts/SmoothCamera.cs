
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;
    private Space offsetPositionSpace = Space.Self;
    private bool lookAt = true;

    private void FixedUpdate()
    {
        /*Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target);*/

        if (offsetPositionSpace == Space.Self)
        {
            transform.position = target.TransformPoint(offset);
        }
        else
        {
            transform.position = target.position + offset;
        }

        // compute rotation
        if (lookAt)
        {
            transform.LookAt(target);
        }
        else
        {
            transform.rotation = target.rotation;
        }
    }
}
