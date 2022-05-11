// using System.Collections;
// using UnityEditor;
// using System.Collections.Generic;
// using NUnit.Framework;
// using UnityEngine;
// using Photon.Pun;
// using Photon.Realtime;
// using UnityEngine.TestTools;
// using System.IO;
// using TMPro;

// public class CanvasControllerTest : PhotonTestSetup
// {

//     GameObject obj;

//     CanvasController contr1, contr2;


//     [UnitySetUp]
//     public IEnumerator Setup()
//     {
        
//         obj = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers",
//             "Player_cat_Model"),
//             new Vector3(-1.98f, 0.006363153f, -8.37f),
//             Quaternion.identity,
//             0
//         );
//         PhotonNetwork.LocalPlayer.CustomProperties["Team"] = 0;
//         obj.AddComponent<PhotonPlayer>();
//         GameObject team1 = GameObject.Find("Team1OrderController");
//         contr1 = team1.GetComponent<CanvasController>();
//         GameObject team2 = GameObject.Find("Team2OrderController");
//         contr2 = team2.GetComponent<CanvasController>();
//         yield return null;
//     }

//     [UnityTearDown]
//     public IEnumerator TearDown()
//     {
//         if (obj != null)
//             PhotonNetwork.Destroy(obj);
//         yield return null;
//     }

  

//     [Test]
//     public void k1t1TrayOrderOptions()
//     {
//         GameObject.Find("Team1").transform.Find("ticket1-1").gameObject.SetActive(true);
//         contr1.TrayOrderOptions("Tray1-1");
//         Assert.IsTrue(contr1.orderMenu.activeSelf);
//         Assert.AreEqual(contr1.ticket1, contr1.justClicked);
//     }

//     [Test]
//     public void k1t2TrayOrderOptions()
//     {
//         GameObject.Find("Team1").transform.Find("ticket2-1").gameObject.SetActive(true);
//         contr1.TrayOrderOptions("Tray2-1");
//         Assert.IsTrue(contr1.orderMenu.activeSelf);
//         Assert.AreEqual(contr1.ticket2, contr1.justClicked);
//     }

//     [Test]
//     public void k1t3TrayOrderOptions()
//     {
//        GameObject.Find("Team1").transform.Find("ticket3-1").gameObject.SetActive(true);
//         contr1.TrayOrderOptions("Tray3-1");
//         Assert.IsTrue(contr1.orderMenu.activeSelf);
//         Assert.AreEqual(contr1.ticket3, contr1.justClicked);
//     }


//     [Test]
//     public void k2t1TrayOrderOptions()
//     {
//         GameObject.Find("Team2").transform.Find("ticket1-2").gameObject.SetActive(true);
//         contr2.TrayOrderOptions("Tray1-2");
//         Assert.IsTrue(contr2.orderMenu.activeSelf);
//         Assert.AreEqual(contr2.ticket1, contr2.justClicked);
//     }

//     [Test]
//     public void k2t2TrayOrderOptions()
//     {
//         GameObject.Find("Team2").transform.Find("ticket2-2").gameObject.SetActive(true);
//         contr2.TrayOrderOptions("Tray2-2");
//         Assert.IsTrue(contr2.orderMenu.activeSelf);
//         Assert.AreEqual(contr2.ticket2, contr2.justClicked);
//     }

//     [Test]
//     public void k2t3TrayOrderOptions()
//     {
//         GameObject.Find("Team2").transform.Find("ticket3-2").gameObject.SetActive(true);
//         contr2.TrayOrderOptions("Tray3-2");
//         Assert.IsTrue(contr2.orderMenu.activeSelf);
//         Assert.AreEqual(contr2.ticket3, contr2.justClicked);
//     }

//     [UnityTest]
//     public IEnumerator TaskOnClickt1k1(){
//         contr1.ticket1.GetComponent<DisplayTicket>().orderMainText.text = "main dish";
//         contr1.ticket1.GetComponent<DisplayTicket>().orderSideText.text = "side dish";
//         contr1.TrayOrderOptions("Tray1-1");
//         contr1.ticket1.SetActive(true);
//         contr1.TaskOnClick();
//         yield return null;
//         Assert.IsFalse(contr1.orderMenu.activeSelf);
//         Assert.IsFalse(contr1.ticket1.activeSelf);

//         Assert.AreEqual("", contr1.ticket1.GetComponent<DisplayTicket>().orderMainText.text);
//         Assert.AreEqual("", contr1.ticket1.GetComponent<DisplayTicket>().orderSideText.text);

//     }
    

//     [UnityTest]
//     public IEnumerator TaskOnClickt2k2(){
//         contr2.ticket2.GetComponent<DisplayTicket>().orderMainText.text = "main dish";
//         contr2.ticket2.GetComponent<DisplayTicket>().orderSideText.text = "side dish";
//         contr2.TrayOrderOptions("Tray2-2");
//         contr2.ticket2.SetActive(true);
//         contr2.TaskOnClick();
//         yield return null;
//         Assert.IsFalse(contr2.orderMenu.activeSelf);
//         Assert.IsFalse(contr2.ticket2.activeSelf);

//         Assert.AreEqual("", contr2.ticket2.GetComponent<DisplayTicket>().orderMainText.text);
//         Assert.AreEqual("", contr2.ticket2.GetComponent<DisplayTicket>().orderSideText.text);

//     }


    
//     [Test]
//     public void t1NotActive()
//     {
//         contr1.ticket1.SetActive(false);
//         contr1.ticket2.SetActive(true);
//         contr1.ticket3.SetActive(true);
//         contr1.ShowNewTicketWithID("OR01");
//         Assert.IsTrue(contr1.ticket1.activeSelf);
//     }

//     [Test]
//     public void t2NotActive()
//     {
//         contr1.ticket1.SetActive(true);
//         contr1.ticket2.SetActive(false);
//         contr1.ticket3.SetActive(true);
//         contr1.ShowNewTicketWithID("OR01");
//         Assert.IsTrue(contr1.ticket2.activeSelf);
//     }


//     [Test]
//     public void t3NotActive()
//     {
//         contr1.ticket1.SetActive(true);
//         contr1.ticket2.SetActive(true);
//         contr1.ticket3.SetActive(false);
//         contr1.ShowNewTicketWithID("OR01");
//         Assert.IsTrue(contr1.ticket3.activeSelf);
//     }

//     [Test]
//     public void DisplayOrderFromID(){
//         int orderNum = contr1.DisplayOrderFromID(contr1.ticket1.GetComponent<DisplayTicket>(), "OR01");
//         int orderNum2 = contr1.DisplayOrderFromID(contr1.ticket1.GetComponent<DisplayTicket>(), "OR02");
//         Assert.AreEqual(orderNum+1, orderNum2);

//     }

    
// }
