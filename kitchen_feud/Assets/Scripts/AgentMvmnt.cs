using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMvmnt : MonoBehaviour
{
        public Vector3 oldPos;
        public Quaternion oldRot;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 movement = (oldRot * (transform.position - oldPos));
 
        {
            if (movement.z > 0)
            {
                animator.SetBool("IsMovingForwards", true);
                // forward
            }
            else if (movement.z < 0)
            {
                animator.SetBool("IsMovingBackwards", true);
                // backwards
            }
            if (movement.x > 0)
            {
                // right
            }
            else if (movement.x < 0)
            {
                // left
            }
            else
            {
             
                if (animator.GetBool("IsMovingBackwards"))
                    animator.SetBool("IsMovingBackwards", false);
                // disable forwards
                else if (animator.GetBool("IsMovingForwards"))
                    animator.SetBool("IsMovingForwards", false);
            }
        }
        oldPos = transform.position;
        oldRot = transform.rotation;
    }
}
