using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private readonly float delay = 3f;

    float countdown;

    private ParticleSystem particle1;

    GameObject smokeClone;
    bool hasExploded = false;
    private bool started = false;

    readonly EnableSmoke enableSmoke = new EnableSmoke();

    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        if (enableSmoke.GetPlayerState()) {
            if (Input.GetKeyDown(KeyCode.Alpha1) && !started) {
                started = true;
                enableSmoke.UseSmoke();
                Throw();
            } else if (started && !hasExploded) {
                countdown -= Time.deltaTime;
            }


            if (countdown <= 0 && !hasExploded)
            {
                Explode();
                hasExploded = true;
            }

            // when smoke has finished animation destroy
            if (particle1 != null && hasExploded && started && !particle1.isEmitting)
            {
                StartCoroutine(WaitForSmokeToDissipate());
            }
        }
    }

    // Destroys game object after some time to allow smoke to dissipate
    IEnumerator WaitForSmokeToDissipate()
    {
        //Wait for 5 seconds
        yield return new WaitForSeconds(5);
        Destroy(smokeClone);
    }

    void Throw() {
        enableSmoke.DisableSmokeSlot();
        smokeClone = Instantiate(prefab, transform.position, transform.rotation);
        particle1 = smokeClone.transform.GetChild(0).GetComponent<ParticleSystem>();
        smokeClone.GetComponent<Rigidbody>().AddRelativeForce(Vector3.back * 500); //Moving projectile
    }

    void Explode()
    {
        particle1.Play();
    }
}
