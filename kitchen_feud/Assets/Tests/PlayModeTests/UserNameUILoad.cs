using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Photon.Pun;

public class UserNameUILoad
{

    [OneTimeSetUp]
    public void SetUp()
    {
        SceneManager.LoadScene("mainMenu");
    }
   
    [UnityTest]
    public IEnumerator userUILoad()
    {
        Assert.IsFalse(GameObject.Find("LoadingScreen") != null && GameObject.Find("LoadingScreen").activeSelf);
        Assert.IsTrue(GameObject.Find("usernameCanvas").activeSelf);
        Assert.IsFalse(GameObject.Find("startGame") != null && GameObject.Find("startGame").activeSelf);
        yield return null;
    }


}
