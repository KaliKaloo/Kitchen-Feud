using UnityEngine;

public class camera_follow : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public Vector3 offset;

    void Start()
    {

    }

    // Update is called once per frame
    void Update(){
        transform.position = player.position + offset;
    }
}