using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
public class MenuTest
{

    [OneTimeSetUp]
    public void SetUp()
    {
        PlayerPrefs.DeleteAll();
        
        GameObject obj = new GameObject();
        SceneManager.LoadScene("mainMenu");
    }


    [UnityTearDown]
    public IEnumerator PhotonTearDown()
    {
        SceneManager.LoadScene("Test");
        yield return new WaitForSeconds(4);
        PhotonNetwork.Disconnect();
        yield return new WaitForSeconds(4);

    }
   
    [UnityTest]
    public IEnumerator createLobby()
    {
        yield return new WaitForSeconds(3f);
        menuController mC = GameObject.Find("MenuController").GetComponent<menuController>();
        Assert.IsTrue(mC);
        yield return new WaitForSeconds(1f);
        GameObject.Find("usernameInput").GetComponent<InputField>().text = "test";
        yield return new WaitForSeconds(0.2f);
        Assert.IsTrue(GameObject.Find("startGame"));

        yield return new WaitForSeconds(0.2f);
        mC.SetUsername();

        GameObject.Find("Settings Button").GetComponent<Button>().onClick.Invoke();
        Assert.IsTrue(GameObject.Find("UserSettingsCanvas"));
        GameObject.Find("BackButton").GetComponent<Button>().onClick.Invoke();
        Assert.IsFalse(GameObject.Find("UserSettingsCanvas"));

        GameObject.Find("Settings Button").GetComponent<Button>().onClick.Invoke();
        GameObject.Find("changeUsername").GetComponent<Button>().onClick.Invoke();
        GameObject.Find("usernameInput").GetComponent<InputField>().text = "test2";
        yield return new WaitForSeconds(0.2f);
        Assert.IsTrue(GameObject.Find("startGame"));

        yield return new WaitForSeconds(0.2f);
        mC.SetUsername();

        GameObject.Find("findLobby").GetComponent<Button>().onClick.Invoke();
        Assert.IsTrue(GameObject.Find("FindRoomCanvas"));
        GameObject.Find("backToMenu").GetComponent<Button>().onClick.Invoke();

        GameObject.Find("MenuController").GetComponent<menuController>().CreateGame();
        

        GameObject.Find("createInput").GetComponent<InputField>().text = "AS";
        yield return new WaitForSeconds(0.2f);

        GameObject.Find("MenuController").GetComponent<menuController>().CreateGame();
        yield return new WaitForSeconds(2f);

        GameObject.Find("Settings").GetComponent<Button>().onClick.Invoke();
        GameObject.Find("IncreaseTime").GetComponent<Button>().onClick.Invoke();
        yield return new WaitForSeconds(0.2f);

        GameObject.Find("DecreaseTime").GetComponent<Button>().onClick.Invoke();
        yield return new WaitForSeconds(0.2f);

        Assert.AreEqual("05:00",  GameObject.Find("SettingsCanvas").transform.Find("Timer").transform.Find("Timer").transform.Find("Timer").GetComponent<Text>().text);


        yield return null;
    }
}
