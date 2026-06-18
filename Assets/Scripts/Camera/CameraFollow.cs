using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    private float highestY;

    void Start()
    {
        highestY = transform.position.y; // NO usar player.position.y
    }

    void LateUpdate()
    {
        if (player.position.y > highestY)
        {
            highestY = player.position.y;

            transform.position = new Vector3(
                0f,
                highestY,
                -10f
            );
        }
    }
}