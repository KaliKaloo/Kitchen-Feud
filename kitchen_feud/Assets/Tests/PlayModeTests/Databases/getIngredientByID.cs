using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Photon.Pun;
using UnityEngine.TestTools;
using System.IO;


public class getIngredientByID : PhotonTestSetup
{
   
    [Test]
    public void getMushroomByID()
    {
        GameObject mushroom = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "mushroom"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO mushroomSO = mushroom.GetComponent<IngredientItem>().item;
        IngredientSO foundIngredient =  Database.GetIngredientByID("11");
        Assert.AreEqual(mushroomSO, foundIngredient);
    }

    [Test]
    public void getPotatoByID()
    {

        GameObject potato = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "potato"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO potatoSO = potato.GetComponent<IngredientItem>().item;
        IngredientSO foundIngredient =  Database.GetIngredientByID("12");
        Assert.AreEqual(potatoSO, foundIngredient);
    }


    [Test]
    public void getBreadByID()
    {
        GameObject bread = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "bread"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO breadSO = bread.GetComponent<IngredientItem>().item;
        IngredientSO foundIngredient =  Database.GetIngredientByID("13");
        Assert.AreEqual(breadSO, foundIngredient);
    }

    [Test]
    public void getBunsByID()
    {
        GameObject buns = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "buns"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO bunsSO = buns.GetComponent<IngredientItem>().item;
        IngredientSO foundIngredient =  Database.GetIngredientByID("14");
        Assert.AreEqual(bunsSO, foundIngredient);
    }


    [Test]
    public void getPattyByID()
    {
        GameObject patty = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "patty"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO pattySO = patty.GetComponent<IngredientItem>().item;
        IngredientSO foundIngredient =  Database.GetIngredientByID("15");
        Assert.AreEqual(pattySO, foundIngredient);
    }


    [Test]
    public void getEggByID()
    {
        GameObject egg = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "egg"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO eggSO = egg.GetComponent<IngredientItem>().item;
        IngredientSO foundIngredient =  Database.GetIngredientByID("16");
        Assert.AreEqual(eggSO, foundIngredient);
    }


     [Test]
    public void getButterByID()
    {
        GameObject butter = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "butter"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO butterSO = butter.GetComponent<IngredientItem>().item;
        IngredientSO foundIngredient =  Database.GetIngredientByID("17");
        Assert.AreEqual(butterSO, foundIngredient);
    }


    [Test]
    public void getTomatoByID()
    {
        GameObject tomato = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "tomato"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO tomatoSO = tomato.GetComponent<IngredientItem>().item;
        IngredientSO foundIngredient =  Database.GetIngredientByID("18");
        Assert.AreEqual(tomatoSO, foundIngredient);
    }


    [Test]
    public void getLettuceByID()
    {
        GameObject lettuce = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "lettuce"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO lettuceSO = lettuce.GetComponent<IngredientItem>().item;
        IngredientSO foundIngredient =  Database.GetIngredientByID("19");
        Assert.AreEqual(lettuceSO, foundIngredient);
    }


    [Test]
    public void getOnionByID()
    {
        GameObject onion = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "onion"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO onionSO = onion.GetComponent<IngredientItem>().item;
        IngredientSO foundIngredient =  Database.GetIngredientByID("20");
        Assert.AreEqual(onionSO, foundIngredient);
    }


    [Test]
    public void getRiceByID()
    {
        GameObject rice = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "rice"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO riceSO = rice.GetComponent<IngredientItem>().item;
        IngredientSO foundIngredient =  Database.GetIngredientByID("21");
        Assert.AreEqual(riceSO, foundIngredient);
    }

    [Test]
    public void getAppleByID()
    {
        GameObject apple = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "apple"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO appleSO = apple.GetComponent<IngredientItem>().item;
        IngredientSO foundIngredient =  Database.GetIngredientByID("22");
        Assert.AreEqual(appleSO, foundIngredient);
    }


    [Test]
    public void getOrangeByID()
    {
        GameObject orange = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "orange"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO orangeSO = orange.GetComponent<IngredientItem>().item;
        IngredientSO foundIngredient =  Database.GetIngredientByID("23");
        Assert.AreEqual(orangeSO, foundIngredient);
    }

    [Test]
    public void getCucumberByID()
    {
        GameObject cucumber = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "cucumber 1"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO cucumberSO = cucumber.GetComponent<IngredientItem>().item;
        IngredientSO foundIngredient =  Database.GetIngredientByID("24");
        Assert.AreEqual(cucumberSO, foundIngredient);
    }

    
    [Test]
    public void getKiwiByID()
    {
        GameObject kiwi = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "kiwi"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO kiwiSO = kiwi.GetComponent<IngredientItem>().item;
        IngredientSO foundIngredient =  Database.GetIngredientByID("25");
        Assert.AreEqual(kiwiSO, foundIngredient);
    }


    
    [Test]
    public void getGarlicByID()
    {
        GameObject garlic = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "garlic"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO garlicSO = garlic.GetComponent<IngredientItem>().item;
        IngredientSO foundIngredient =  Database.GetIngredientByID("26");
        Assert.AreEqual(garlicSO, foundIngredient);
    }


    
    [Test]
    public void getMarinaraByID()
    {
        GameObject marinara = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "sauce"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO marinaraSO = marinara.GetComponent<IngredientItem>().item;
        IngredientSO foundIngredient =  Database.GetIngredientByID("27");
        Assert.AreEqual(marinaraSO, foundIngredient);
    }


    [Test]
    public void getPeanutButterByID()
    {
        GameObject PeanutButter = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "peanutButter"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO PeanutButterSO = PeanutButter.GetComponent<IngredientItem>().item;
        IngredientSO foundIngredient =  Database.GetIngredientByID("28");
        Assert.AreEqual(PeanutButterSO, foundIngredient);
    }



    [Test]
    public void getJamByID()
    {
        GameObject jam = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "jam"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO jamSO = jam.GetComponent<IngredientItem>().item;
        IngredientSO foundIngredient =  Database.GetIngredientByID("29");
        Assert.AreEqual(jamSO, foundIngredient);
    }

    [Test]
    public void getPastaByID()
    {
        GameObject pasta = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "rawPasta"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO pastaSO = pasta.GetComponent<IngredientItem>().item;
        IngredientSO foundIngredient =  Database.GetIngredientByID("30");
        Assert.AreEqual(pastaSO, foundIngredient);
    }
    [Test]
    public void getFlourByID()
    {
        GameObject flour = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "flour"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO flourSO = flour.GetComponent<IngredientItem>().item;
        IngredientSO foundIngredient =  Database.GetIngredientByID("31");
        Assert.AreEqual(flourSO, foundIngredient);
    }



    [Test]
    public void getMayoByID()
    {
        GameObject mayo = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "mayo"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO mayoSO = mayo.GetComponent<IngredientItem>().item;
        IngredientSO foundIngredient =  Database.GetIngredientByID("32");
        Assert.AreEqual(mayoSO, foundIngredient);
    }

    [Test]
    public void getCheeseByID()
    {
        GameObject cheese = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "cheese"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO cheeseSO = cheese.GetComponent<IngredientItem>().item;
        IngredientSO foundIngredient =  Database.GetIngredientByID("33");
        Assert.AreEqual(cheeseSO, foundIngredient);
    }


    [Test]
    public void getMilkByID()
    {
        GameObject milk = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "milk"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO milkSO = milk.GetComponent<IngredientItem>().item;
        IngredientSO foundIngredient =  Database.GetIngredientByID("34");
        Assert.AreEqual(milkSO, foundIngredient);
    }

    [Test]
    public void getDoughByID()
    {
        GameObject dough = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "dough"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO doughSO = dough.GetComponent<IngredientItem>().item;
        IngredientSO foundIngredient =  Database.GetIngredientByID("35");
        Assert.AreEqual(doughSO, foundIngredient);
    }


    [Test]
    public void getCakeByID()
    {
        GameObject cake = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "unCookedCake"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO cakeSO = cake.GetComponent<IngredientItem>().item;
        IngredientSO foundIngredient =  Database.GetIngredientByID("36");
        Assert.AreEqual(cakeSO, foundIngredient);
    }

}
