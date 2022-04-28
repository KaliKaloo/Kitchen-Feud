using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Photon.Pun;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Debug = UnityEngine.Debug;


public class cookingMethod : PhotonTestSetup
{
    [Test]
    public void cookingMethodMushroomSoup()
    {
        
        GameObject mushroomSoup = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Mushroom Soup"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO mushroomSoupSO = (DishSO) mushroomSoup.GetComponent<pickableItem>().item;
        Assert.AreEqual("Stove", mushroomSoupSO.toCook);
    }


    [Test]
    public void cookingMethodBurger()
    {
   
        GameObject burger = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Burger"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO burgerSO = (DishSO) burger.GetComponent<pickableItem>().item;
        Assert.AreEqual("Stove", burgerSO.toCook);
    }

    [Test]
    public void cookingMethodChips()
    {
        GameObject chips = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Chips"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO chipsSO = (DishSO) chips.GetComponent<pickableItem>().item;
        Assert.AreEqual("Stove", chipsSO.toCook);

    }


    [Test]
    public void cookingMethodEggRice()
    {
        GameObject EggRice = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Egg-fried rice"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO EggRiceSO = (DishSO) EggRice.GetComponent<pickableItem>().item;
        Assert.AreEqual("Stove", EggRiceSO.toCook);
    }


    [Test]
    public void cookingMethodEggyBread()
    {
        GameObject eggyBread = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Eggy Bread"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO eggyBreadSO = (DishSO) eggyBread.GetComponent<pickableItem>().item;
        Assert.AreEqual("Stove", eggyBreadSO.toCook);

    }


    [Test]
    public void cookingMethodOmelette()
    {
        GameObject omelette = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Omelette"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO omeletteSO = (DishSO) omelette.GetComponent<pickableItem>().item;
        Assert.AreEqual("Stove", omeletteSO.toCook);
    }

    [Test]
    public void cookingMethodSalad()
    {
        GameObject salad = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Salad"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO saladSO = (DishSO) salad.GetComponent<pickableItem>().item;
        Assert.AreEqual("Cutting Board", saladSO.toCook);

    }
    
    [Test]
    public void cookingMethodSandwich()
    {
        GameObject sandwich = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Sandwich"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO sandwichSO = (DishSO) sandwich.GetComponent<pickableItem>().item;
        Assert.AreEqual("Sandwich Station", sandwichSO.toCook);
    }


    [Test]
    public void cookingMethodTomatoSoup()
    {
        GameObject tomatoSoup = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Tomato Soup"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO tomatoSoupSO = (DishSO) tomatoSoup.GetComponent<pickableItem>().item;
        Assert.AreEqual("Stove", tomatoSoupSO.toCook);
    }

    [Test]
    public void cookingMethodMushroomRisotto()
    {
        GameObject mushroomRisotto = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Mushroom rissotto"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO mushroomRisottoSO = (DishSO) mushroomRisotto.GetComponent<pickableItem>().item;
        Assert.AreEqual("Stove", mushroomRisottoSO.toCook);
    }


    [Test]
    public void cookingMethodPasta()
    {
        GameObject pasta = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Pasta"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO pastaSO = (DishSO) pasta.GetComponent<pickableItem>().item;
        Assert.AreEqual("Stove", pastaSO.toCook);
    }
    
    
    [Test]
    public void cookingMethodFruitSalad()
    {
        GameObject fruitSalad = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "fruitSalad"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO fruitSaladSO = (DishSO) fruitSalad.GetComponent<pickableItem>().item;
        Assert.AreEqual("Cutting Board", fruitSaladSO.toCook);
    }
    
    
    [Test]
    public void cookingMethodCake()
    {
        GameObject cake = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Cake"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO cakeSO = (DishSO) cake.GetComponent<pickableItem>().item;
        Assert.AreEqual("Oven", cakeSO.toCook);
    }
    
    [Test]
    public void cookingMethodPancakes()
    {
        GameObject pancakes = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Pancakes"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO pancakesSO = (DishSO) pancakes.GetComponent<pickableItem>().item;
        Assert.AreEqual("Stove", pancakesSO.toCook);
    }
    
    
    [Test]
    public void cookingMethodEggMayo()
    {
        GameObject eggMayo = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "EggMayo sandwich"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO eggMayoSO = (DishSO) eggMayo.GetComponent<pickableItem>().item;
        Assert.AreEqual("Sandwich Station", eggMayoSO.toCook);
    }

    [Test]
    public void cookingMethodCheeseOnion()
    {
        GameObject cheeseOnion = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "CheeseOnion sandwich"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO cheeseOnionSO = (DishSO) cheeseOnion.GetComponent<pickableItem>().item;
        Assert.AreEqual("Sandwich Station", cheeseOnionSO.toCook);
    }

    [Test]
    public void cookingMethodPBJ()
    {
        GameObject PBJ = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "PBJ_sandwich"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO PBJSO = (DishSO) PBJ.GetComponent<pickableItem>().item;
        Assert.AreEqual("Sandwich Station", PBJSO.toCook);
    }

    [Test]
    public void cookingMethodPizza()
    {
        GameObject pizza = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "pizza"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO pizzaSO = (DishSO) pizza.GetComponent<pickableItem>().item;
        Assert.AreEqual("Oven", pizzaSO.toCook);
    }
}
