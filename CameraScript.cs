using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            transform.LookAt(player.position);
        }
    }
}
