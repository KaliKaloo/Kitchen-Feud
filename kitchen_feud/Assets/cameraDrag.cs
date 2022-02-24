using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraDrag : MonoBehaviour
{
    float speed = 5.0f, yaw = 0.0f, pitch = 0.0f;
    private Quaternion currentRotation;

    void LateUpdate()
    {
         if (Input.GetMouseButton(1)) //start dragging
        {
            yaw += speed * Input.GetAxis("Mouse X");
            pitch -= speed * Input.GetAxis("Mouse Y");
            currentRotation.eulerAngles = new Vector3(pitch, yaw, 0.0f);
            transform.localRotation = currentRotation;
        }

        if (Input.GetMouseButton(2)){
            transform.localRotation = Quaternion.identity;
        }
    }
}