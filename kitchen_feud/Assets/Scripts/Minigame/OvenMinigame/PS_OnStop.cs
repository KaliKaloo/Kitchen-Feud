using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_OnStop : MonoBehaviour
{
    private ParticleSystem _ps;
 
 
    public void Start()
    {
        _ps = GetComponent<ParticleSystem>();
    }

    public void FixedUpdate()
    {
        if (_ps && !_ps.IsAlive())
        {
            
        }
    }
}