using UnityEngine;

public class arrow_walk : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 1.5f;
    public float rotationSpeed = 2.5f;
    void Start()
    {
        
    }


    // Update is called once per frame
        void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            transform.position += Vector3.back * speed * Time.deltaTime;
        }
    }

}