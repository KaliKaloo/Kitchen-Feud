using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Photon.Pun;
using System.IO;
public class FryDish : PhotonTestSetup
{
    [Test]
    public void cookingMethodMushroomSoup()
    {
        GameObject mushroomSoup = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Mushroom Soup"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO mushroomSoupSO = (DishSO) mushroomSoup.GetComponent<pickableItem>().item;
        Assert.IsFalse(mushroomSoupSO.stoveFry);
    }


    [Test]
    public void cookingMethodBurger()
    {
        GameObject burger = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Burger"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO burgerSO = (DishSO) burger.GetComponent<pickableItem>().item;
        Assert.IsTrue(burgerSO.stoveFry);
    }

// eventually change to frying
    // [Test]
    // public void cookingMethodChips()
    // {
    //     GameObject chips = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Chips"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
    //     DishSO chipsSO = (DishSO) chips.GetComponent<pickableItem>().item;
    //     Assert.IsTrue(chipsSO.stoveFry);
    // }


    [Test]
    public void cookingMethodEggRice()
    {
        GameObject EggRice = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Egg-fried rice"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO EggRiceSO = (DishSO) EggRice.GetComponent<pickableItem>().item;
        Assert.IsTrue(EggRiceSO.stoveFry);
    }


    [Test]
    public void cookingMethodEggyBread()
    {
        GameObject eggyBread = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Eggy Bread"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO eggyBreadSO = (DishSO) eggyBread.GetComponent<pickableItem>().item;
        Assert.IsTrue(eggyBreadSO.stoveFry);

    }

    [Test]
    public void cookingMethodSalad()
    {
        GameObject salad = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Salad"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO saladSO = (DishSO) salad.GetComponent<pickableItem>().item;
        Assert.IsFalse(saladSO.stoveFry);
    }
    
    [Test]
    public void cookingMethodSandwich()
    {
        GameObject sandwich = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Sandwich"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO sandwichSO = (DishSO) sandwich.GetComponent<pickableItem>().item;
        Assert.IsFalse(sandwichSO.stoveFry);
    }


    [Test]
    public void cookingMethodTomatoSoup()
    {
        GameObject tomatoSoup = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Tomato Soup"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO tomatoSoupSO = (DishSO) tomatoSoup.GetComponent<pickableItem>().item;
        Assert.IsFalse(tomatoSoupSO.stoveFry);
    }

    [Test]
    public void cookingMethodMushroomRisotto()
    {
        GameObject mushroomRisotto = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Mushroom rissotto"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO mushroomRisottoSO = (DishSO) mushroomRisotto.GetComponent<pickableItem>().item;
        Assert.IsFalse(mushroomRisottoSO.stoveFry);
    }


    [Test]
    public void cookingMethodPasta()
    {
        GameObject pasta = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Pasta"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO pastaSO = (DishSO) pasta.GetComponent<pickableItem>().item;
        Assert.IsFalse(pastaSO.stoveFry);
    }
    
    
    [Test]
    public void cookingMethodFruitSalad()
    {
        GameObject fruitSalad = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "fruitSalad"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO fruitSaladSO = (DishSO) fruitSalad.GetComponent<pickableItem>().item;
        Assert.IsFalse(fruitSaladSO.stoveFry);
    }
    
    
    [Test]
    public void cookingMethodCake()
    {
        GameObject cake = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Cake"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO cakeSO = (DishSO) cake.GetComponent<pickableItem>().item;
        Assert.IsFalse(cakeSO.stoveFry);
    }
    
    [Test]
    public void cookingMethodPancakes()
    {
        GameObject pancakes = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Pancakes"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO pancakesSO = (DishSO) pancakes.GetComponent<pickableItem>().item;
        Assert.IsTrue(pancakesSO.stoveFry);
    }
    
    
    [Test]
    public void cookingMethodEggMayo()
    {
        GameObject eggMayo = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "EggMayo sandwich"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO eggMayoSO = (DishSO) eggMayo.GetComponent<pickableItem>().item;
        Assert.IsFalse(eggMayoSO.stoveFry);
    }

    [Test]
    public void cookingMethodCheeseOnion()
    {
        GameObject cheeseOnion = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "CheeseOnion sandwich"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO cheeseOnionSO = (DishSO) cheeseOnion.GetComponent<pickableItem>().item;
        Assert.IsFalse(cheeseOnionSO.stoveFry);
    }

    [Test]
    public void cookingMethodPBJ()
    {
        GameObject PBJ = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "PBJ_sandwich"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO PBJSO = (DishSO) PBJ.GetComponent<pickableItem>().item;
        Assert.IsFalse(PBJSO.stoveFry);
    }

    [Test]
    public void cookingMethodPizza()
    {
        GameObject pizza = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "pizza"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO pizzaSO = (DishSO) pizza.GetComponent<pickableItem>().item;
        Assert.IsFalse(pizzaSO.stoveFry);
    }
}
