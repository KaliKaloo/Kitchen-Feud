using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnableSmoke
{
    public static bool inEnemyKitchen = false;
    public static GameObject smokeSlot;
    public static bool usedUp;

    public bool GetPlayerState()
    {
        return inEnemyKitchen;
    }

    public void SetUIForSmoke(GameObject slot)
    {
        smokeSlot = slot;
        smokeSlot.SetActive(false);
    }

    public void DisableSmokeSlot()
    {
        smokeSlot.SetActive(false);
    }

    public void ChangePlayerState(bool playerState)
    {
        if (!usedUp)
        {
            inEnemyKitchen = playerState;
            if (smokeSlot != null && playerState)
            {
                smokeSlot.SetActive(true);
            }
        }
    }

    public void UseSmoke()
    {
        usedUp = true;
    }

}

public class SmokeGrenade : MonoBehaviour
{
    public GameObject prefab;

    private ParticleSystem particle1;

    GameObject smokeClone;
    bool hasExploded = false;
    private bool started = false;

    readonly EnableSmoke enableSmoke = new EnableSmoke();

    // Start is called before the first frame update
    void Start()
    {
        //a
    }

    // Update is called once per frame
    void Update()
    {
        if (enableSmoke.GetPlayerState()) {
            if (Input.GetKeyDown(KeyCode.Alpha1) && !started) {
                started = true;
                enableSmoke.UseSmoke();
                Throw();
            } 

            // when smoke has finished animation destroy
            if (particle1 != null && hasExploded && started && !particle1.isEmitting)
            {
                //StartCoroutine(WaitForSmokeToDissipate());
            }
        }
    }

    [PunRPC]
    void AddSmokeForce(GameObject prefabObject)
    {
        prefabObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.back * 500); //Moving projectile
    }

    void Throw() {
        enableSmoke.DisableSmokeSlot();
        smokeClone = PhotonNetwork.Instantiate("smoke_grenade", transform.position, transform.rotation);

        //this.GetComponent<PhotonView>().RPC("AddSmokeForce", RpcTarget.All, smokeClone);
    }

    void Explode()
    {
        particle1.Play();
    }
}
