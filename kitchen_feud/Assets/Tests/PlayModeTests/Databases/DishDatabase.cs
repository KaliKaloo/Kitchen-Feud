using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Photon.Pun;
using UnityEngine.TestTools;
using System.IO;

public class GetDishFromIngredients : PhotonTestSetup
{

    [Test]
    public void getIngredientsMushroomSoup()
    {
        GameObject potato = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "potato"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO potatoSO = potato.GetComponent<IngredientItem>().item;
        GameObject mushroom = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "mushroom"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO mushroomSO = mushroom.GetComponent<IngredientItem>().item;
        List<IngredientSO> ingredients = new List<IngredientSO>{potatoSO, mushroomSO};
        DishSO foundDish =  Database.GetDishFromIngredients(ingredients);
        GameObject mushroomSoup = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Mushroom Soup"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO mushroomSoupSO = (DishSO) mushroomSoup.GetComponent<pickableItem>().item;
        Assert.AreEqual(mushroomSoupSO, foundDish);
    }


    [Test]
    public void getIngredientsBurger()
    {
        GameObject patty = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "patty"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO pattySO = patty.GetComponent<IngredientItem>().item;
        GameObject buns = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "buns"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO bunsSO = buns.GetComponent<IngredientItem>().item;
        List<IngredientSO> ingredients = new List<IngredientSO>{pattySO, bunsSO};
        DishSO foundDish =  Database.GetDishFromIngredients(ingredients);
        GameObject burger = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Burger"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO burgerSO = (DishSO) burger.GetComponent<pickableItem>().item;
        Assert.AreEqual(burgerSO, foundDish);
    }

    [Test]
    public void getIngredientsChips()
    {
        GameObject potato = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "potato"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO potatoSO = potato.GetComponent<IngredientItem>().item;
        List<IngredientSO> ingredients = new List<IngredientSO>{potatoSO};
        DishSO foundDish =  Database.GetDishFromIngredients(ingredients);
        GameObject chips = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Chips"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO chipsSO = (DishSO) chips.GetComponent<pickableItem>().item;
        Assert.AreEqual(chipsSO, foundDish);
    }


    [Test]
    public void getIngredientsEggRice()
    {
        GameObject egg = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "Egg"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO eggSO = egg.GetComponent<IngredientItem>().item;
        GameObject rice = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "rice"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO riceSO = rice.GetComponent<IngredientItem>().item;
        List<IngredientSO> ingredients = new List<IngredientSO>{eggSO, riceSO};
        DishSO foundDish =  Database.GetDishFromIngredients(ingredients);
        GameObject EggRice = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Egg-fried rice"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO EggRiceSO = (DishSO) EggRice.GetComponent<pickableItem>().item;
        Assert.AreEqual(EggRiceSO, foundDish);
    }


    [Test]
    public void getIngredientsEggyBread()
    {
        GameObject egg = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "Egg"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO eggSO = egg.GetComponent<IngredientItem>().item;
        GameObject bread = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "bread"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO breadSO = bread.GetComponent<IngredientItem>().item;
        List<IngredientSO> ingredients = new List<IngredientSO>{eggSO, breadSO};
        DishSO foundDish =  Database.GetDishFromIngredients(ingredients);
        GameObject eggyBread = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Eggy Bread"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO eggyBreadSO = (DishSO) eggyBread.GetComponent<pickableItem>().item;
        Assert.AreEqual(eggyBreadSO, foundDish);
    }

    [Test]
    public void getIngredientsSalad()
    {
        GameObject tomato = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "tomato"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO tomatoSO = tomato.GetComponent<IngredientItem>().item;
        GameObject lettuce = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "lettuce"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO lettuceSO = lettuce.GetComponent<IngredientItem>().item;
        GameObject cucumber = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "cucumber 1"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO cucumberSO = cucumber.GetComponent<IngredientItem>().item;
        List<IngredientSO> ingredients = new List<IngredientSO>{tomatoSO, lettuceSO, cucumberSO};
        DishSO foundDish =  Database.GetDishFromIngredients(ingredients);
        GameObject salad = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Salad"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO saladSO = (DishSO) salad.GetComponent<pickableItem>().item;
        Assert.AreEqual(saladSO, foundDish);
    }
    
    [Test]
    public void getIngredientsSandwich()
    {
        GameObject bread = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "bread"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO breadSO = bread.GetComponent<IngredientItem>().item;
        GameObject tomato = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "tomato"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO tomatoSO = tomato.GetComponent<IngredientItem>().item;
        GameObject lettuce = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "lettuce"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO lettuceSO = lettuce.GetComponent<IngredientItem>().item;
        List<IngredientSO> ingredients = new List<IngredientSO>{breadSO, tomatoSO, lettuceSO};
        DishSO foundDish =  Database.GetDishFromIngredients(ingredients);
        GameObject sandwich = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Sandwich"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO sandwichSO = (DishSO) sandwich.GetComponent<pickableItem>().item;
        Assert.AreEqual(sandwichSO, foundDish);
    }


    [Test]
    public void getIngredientsTomatoSoup()
    {
        GameObject potato = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "potato"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO potatoSO = potato.GetComponent<IngredientItem>().item;
        GameObject tomato = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "tomato"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO tomatoSO = tomato.GetComponent<IngredientItem>().item;
        List<IngredientSO> ingredients = new List<IngredientSO>{potatoSO, tomatoSO};
        DishSO foundDish =  Database.GetDishFromIngredients(ingredients);
        GameObject tomatoSoup = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Tomato Soup"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO tomatoSoupSO = (DishSO) tomatoSoup.GetComponent<pickableItem>().item;
        Assert.AreEqual(tomatoSoupSO, foundDish);
    }

    [Test]
    public void getIngredientsMushroomRisotto()
    {
        GameObject rice = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "rice"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO riceSO = rice.GetComponent<IngredientItem>().item;
        GameObject mushroom = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "mushroom"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO mushroomSO = mushroom.GetComponent<IngredientItem>().item;
        List<IngredientSO> ingredients = new List<IngredientSO>{mushroomSO, riceSO};
        DishSO foundDish =  Database.GetDishFromIngredients(ingredients);
        GameObject mushroomRisotto = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Mushroom rissotto"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO mushroomRisottoSO = (DishSO) mushroomRisotto.GetComponent<pickableItem>().item;
        Assert.AreEqual(mushroomRisottoSO, foundDish);
    }


    [Test]
    public void getIngredientsPasta()
    {
        GameObject garlic = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "garlic"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO garlicSO = garlic.GetComponent<IngredientItem>().item;
        GameObject marinara = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "sauce"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO marinaraSO = marinara.GetComponent<IngredientItem>().item;
        GameObject rawPasta = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "rawPasta"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO rawPastaSO = rawPasta.GetComponent<IngredientItem>().item;
        List<IngredientSO> ingredients = new List<IngredientSO>{garlicSO, marinaraSO, rawPastaSO};
        DishSO foundDish =  Database.GetDishFromIngredients(ingredients);
        GameObject pasta = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Pasta"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO pastaSO = (DishSO) pasta.GetComponent<pickableItem>().item;
        Assert.AreEqual(pastaSO, foundDish);
    }
    
    
    [Test]
    public void getIngredientsFruitSalad()
    {
        GameObject apple = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "apple"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO appleSO = apple.GetComponent<IngredientItem>().item;
        GameObject orange = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "orange"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO orangeSO = orange.GetComponent<IngredientItem>().item;
        GameObject kiwi = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "kiwi"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO kiwiSO = kiwi.GetComponent<IngredientItem>().item;
        List<IngredientSO> ingredients = new List<IngredientSO>{appleSO, orangeSO, kiwiSO};
        DishSO foundDish = Database.GetDishFromIngredients(ingredients);
        GameObject fruitSalad = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "fruitSalad"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO fruitSaladSO = (DishSO) fruitSalad.GetComponent<pickableItem>().item;
        Assert.AreEqual(fruitSaladSO, foundDish);
    }
    
    
    [Test]
    public void getIngredientsCake()
    {
        GameObject uncookedCake = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "unCookedCake"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO uncookedSO = uncookedCake.GetComponent<IngredientItem>().item;
        List<IngredientSO> ingredients = new List<IngredientSO>{uncookedSO};
        DishSO foundDish = Database.GetDishFromIngredients(ingredients);
        GameObject cake = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Cake"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO cakeSO = (DishSO) cake.GetComponent<pickableItem>().item;
        Assert.AreEqual(cakeSO, foundDish);
    }
    
    [Test]
    public void getIngredientsPancakes()
    {
        GameObject egg = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "Egg"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO eggSO = egg.GetComponent<IngredientItem>().item;
        GameObject flour = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "flour"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO flourSO = flour.GetComponent<IngredientItem>().item;
        GameObject milk = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "milk"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO milkSO = milk.GetComponent<IngredientItem>().item;
        List<IngredientSO> ingredients = new List<IngredientSO>{eggSO, flourSO, milkSO};
        DishSO foundDish =  Database.GetDishFromIngredients(ingredients);
        GameObject pancakes = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "Pancakes"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO pancakesSO = (DishSO) pancakes.GetComponent<pickableItem>().item;
        Assert.AreEqual(pancakesSO, foundDish);
    }
    
    
    [Test]
    public void getIngredientsEggMayo()
    {
        GameObject egg = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "Egg"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO eggSO = egg.GetComponent<IngredientItem>().item;
        GameObject mayo = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "mayo"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO mayoSO = mayo.GetComponent<IngredientItem>().item;
        GameObject bread = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "bread"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO breadSO = bread.GetComponent<IngredientItem>().item;
        List<IngredientSO> ingredients = new List<IngredientSO>{eggSO, mayoSO, breadSO};
        DishSO foundDish =  Database.GetDishFromIngredients(ingredients);
        GameObject eggMayo = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "EggMayo sandwich"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO eggMayoSO = (DishSO) eggMayo.GetComponent<pickableItem>().item;
        Assert.AreEqual(eggMayoSO, foundDish);
    }

    [Test]
    public void getIngredientsCheeseOnion()
    {
        GameObject bread = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "bread"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO breadSO = bread.GetComponent<IngredientItem>().item;
        GameObject cheese = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "cheese"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO cheeseSO = cheese.GetComponent<IngredientItem>().item;
        GameObject onion = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "onion"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO onionSO = onion.GetComponent<IngredientItem>().item;
        List<IngredientSO> ingredients = new List<IngredientSO>{breadSO, cheeseSO, onionSO};
        DishSO foundDish =  Database.GetDishFromIngredients(ingredients);
        GameObject cheeseOnion = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "CheeseOnion sandwich"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO cheeseOnionSO = (DishSO) cheeseOnion.GetComponent<pickableItem>().item;
        Assert.AreEqual(cheeseOnionSO, foundDish);
    }

    [Test]
    public void getIngredientsPBJ()
    {
        GameObject bread = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "bread"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO breadSO = bread.GetComponent<IngredientItem>().item;
        GameObject jam = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "jam"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO jamSO = jam.GetComponent<IngredientItem>().item;
        GameObject peanutButter = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "peanutButter"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO peanutButterSO = peanutButter.GetComponent<IngredientItem>().item;
        List<IngredientSO> ingredients = new List<IngredientSO>{breadSO, jamSO, peanutButterSO};
        DishSO foundDish =  Database.GetDishFromIngredients(ingredients);
        GameObject PBJ = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "PBJ_sandwich"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO PBJSO = (DishSO) PBJ.GetComponent<pickableItem>().item;
        Assert.AreEqual(PBJSO, foundDish);
    }

    [Test]
    public void getIngredientsPizza()
    {
        GameObject dough = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "dough"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO doughSO = dough.GetComponent<IngredientItem>().item;
        GameObject marinara = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "sauce"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO marinaraSO = marinara.GetComponent<IngredientItem>().item;
         GameObject cheese = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "cheese"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO cheeseSO = cheese.GetComponent<IngredientItem>().item;
        List<IngredientSO> ingredients = new List<IngredientSO>{doughSO, marinaraSO, cheeseSO};
        DishSO foundDish =  Database.GetDishFromIngredients(ingredients);
        GameObject pizza = PhotonNetwork.Instantiate(Path.Combine("DishPrefabs", "pizza"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        DishSO pizzaSO = (DishSO) pizza.GetComponent<pickableItem>().item;
        Assert.AreEqual(pizzaSO, foundDish);
    }



    [Test]
    public void getIngredientsNotADish()
    {
        GameObject dough = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "dough"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO doughSO = dough.GetComponent<IngredientItem>().item;
        GameObject jam = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "jam"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO jamSO = jam.GetComponent<IngredientItem>().item;
        List<IngredientSO> ingredients = new List<IngredientSO>{doughSO, jamSO};
        DishSO foundDish =  Database.GetDishFromIngredients(ingredients);
        Assert.IsNull(foundDish);
    }


    [Test]
    public void getIngredientsDishAdditionalIngredient()
    {
        GameObject jam = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "jam"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO jamSO = jam.GetComponent<IngredientItem>().item;
        GameObject uncookedCake = PhotonNetwork.Instantiate(Path.Combine("IngredientPrefabs", "unCookedCake"), new Vector3(-1.98f, 0.006363153f, -8.37f), Quaternion.identity);
        IngredientSO uncookedSO = uncookedCake.GetComponent<IngredientItem>().item;
        List<IngredientSO> ingredients = new List<IngredientSO>{jamSO, uncookedSO};
        DishSO foundDish = Database.GetDishFromIngredients(ingredients);
        Assert.IsNull(foundDish);
    }

}