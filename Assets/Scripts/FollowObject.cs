using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform Target;
    public float SmoothTime;

    private void FixedUpdate()
    {
        Vector2 smoothed2D = Vector2.Lerp(Target.position, transform.position, SmoothTime);
        transform.position = new Vector3(smoothed2D.x, smoothed2D.y, transform.position.z);
    }
}
