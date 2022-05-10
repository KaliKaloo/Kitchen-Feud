using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Photon.Pun;
using System.IO;
using Hashtable = ExitGames.Client.Photon.Hashtable;


    public class FryingMiniGameTests:PhotonTestSetup
    {
    GameObject obj,obj1;
    GameObject buns, patty;

    PlayerHolding playerHold;
    Appliance stove;
    Hashtable ht = new Hashtable();
    Hashtable ht1 = new Hashtable();

    [UnitySetUp]
    public IEnumerator Setup()
    {

        buns = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "buns"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        patty = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "patty"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        //lettuce = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "lettuce"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);

        obj = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers",
            "Player_cat_Model"),
            new Vector3(-1.98f, 0.006363153f, -8.37f),
            Quaternion.identity,
            0
        );
        obj1 = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers",
       "Player_cat_Model"),
       new Vector3(-1.98f, 0.006363153f, -8.37f),
       Quaternion.identity,
       0
   );
        obj1.name = "Local";

        stove = GameObject.Find("stove1").GetComponent<Appliance>();
        ht["Team"] = 1;
        ht["ViewID"] = obj1.GetPhotonView().ViewID;
        ht1["Time"] = 10;
        PhotonNetwork.LocalPlayer.SetCustomProperties(ht);
        PhotonNetwork.CurrentRoom.SetCustomProperties(ht1);
        //PhotonNetwork.LocalPlayer.CustomProperties["Team"] = 0;
        playerHold = obj.GetComponent<PlayerHolding>();
        yield return null;
    }


    [UnityTearDown]
    public IEnumerator TearDown()
    {
        if (obj != null)
            PhotonNetwork.Destroy(obj);
        yield return null;
    }



    [UnityTest]
    public IEnumerator correctIngredients()
    {
        obj1.name = "Local";
        obj.GetComponent<PlayerController>().myTeam = 1;
        obj1.GetComponent<PlayerController>().myTeam = 2;
        stove.player = obj.transform;
        playerHold.pickUpItem(buns);
        stove.Interact();
        playerHold.pickUpItem(patty);
        stove.Interact();
       
        stove.Interact();
        stove.player = obj.transform;
        stove.Interact();
 
        Assert.IsTrue(stove.minigameCanvas2.activeSelf);
    
       
        yield return new WaitForSeconds(2);
        stove.minigameCanvas2.transform.Find("BackButton").gameObject.SetActive(true);
        // if (obj.GetComponent<PhotonView>().IsMine)
        if (PhotonNetwork.IsMasterClient)
        {
          //  PhotonNetwork.Destroy(stove.minigameCanvas2.transform.Find("PanGameObject").gameObject);

            {
                stove.minigameCanvas2.transform.Find("BackButton").GetComponent<ExitFryingMinigame>().appliance = stove;
                stove.minigameCanvas2.transform.Find("BackButton").gameObject.AddComponent<PhotonView>();
               yield return new WaitForSeconds(1);
                stove.minigameCanvas2.transform.Find("BackButton").GetComponent<ExitFryingMinigame>().TaskOnClick();
            }
        }

      // PhotonNetwork.Destroy(stove.minigameCanvas2);


        yield return null;
    }



}
