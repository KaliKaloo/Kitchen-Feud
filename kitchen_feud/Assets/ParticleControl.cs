using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleControl : MonoBehaviour
{
    //public List<GameObject> particles = new List<GameObject>();
    // Start is called before the first frame update
    ParticleSystem psParent;
    void Start()
    {
        psParent = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    public void PlayPS()
    {
        psParent.Play();
    }
}
