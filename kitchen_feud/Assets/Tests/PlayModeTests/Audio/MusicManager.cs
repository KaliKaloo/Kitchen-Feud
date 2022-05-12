using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.TestTools;
using System.IO;
using TMPro;
using UnityEngine.UI;


public class MusicManagerTests : PhotonTestSetup
{

   GameObject obj;


   [UnitySetUp]
   public IEnumerator Setup()
   {
      
      obj = PhotonNetwork.Instantiate(Path.Combine("PhotonPlayers",
         "Player_cat_Model"),
         new Vector3(-1.98f, 0.006363153f, -8.37f),
         Quaternion.identity,
         0
      );
      PhotonNetwork.LocalPlayer.CustomProperties["Team"] = 0;
      obj.AddComponent<PhotonPlayer>();
 
      yield return null;
   }

   [UnityTearDown]
   public IEnumerator TearDown()
   {
      if (obj != null)
         PhotonNetwork.Destroy(obj);
      yield return null;
   }


   [Test]
   public void switchLocationk1()
   {
      MusicManager.instance.switchLocation(1);
      Assert.AreEqual(1, MusicManager.instance.location);
      Assert.AreEqual(MusicManager.instance.k1_1, MusicManager.instance.musicClips);

   }

   [Test]
   public void switchLocationk2()
   {
      MusicManager.instance.switchLocation(2);
      Assert.AreEqual(2, MusicManager.instance.location);
      Assert.AreEqual(MusicManager.instance.k2_1, MusicManager.instance.musicClips);

   }


   [Test]
   public void switchLocationHallway()
   {
      MusicManager.instance.switchLocation(3);
      Assert.AreEqual(3, MusicManager.instance.location);
      Assert.AreEqual(MusicManager.instance.hallway, MusicManager.instance.musicClips);

   }


   [Test]
   public void musicReact()
   {
      MusicManager.instance.musicReact();
      Assert.AreEqual(1.3f, MusicManager.instance.pitch);
   }

   [Test]
   public void musicReactParam()
   {
      MusicManager.instance.musicReact(0.5f);
      Assert.AreEqual(0.5f, MusicManager.instance.pitch);
   }


   [UnityTest]
   public IEnumerator endReaction()
   {
      
      MusicManager.instance.endReaction();
      Assert.AreEqual(1, MusicManager.instance.pitch);
        yield return null;
   }


   [UnityTest]
   public IEnumerator settings()
   {
      GameObject.Find("Settings Button").GetComponent<Button>().onClick.Invoke();
      Assert.IsTrue(GameObject.Find("Music Volume"));
      yield return null;
   }


   [UnityTest]
   public IEnumerator playRandom()
   {
      MusicManager.instance.switchLocation(1);

      AudioClip oldClip = MusicManager.instance.track.clip;
      MusicManager.instance.playRandom();
      yield return new WaitForSeconds(0.2f);
      Assert.AreNotEqual(oldClip, MusicManager.instance.track.clip);

   }

    
    
}
