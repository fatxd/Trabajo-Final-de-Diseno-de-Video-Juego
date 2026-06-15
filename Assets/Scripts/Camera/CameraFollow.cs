using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 3f;

    private float highestY;

    void Start()
    {
        highestY = transform.position.y;
    }

    void LateUpdate()
    {
        if (target.position.y > highestY)
        {
            highestY = target.position.y;

            Vector3 targetPos = new Vector3(
                transform.position.x,
                highestY,
                transform.position.z
            );

            transform.position = Vector3.Lerp(
                transform.position,
                targetPos,
                smoothSpeed * Time.deltaTime
            );
        }
    }
}