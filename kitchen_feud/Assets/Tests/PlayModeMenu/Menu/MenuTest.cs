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

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        PlayerPrefs.DeleteAll();

        SceneManager.LoadScene("mainMenu");
        yield return null;
    }


    [UnityTearDown]
    public IEnumerator PhotonTearDown()
    {
        GameObject.Find("MenuController").SetActive(false);
        yield return null;
        PhotonNetwork.Disconnect();
        yield return null;


    }
    

    [UnityTest]
    public IEnumerator createLobby()
    {
        yield return new WaitForSeconds(1);
        menuController mC = GameObject.Find("MenuController").GetComponent<menuController>();
        Assert.IsTrue(mC);
        yield return null;
        GameObject.Find("usernameInput").GetComponent<InputField>().text = "test";
        yield return null;
        Assert.IsTrue(GameObject.Find("startGame"));

        yield return null;
        mC.SetUsername();

        GameObject.Find("Settings Button").GetComponent<Button>().onClick.Invoke();
        Assert.IsTrue(GameObject.Find("UserSettingsCanvas"));
        GameObject.Find("BackButton").GetComponent<Button>().onClick.Invoke();
        Assert.IsFalse(GameObject.Find("UserSettingsCanvas"));

        GameObject.Find("Settings Button").GetComponent<Button>().onClick.Invoke();
        GameObject.Find("changeUsername").GetComponent<Button>().onClick.Invoke();
        GameObject.Find("usernameInput").GetComponent<InputField>().text = "test2";
        yield return null;
        Assert.IsTrue(GameObject.Find("startGame"));

        yield return null;
        mC.SetUsername();

        GameObject.Find("findLobby").GetComponent<Button>().onClick.Invoke();
        Assert.IsTrue(GameObject.Find("FindRoomCanvas"));
        GameObject.Find("backToMenu").GetComponent<Button>().onClick.Invoke();

        GameObject.Find("MenuController").GetComponent<menuController>().CreateGame();

        GameObject.Find("createInput").GetComponent<InputField>().text = "AS";
        yield return null;

        GameObject.Find("MenuController").GetComponent<menuController>().CreateGame();
        yield return null;
        yield return new WaitForSeconds(1);

        GameObject.Find("Settings").GetComponent<Button>().onClick.Invoke();
        GameObject.Find("IncreaseTime").GetComponent<Button>().onClick.Invoke();
        yield return null;

        GameObject.Find("DecreaseTime").GetComponent<Button>().onClick.Invoke();
        yield return null;

        Assert.AreEqual("05:00", GameObject.Find("SettingsCanvas").transform.Find("Timer").transform.Find("Timer").transform.Find("Timer").GetComponent<Text>().text);


        yield return null;
    }
}
