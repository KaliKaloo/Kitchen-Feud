using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ProjectileLifetime : MonoBehaviour
{
    [SerializeField] private GameObject particleObject;

    // Time until the smoke animation goes off after thrown
    public readonly float timeToExplode = 3;

    // Allows smoke to dissipate before destroying everything
    public readonly float timeToDestroyAfterFinish = 5;

    // On 
    private void Start()
    {
        ParticleSystem particleSystem = particleObject.GetComponent<ParticleSystem>();
        StartCoroutine(WaitForExplosion());

        // Destroy object after set amount of time
        Destroy(gameObject, particleSystem.main.duration);
    }

    IEnumerator WaitForExplosion()
    {
        // Push grenade in forward direction
        this.GetComponent<Rigidbody>().AddForce(transform.forward * 200);

        // Wait a bit before smoke appears
        yield return new WaitForSeconds(timeToExplode);

        // Start smoke animation
        gameObject.transform.GetChild(0).GetComponent<ParticleSystem>().Play();

        //SOUND ------------------------------------------------------
        gameObject.GetComponent<PhotonView>().RPC("PlaySmokeBombSound", RpcTarget.All);
        // -----------------------------------------------------------
    }

    [PunRPC]
    void PlaySmokeBombSound() {
        GetComponent<AudioSource>().Play();
    }
}
