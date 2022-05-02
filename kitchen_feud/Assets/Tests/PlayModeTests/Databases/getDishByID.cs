using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Photon.Pun;
using UnityEngine.TestTools;
using System.IO;
public class GetDishByID : PhotonTestSetup
{
   [Test]
    public void getMushroomSoupByID()
    {
        GameObject mushroomSoup = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Mushroom Soup"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO mushroomSoupSO = (DishSO) mushroomSoup.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByID("DI1112");
        Assert.AreEqual(mushroomSoupSO, foundDish);
    }


    [Test]
    public void getBurgerByID()
    {
        GameObject burger = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Burger"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO burgerSO = (DishSO) burger.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByID("DI1415");
        Assert.AreEqual(burgerSO, foundDish);
    }

    [Test]
    public void getChipsByID()
    {
        GameObject chips = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Chips"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO chipsSO = (DishSO) chips.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByID("DI12");
        Assert.AreEqual(chipsSO, foundDish);

    }


    [Test]
    public void getEggRiceByID()
    {
        GameObject EggRice = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Egg-fried rice"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO EggRiceSO = (DishSO) EggRice.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByID("DI1621");
        Assert.AreEqual(EggRiceSO, foundDish);
    }


    [Test]
    public void getEggyBreadByID()
    {
        GameObject eggyBread = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Eggy Bread"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO eggyBreadSO = (DishSO) eggyBread.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByID("DI1316");
        Assert.AreEqual(eggyBreadSO, foundDish);
    }

    [Test]
    public void getOmeletteByID()
    {
        GameObject omelette = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Omelette"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO omeletteSO = (DishSO) omelette.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByID("DI1617");
        Assert.AreEqual(omeletteSO, foundDish);
    }

    [Test]
    public void getSaladByID()
    {
        GameObject salad = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Salad"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO saladSO = (DishSO) salad.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByID("DI181924");
        Assert.AreEqual(saladSO, foundDish);
    }
    
    [Test]
    public void getSandwichByID()
    {
        GameObject sandwich = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Sandwich"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO sandwichSO = (DishSO) sandwich.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByID("DI131819");
        Assert.AreEqual(sandwichSO, foundDish);
    }


    [Test]
    public void getTomatoSoupByID()
    {
        GameObject tomatoSoup = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Tomato Soup"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO tomatoSoupSO = (DishSO) tomatoSoup.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByID("DI1218");
        Assert.AreEqual(tomatoSoupSO, foundDish);
    }

    [Test]
    public void getMushroomRisottoByID()
    {
        GameObject mushroomRisotto = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Mushroom rissotto"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO mushroomRisottoSO = (DishSO) mushroomRisotto.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByID("DI1121");
        Assert.AreEqual(mushroomRisottoSO, foundDish);
    }


    [Test]
    public void getPastaByID()
    {
        GameObject pasta = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Pasta"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO pastaSO = (DishSO) pasta.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByID("DI262730");
        Assert.AreEqual(pastaSO, foundDish);
    }
    
    
    [Test]
    public void getFruitSaladByID()
    {
        GameObject fruitSalad = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "fruitSalad"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO fruitSaladSO = (DishSO) fruitSalad.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByID("DI222325");
        Assert.AreEqual(fruitSaladSO, foundDish);
    }
    
    
    [Test]
    public void getCakeByID()
    {
        GameObject cake = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Cake"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO cakeSO = (DishSO) cake.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByID("DI36");
        Assert.AreEqual(cakeSO, foundDish);
    }
    
    [Test]
    public void getPancakesByID()
    {
        GameObject pancakes = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Pancakes"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO pancakesSO = (DishSO) pancakes.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByID("DI163134");
        Assert.AreEqual(pancakesSO, foundDish);
    }
    
    
    [Test]
    public void getEggMayoByID()
    {
        GameObject eggMayo = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "EggMayo sandwich"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO eggMayoSO = (DishSO) eggMayo.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByID("DI131632");
        Assert.AreEqual(eggMayoSO, foundDish);
    }

    [Test]
    public void getCheeseOnionByID()
    {
        GameObject cheeseOnion = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "CheeseOnion sandwich"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO cheeseOnionSO = (DishSO) cheeseOnion.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByID("DI132033");
        Assert.AreEqual(cheeseOnionSO, foundDish);
    }

    [Test]
    public void getPBJByID()
    {
        GameObject PBJ = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "PBJ_sandwich"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO PBJSO = (DishSO) PBJ.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByID("DI132829");
        Assert.AreEqual(PBJSO, foundDish);
    }

    [Test]
    public void getPizzaByID()
    {
        GameObject pizza = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "pizza"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO pizzaSO = (DishSO) pizza.GetComponent<pickableItem>().item;
        DishSO foundDish =  Database.GetDishByID("DI273335");
        Assert.AreEqual(pizzaSO, foundDish);
    }

    [Test]
    public void getNullDishByID()
    {
        DishSO foundDish =  Database.GetDishByID("Not an ID");
        Assert.IsNull(foundDish);
    }
}
