using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;


    public class MenuTest:MainMenuSetup
    {
    GlobalTimer timer;
    [UnitySetUp]
    public IEnumerator Setup()
    {


        timer = new GlobalTimer();
        PlayerPrefs.DeleteAll();


        yield return null;
    }

    [UnityTearDown]
    public IEnumerator MovementTearDown()
    {
        //if (obj != null)
        //    PhotonNetwork.Destroy(obj);
        yield return null;
    }

    [UnityTest]
    public IEnumerator menuLoaded()
    {
        yield return null;
        //yield return new WaitForSeconds(5);
    }
    [UnityTest]
    public IEnumerator createLobby()
    {
        yield return new WaitForSeconds(0.5f);
        menuController mC = GameObject.Find("MenuController").GetComponent<menuController>();
        Assert.IsTrue(mC);
        yield return new WaitForSeconds(1);
        GameObject.Find("usernameInput").GetComponent<InputField>().text = "test";
        yield return new WaitForSeconds(1f);
        Assert.IsTrue(GameObject.Find("startGame"));

        yield return new WaitForSeconds(1f);
        mC.SetUsername();
        //yield return new WaitForSeconds(5);
        GameObject.Find("createInput").GetComponent<InputField>().text = "AS";
        yield return new WaitForSeconds(1f);


        GameObject.Find("MenuController").GetComponent<menuController>().CreateGame();
       

        yield return null;
    }
}
