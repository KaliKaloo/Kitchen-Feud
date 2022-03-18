using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLifetime : MonoBehaviour
{
    public float timeToLive = 20;
    public float timeToExplode = 3;

    private void Start()
    {
        StartCoroutine(WaitForExplosion());
        Destroy(gameObject, timeToLive);
    }

    IEnumerator WaitForExplosion()
    {
        yield return new WaitForSeconds(timeToExplode);
        gameObject.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
    }
}
