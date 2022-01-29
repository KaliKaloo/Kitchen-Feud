using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public WASD_walk movement;

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Player")
        {
            Debug.Log("Collision");
           //movement.enabled = false;
        }
    }
}
