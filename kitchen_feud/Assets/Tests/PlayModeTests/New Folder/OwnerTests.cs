using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Photon.Pun;
using System.IO;
using UnityEngine.AI;
using Hashtable = ExitGames.Client.Photon.Hashtable;


    public class OwnerTests:PhotonTestSetup
    {
    GameObject obj, cake;
    GameObject obj1;
    GlobalTimer timer;
    Owner o;
    Owner o1;

    Hashtable ht;
    PlayerHolding playerHold;
    Appliance sandwichStation;


    [UnitySetUp]
    public IEnumerator Setup()
    {


        timer = new GlobalTimer();
        obj = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers",
            "Player_cat_Model"),
            new Vector3(-1.98f, 0.006363153f, -8.37f),
            Quaternion.identity,
            0
        );
        cake = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs",
         "unCookedCake"),
         new Vector3(-1.98f, 0.006363153f, -8.37f),
         Quaternion.identity,
         0
     );

        obj.transform.position = new Vector3(12.61f, 0.2f, -4.8f); 
        ht = new Hashtable();
        ht["Team"] = 1;
        ht["CookedDishes"] = 0;
       ht["ViewID"] = obj.GetPhotonView().ViewID;
        PhotonNetwork.LocalPlayer.SetCustomProperties(ht);

        obj.tag = "Player";



        playerHold = obj.GetComponent<PlayerHolding>();
        yield return null;
    }

    [UnityTearDown]
    public IEnumerator MovementTearDown()
    {
        if (obj != null)
            PhotonNetwork.Destroy(obj);
        yield return null;
    }
    [UnityTest]
    public IEnumerator ownerCreated()
    {
        obj.transform.position = new Vector3(12.61f, 0.2f, -4.8f);
        ht["Team"] = 1;
        PhotonNetwork.LocalPlayer.SetCustomProperties(ht);
        //timer.changeTimerOnly(30);
        Debug.Log(timer.GetLocalTime());
       yield return new WaitForSeconds(1);
        Assert.IsTrue(GameObject.FindGameObjectsWithTag("Owner1").Length == 1); 
        Debug.Log(timer.GetLocalTime());
        
        o = GameObject.FindGameObjectWithTag("Owner1").GetComponent<Owner>();

        o1 = GameObject.FindGameObjectWithTag("Owner1").GetComponent<Owner>();


        o.GetComponent<NavMeshAgent>().ResetPath();
        o.GetComponent<NavMeshAgent>().enabled = false;
        obj.GetComponent<PlayerVoiceManager>().entered2 = false;
        obj.GetComponent<PlayerVoiceManager>().entered1 = true;
        yield return new WaitForSeconds(0.5f);
        o.transform.position = new Vector3(12.61f, 0.2f, -4.8f);
        o.GetComponent<NavMeshAgent>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        o.shout = true;
        yield return new WaitForSeconds(0.5f);
        o.currentlyTalking = false;
      //  yield return new WaitForSeconds(3);

        // yield return new WaitForSeconds(0.5f);
        yield return null;
    }

    [UnityTest]
    public IEnumerator throwSmokeBomb()
    {
        obj.transform.position = new Vector3(12.61f, 0.2f, -4.8f);
        ht["Team"] = 1;
        PhotonNetwork.LocalPlayer.SetCustomProperties(ht);
        //timer.changeTimerOnly(30);
        Debug.Log(timer.GetLocalTime());
        //  yield return new WaitForSeconds(1);
        Assert.IsTrue(GameObject.FindGameObjectsWithTag("Owner1").Length == 1);
        Debug.Log(timer.GetLocalTime());

        o = GameObject.FindGameObjectWithTag("Owner1").GetComponent<Owner>();

        o1 = GameObject.FindGameObjectWithTag("Owner2").GetComponent<Owner>();


        o.GetComponent<NavMeshAgent>().ResetPath();
        o.GetComponent<NavMeshAgent>().enabled = false;
        obj.GetComponent<PlayerVoiceManager>().entered2 = false;
        obj.GetComponent<PlayerVoiceManager>().entered1 = true;
        yield return new WaitForSeconds(0.2f);
        o.transform.position = new Vector3(12.61f, 0.2f, -4.8f);
        o.GetComponent<NavMeshAgent>().enabled = true;
        yield return new WaitForSeconds(0.4f);
        o.goThrowSmokeBomb = true;
        yield return new WaitForSeconds(0.4f);
        o1.currentlyShouting = false;
        o.currentlyShouting = false;
        o.GetComponent<NavMeshAgent>().enabled = false;
        o1.GetComponent<NavMeshAgent>().enabled = false;
        //yield return new WaitForSeconds(2);

        o.transform.position = new Vector3(-13.3f, 0.2f, 3.6f);
        o1.transform.position = new Vector3(6.743f, 0.2f, 2.076f);

        o.GetComponent<NavMeshAgent>().enabled = true;
        o1.GetComponent<NavMeshAgent>().enabled = true;
        yield return new WaitForSeconds(1f);
        o.GetComponent<NavMeshAgent>().enabled = false;
        o1.GetComponent<NavMeshAgent>().enabled = false;
        o.transform.position = new Vector3(12.61f, 0.2f, -4.8f);
        o1.transform.position = new Vector3(-6.363f, 0.2f, -7);
        o.GetComponent<NavMeshAgent>().enabled = true;
        o1.GetComponent<NavMeshAgent>().enabled = true;
        yield return new WaitForSeconds(1f);
        o1.returned = true;
        o.returned = true;
        o.GetComponent<NavMeshAgent>().enabled = false;
        o1.GetComponent<NavMeshAgent>().enabled = false;
        o.transform.position = o.spawnPoint;
        o1.transform.position = o1.spawnPoint;
        o.GetComponent<NavMeshAgent>().enabled = true;
        o1.GetComponent<NavMeshAgent>().enabled = true;
        yield return new WaitForSeconds(1f);



        // yield return new WaitForSeconds(0.5f);
        yield return null;
    }
    [UnityTest]
    public IEnumerator ownerCreated2()
    {

        obj.transform.position = new Vector3(-6.363f, 0.2f, -7);
        //timer.changeTimerOnly(30);
        Debug.Log(timer.GetLocalTime());
        yield return new WaitForSeconds(0.5f);
        Assert.IsTrue(GameObject.FindGameObjectsWithTag("Owner1").Length == 1);
        Debug.Log(timer.GetLocalTime());

        o = GameObject.FindGameObjectWithTag("Owner2").GetComponent<Owner>();

        ht["Team"] = 2;
        PhotonNetwork.LocalPlayer.SetCustomProperties(ht);

        o.GetComponent<NavMeshAgent>().ResetPath();
        o.GetComponent<NavMeshAgent>().enabled = false;
        obj.GetComponent<PlayerVoiceManager>().entered2 = true;
        obj.GetComponent<PlayerVoiceManager>().entered1 = false;

        o.transform.position = new Vector3(-6.363f, 0.2f, -7);
        o.GetComponent<NavMeshAgent>().enabled = true;
        yield return new WaitForSeconds(0.05f);
        o.shout = true;
        yield return new WaitForSeconds(0.05f);
        o.currentlyTalking = false;
        //    yield return new WaitForSeconds(2);

        // yield return new WaitForSeconds(0.5f);
        yield return null;
    }
    [UnityTest]
    public IEnumerator ownerReturned()
    {

        obj.transform.position = new Vector3(12.61f, 0.2f, -4.8f);
        yield return new WaitForSeconds(0.3f);
        o = GameObject.FindGameObjectWithTag("Owner1").GetComponent<Owner>();
        o.GetComponent<NavMeshAgent>().ResetPath();
        o.GetComponent<NavMeshAgent>().enabled = false;
        o.transform.position = new Vector3(12.61f, 0.2f, -4.8f);
        o.GetComponent<NavMeshAgent>().enabled = true;
        o.returnNormally();
        //     yield return new WaitForSeconds(3);

        yield return null;

    }

    [UnityTest]
    public IEnumerator ownerReturned2()
    {

        obj.transform.position = new Vector3(-6.363f, 0.2f, -7);
        yield return new WaitForSeconds(0.5f);
        o = GameObject.FindGameObjectWithTag("Owner2").GetComponent<Owner>();
        o.GetComponent<NavMeshAgent>().ResetPath();
        o.GetComponent<NavMeshAgent>().enabled = false;
        o.transform.position = new Vector3(-6.363f, 0.2f, -7);
        o.GetComponent<NavMeshAgent>().enabled = true;
        o.returnNormally();
        //      yield return new WaitForSeconds(2);

        yield return null;

    }
    [UnityTest]
    public IEnumerator collectFromOven()
    {

        obj.transform.position = new Vector3(-6.363f, 0.2f, -7);
        yield return new WaitForSeconds(1f);
        o = GameObject.FindGameObjectWithTag("Owner2").GetComponent<Owner>();
        playerHold.pickUpItem(cake);
        o.oven.GetComponent<Appliance>().player = obj.transform;
        o.oven.GetComponent<Appliance>().Interact();
        o.oven.GetComponent<Appliance>().Interact();

        o.collecting = true;
        o.GetComponent<NavMeshAgent>().ResetPath();
        o.GetComponent<NavMeshAgent>().enabled = false;
        o.transform.position = o.oven.transform.position - new Vector3(1, 0, 1);
        o.GetComponent<NavMeshAgent>().enabled = true;

             yield return new WaitForSeconds(0.1f);

        yield return null;

    }


}