using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Photon.Pun;
using System.IO;


    public class Owner:PhotonTestSetup
    {
    GameObject obj;
    GlobalTimer timer;


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
        sandwichStation = GameObject.Find("sandwhich_station1").GetComponent<Appliance>();
        PhotonNetwork.LocalPlayer.CustomProperties["Team"] = 1;
        PhotonNetwork.LocalPlayer.CustomProperties["ViewID"] = obj.GetPhotonView().ViewID;
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
       

        //timer.changeTimerOnly(30);
        Debug.Log(timer.GetLocalTime());
        yield return new WaitForSeconds(6);
        Assert.IsTrue(GameObject.FindGameObjectsWithTag("Owner1").Length == 1); 
        Debug.Log(timer.GetLocalTime());

        // yield return new WaitForSeconds(0.5f);
        yield return null;
    }


    
}
