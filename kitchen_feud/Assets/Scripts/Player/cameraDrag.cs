using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraDrag : MonoBehaviour
{
    public float speed = 5.0f;
    private float pitch = 0.0f;
    private Quaternion currentRotation;

    void LateUpdate()
    {
         if (Input.GetMouseButton(1))
        {
            pitch -= speed * Input.GetAxis("Mouse Y");
            pitch = Mathf.Clamp(pitch, -90f, 48f);
            currentRotation.eulerAngles = new Vector3(pitch, 0.0f, 0.0f);
            transform.localRotation = currentRotation;
        }

        if (Input.GetKey(KeyCode.R)){
            transform.localRotation = Quaternion.identity;
        }
    }
}