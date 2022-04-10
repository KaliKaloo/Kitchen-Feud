using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Photon.Pun;
using System.IO;

public class IngredientLocation : PhotonTestSetup
{
    [Test]
    public void mushroomLocation()
    {
        GameObject mushroom = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "mushroom"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO mushroomSO = mushroom.GetComponent<IngredientItem>().item;
        Assert.AreEqual(Location.Pantry, mushroomSO.location);
    }

    [Test]
    public void potatoLocation()
    {
        GameObject potato = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "potato"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO potatoSO = potato.GetComponent<IngredientItem>().item;
        Assert.AreEqual(Location.Pantry, potatoSO.location);
    }

    [Test]
    public void breadLocation()
    {
        GameObject bread = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "bread"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO breadSO = bread.GetComponent<IngredientItem>().item;
        Assert.AreEqual(Location.Pantry, breadSO.location);
    }

    [Test]
    public void bunsLocation()
    {
        GameObject buns = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "buns"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO bunsSO = buns.GetComponent<IngredientItem>().item;
        Assert.AreEqual(Location.Pantry, bunsSO.location);
    }


    [Test]
    public void pattyLocation()
    {
        GameObject patty = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "patty"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO pattySO = patty.GetComponent<IngredientItem>().item;
        Assert.AreEqual(Location.Freezer, pattySO.location);
    }


    [Test]
    public void eggLocation()
    {
        GameObject egg = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "egg"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO eggSO = egg.GetComponent<IngredientItem>().item;
        Assert.AreEqual(Location.Fridge, eggSO.location);
    }


     [Test]
    public void butterLocation()
    {
        GameObject butter = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "butter"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO butterSO = butter.GetComponent<IngredientItem>().item;
        Assert.AreEqual(Location.Fridge, butterSO.location);
    }


    [Test]
    public void tomatoLocation()
    {
        GameObject tomato = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "tomato"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO tomatoSO = tomato.GetComponent<IngredientItem>().item;
        Assert.AreEqual(Location.Fridge, tomatoSO.location);
    }


    [Test]
    public void lettuceLocation()
    {
        GameObject lettuce = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "lettuce"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO lettuceSO = lettuce.GetComponent<IngredientItem>().item;
        Assert.AreEqual(Location.Fridge, lettuceSO.location);
    }


     [Test]
    public void onionLocation()
    {
        GameObject onion = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "onion"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO onionSO = onion.GetComponent<IngredientItem>().item;
        Assert.AreEqual(Location.Fridge, onionSO.location);
    }


    [Test]
    public void riceLocation()
    {
        GameObject rice = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "rice"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO riceSO = rice.GetComponent<IngredientItem>().item;
        Assert.AreEqual(Location.Pantry, riceSO.location);
    }


    [Test]
    public void appleLocation()
    {
        GameObject apple = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "apple"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO appleSO = apple.GetComponent<IngredientItem>().item;
        Assert.AreEqual(Location.Fridge, appleSO.location);
    }


    [Test]
    public void orangeLocation()
    {
        GameObject orange = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "orange"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO orangeSO = orange.GetComponent<IngredientItem>().item;
        Assert.AreEqual(Location.Fridge, orangeSO.location);
    }

    [Test]
    public void cucumberLocation()
    {
        GameObject cucumber = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "cucumber 1"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO cucumberSO = cucumber.GetComponent<IngredientItem>().item;
        Assert.AreEqual(Location.Fridge, cucumberSO.location);
    }

    
    public void kiwiLocation()
    {
        GameObject kiwi = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "kiwi"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO kiwiSO = kiwi.GetComponent<IngredientItem>().item;
        Assert.AreEqual(Location.Fridge, kiwiSO.location);
    }


    
    public void garlicLocation()
    {
        GameObject garlic = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "garlic"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO garlicSO = garlic.GetComponent<IngredientItem>().item;
        Assert.AreEqual(Location.Fridge, garlicSO.location);
    }


    
    // public void cucumberLocation()
    // {
    //     GameObject cucumber = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "cucumber 1"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
    //     IngredientSO cucumberSO = cucumber.GetComponent<IngredientItem>().item;
    //     Assert.AreEqual(Location.Fridge, cucumberSO.location);
    // }


   
}
