using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Photon.Pun;
using UnityEngine.UI;


public class UserInput
{
    [OneTimeSetUp]
    public void SetUp()
    {
        SceneManager.LoadScene("mainMenu");
    }
   

    [UnityTest]
    public IEnumerator enterUserName()
    {
        GameObject usrCanvas = GameObject.Find("usernameCanvas");
        GameObject userInput = GameObject.Find("usernameInput");
        Assert.IsNotNull(userInput, "Missing field");
        userInput.GetComponent<InputField>().text = "user";

        GameObject playButton = GameObject.Find("startGame");
        Assert.IsNotNull(playButton, "Missing play Button");

        playButton.GetComponent<Button>().onClick.Invoke();
        Assert.IsTrue(GameObject.Find("mainMenuCanvas") != null && GameObject.Find("mainMenuCanvas").activeSelf);

        yield return null;
    }
}
