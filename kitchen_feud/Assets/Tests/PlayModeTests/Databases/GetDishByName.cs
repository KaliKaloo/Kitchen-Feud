using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Photon.Pun;
using UnityEngine.TestTools;
using System.IO;
public class GetDishByName : PhotonTestSetup
{
      [Test]
    public void getMushroomSoupByName()
    {
        GameObject mushroomSoup = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Mushroom Soup"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO mushroomSoupSO = (DishSO) mushroomSoup.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByName("Mushroom Soup");
        Assert.AreEqual(mushroomSoupSO, foundDish);
    }


    [Test]
    public void getBurgerByName()
    {
        GameObject burger = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Burger"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO burgerSO = (DishSO) burger.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByName("Burger");
        Assert.AreEqual(burgerSO, foundDish);
    }

    [Test]
    public void getChipsByName()
    {
        GameObject chips = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Chips"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO chipsSO = (DishSO) chips.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByName("Chips");
        Assert.AreEqual(chipsSO, foundDish);

    }


    [Test]
    public void getEggRiceByName()
    {
        GameObject EggRice = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Egg-fried rice"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO EggRiceSO = (DishSO) EggRice.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByName("Egg-fried Rice");
        Assert.AreEqual(EggRiceSO, foundDish);
    }


    [Test]
    public void getEggyBreadByName()
    {
        GameObject eggyBread = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Eggy Bread"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO eggyBreadSO = (DishSO) eggyBread.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByName("Eggy Bread");
        Assert.AreEqual(eggyBreadSO, foundDish);
    }

    
    [Test]
    public void getOmeletteByName()
    {
        GameObject omelette = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Omelette"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO omeletteSO = (DishSO) omelette.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByName("Omelette");
        Assert.AreEqual(omeletteSO, foundDish);
    }

    [Test]
    public void getSaladByName()
    {
        GameObject salad = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Salad"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO saladSO = (DishSO) salad.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByName("Salad");
        Assert.AreEqual(saladSO, foundDish);
    }
    
    [Test]
    public void getSandwichByName()
    {
        GameObject sandwich = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Sandwich"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO sandwichSO = (DishSO) sandwich.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByName("Sandwich");
        Assert.AreEqual(sandwichSO, foundDish);
    }


    [Test]
    public void getTomatoSoupByName()
    {
        GameObject tomatoSoup = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Tomato Soup"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO tomatoSoupSO = (DishSO) tomatoSoup.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByName("Tomato Soup");
        Assert.AreEqual(tomatoSoupSO, foundDish);
    }

    [Test]
    public void getMushroomRisottoByName()
    {
        GameObject mushroomRisotto = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Mushroom rissotto"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO mushroomRisottoSO = (DishSO) mushroomRisotto.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByName("Mushroom Risotto");
        Assert.AreEqual(mushroomRisottoSO, foundDish);
    }


    [Test]
    public void getPastaByName()
    {
        GameObject pasta = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Pasta"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO pastaSO = (DishSO) pasta.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByName("Pasta");
        Assert.AreEqual(pastaSO, foundDish);
    }
    
    
    [Test]
    public void getFruitSaladByName()
    {
        GameObject fruitSalad = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "fruitSalad"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO fruitSaladSO = (DishSO) fruitSalad.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByName("Fruit Salad");
        Assert.AreEqual(fruitSaladSO, foundDish);
    }
    
    
    [Test]
    public void getCakeByName()
    {
        GameObject cake = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Cake"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO cakeSO = (DishSO) cake.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByName("Cake");
        Assert.AreEqual(cakeSO, foundDish);
    }
    
    [Test]
    public void getPancakesByName()
    {
        GameObject pancakes = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Pancakes"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO pancakesSO = (DishSO) pancakes.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByName("Pancakes");
        Assert.AreEqual(pancakesSO, foundDish);
    }
    
    
    [Test]
    public void getEggMayoByName()
    {
        GameObject eggMayo = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "EggMayo sandwich"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO eggMayoSO = (DishSO) eggMayo.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByName("Egg-Mayo Sandwich");
        Assert.AreEqual(eggMayoSO, foundDish);
    }

    [Test]
    public void getCheeseOnionByName()
    {
        GameObject cheeseOnion = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "CheeseOnion sandwich"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO cheeseOnionSO = (DishSO) cheeseOnion.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByName("Cheese-Onion Sandwich");
        Assert.AreEqual(cheeseOnionSO, foundDish);
    }

    [Test]
    public void getPBJByName()
    {
        GameObject PBJ = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "PBJ_sandwich"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO PBJSO = (DishSO) PBJ.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByName("PB and Jelly Sandwich");
        Assert.AreEqual(PBJSO, foundDish);
    }

    [Test]
    public void getPizzaByName()
    {
        GameObject pizza = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "pizza"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO pizzaSO = (DishSO) pizza.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByName("Pizza");
        Assert.AreEqual(pizzaSO, foundDish);
    } 

    [Test]
    public void getNullDishByName()
    {
        DishSO foundDish =  Database.GetDishByName("not a dish");
        Assert.IsNull(foundDish);
    } 
}
