//using System.Collections;
//using System.Collections.Generic;
//using NUnit.Framework;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.TestTools;
//using Photon.Pun;
//using UnityEngine.UI;


//public class mainMenu
//{
//    [OneTimeSetUp]
//    public void SetUp()
//    {
//        SceneManager.LoadScene("mainMenu");
//    }

//    [UnityTest]
//    public IEnumerator mainMenuLoad()
//    {
//        Assert.IsFalse(GameObject.Find("LoadingScreen") != null && GameObject.Find("LoadingScreen").activeSelf);
//        Assert.IsTrue(GameObject.Find("usernameCanvas").activeSelf);
//        Assert.IsFalse(GameObject.Find("startGame") != null && GameObject.Find("startGame").activeSelf);
//        yield return null;
//    }
    
//    [UnityTest]
//    public IEnumerator UsernameUI()
//    {
//        GameObject usrCanvas = GameObject.Find("usernameCanvas");
//        GameObject userInput = GameObject.Find("usernameInput");
//        Assert.IsNotNull(userInput, "Missing field");
//        userInput.GetComponent<InputField>().text = "user";
//        GameObject playButton = GameObject.Find("startGame");
//        Assert.IsNotNull(playButton, "Missing play Button");
//        playButton.GetComponent<Button>().onClick.Invoke();
//        Assert.IsTrue(GameObject.Find("mainMenuCanvas") != null && GameObject.Find("mainMenuCanvas").activeSelf);
//        Assert.IsNull(GameObject.Find("usernameCanvas"));
//        Assert.AreEqual("user", PhotonNetwork.NickName);
//        string greeting = GameObject.Find("greetingMessage").GetComponent<Text>().text;
//        Assert.AreEqual("Welcome user!", greeting);

//        yield return null;
//    }
//}
