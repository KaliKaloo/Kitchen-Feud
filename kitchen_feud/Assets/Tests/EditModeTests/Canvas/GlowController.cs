using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GlowControllerTests
{
    GlowController glowController;

    [SetUp]
    public void setUp(){
        GameObject obj = new GameObject();
        glowController = obj.AddComponent<GlowController>();
        glowController.currentGlowingDishName = "dish";
        glowController.dishInFocus = ScriptableObject.CreateInstance<DishSO>();
        glowController.dishInFocus.toCook = "Dish";

    }

    [Test]
    public void InitGlow()
    {
        Assert.IsFalse(glowController.firstClick);
    }


    [Test]
    public void GlowAllCorrectDish()
    {
        glowController.currentGlowingDishName = "dish";
        glowController.GlowAll("dish");
        Assert.IsFalse(glowController.newClick);
    }

    [Test]
    public void singleListAppliancesToGlow()
    {
        GameObject obj = new GameObject();
        obj.tag = "Dish";
        GameObject[] objs = {obj};
        glowController.getListOfAppliancesToGlow("new dish");
        Assert.AreEqual("new dish", glowController.currentGlowingDishName);
        Assert.AreEqual(objs, glowController.appliancesToGlow);
    }


    [Test]
    public void multipleListAppliancesToGlow()
    {

        GameObject obj1 = new GameObject();
        obj1.tag = "Dish";
        GameObject obj2 = new GameObject();
        obj2.tag = "Dish";
        GameObject obj3 = new GameObject();
        obj3.tag = "Bullet";
        GameObject[] objs = {obj1, obj2};
        glowController.getListOfAppliancesToGlow("new dish");

        Assert.AreEqual("new dish", glowController.currentGlowingDishName);
        Assert.AreEqual(objs, glowController.appliancesToGlow);
        obj1.tag = "Bullet";
        obj2.tag = "Bullet";
    }



    [Test]
    public void getListAppliancesToGlowTwice()
    {
        GameObject obj1 = new GameObject();
        obj1.tag = "Dish";
        GameObject obj2 = new GameObject();
        obj2.tag = "Dish";
        GameObject obj3 = new GameObject();
        obj3.tag = "Bomb";
        GameObject[] objs = {obj1, obj2, obj3};
        glowController.getListOfAppliancesToGlow("dish");
        glowController.dishInFocus.toCook = "Bomb";

        glowController.getListOfAppliancesToGlow("new dish");
        Assert.AreEqual("new dish", glowController.currentGlowingDishName);
        Assert.AreEqual(objs, glowController.appliancesToGlow);
        obj1.tag = "Bullet";
        obj2.tag = "Bullet";
        obj3.tag = "Bullet";

    }


    [Test]
    public void GlowAppliance()
    {
        glowController.teamNumber = 1;
        GameObject obj1 = new GameObject();
        obj1.AddComponent<Appliance>();
        obj1.GetComponent<Appliance>().kitchenNum = 1;
       
        OutlineEffect outlineEffect = obj1.AddComponent<OutlineEffect>();
        outlineEffect.outlineObject = new GameObject();
        outlineEffect.outlineObject.AddComponent<MeshRenderer>();
        outlineEffect.outlineObject.GetComponent<MeshRenderer>().enabled = false;
        List<GameObject> appliances = new List<GameObject> {obj1};
        glowController.GlowAppliance(appliances);

        Assert.IsTrue(outlineEffect.outlineObject.GetComponent<Renderer>().enabled);
    }

    [Test]
    public void GlowMultipleAppliances()
    {
        glowController.teamNumber = 1;
        GameObject obj1 = new GameObject();
        obj1.AddComponent<Appliance>();
        obj1.GetComponent<Appliance>().kitchenNum = 1;
       
        OutlineEffect outlineEffect1 = obj1.AddComponent<OutlineEffect>();
        outlineEffect1.outlineObject = new GameObject();
        outlineEffect1.outlineObject.AddComponent<MeshRenderer>();
        outlineEffect1.outlineObject.GetComponent<MeshRenderer>().enabled = false;

        GameObject obj2 = new GameObject();
        obj2.AddComponent<Appliance>();
        obj2.GetComponent<Appliance>().kitchenNum = 1;
       
        OutlineEffect outlineEffect2 = obj2.AddComponent<OutlineEffect>();
        outlineEffect2.outlineObject = new GameObject();
        outlineEffect2.outlineObject.AddComponent<MeshRenderer>();
        outlineEffect2.outlineObject.GetComponent<MeshRenderer>().enabled = false;
        List<GameObject> appliances = new List<GameObject> {obj1, obj2};
        glowController.GlowAppliance(appliances);

        Assert.IsTrue(outlineEffect1.outlineObject.GetComponent<Renderer>().enabled);
        Assert.IsTrue(outlineEffect2.outlineObject.GetComponent<Renderer>().enabled);

    }


    [Test]
    public void GlowApplianceWrongTeam()
    {
        glowController.teamNumber = 2;
        GameObject obj1 = new GameObject();
        obj1.AddComponent<Appliance>();
        obj1.GetComponent<Appliance>().kitchenNum = 1;
       
        OutlineEffect outlineEffect = obj1.AddComponent<OutlineEffect>();
        outlineEffect.outlineObject = new GameObject();
        outlineEffect.outlineObject.AddComponent<MeshRenderer>();
        outlineEffect.outlineObject.GetComponent<MeshRenderer>().enabled = false;
        List<GameObject> appliances = new List<GameObject> {obj1};
        glowController.GlowAppliance(appliances);

        Assert.IsFalse(outlineEffect.outlineObject.GetComponent<Renderer>().enabled);
    }

    [Test]
    public void GlowMixedAppliances()
    {
        glowController.teamNumber = 1;
        GameObject obj1 = new GameObject();
        obj1.AddComponent<Appliance>();
        obj1.GetComponent<Appliance>().kitchenNum = 1;
       
        OutlineEffect outlineEffect1 = obj1.AddComponent<OutlineEffect>();
        outlineEffect1.outlineObject = new GameObject();
        outlineEffect1.outlineObject.AddComponent<MeshRenderer>();
        outlineEffect1.outlineObject.GetComponent<MeshRenderer>().enabled = false;

        GameObject obj2 = new GameObject();
        obj2.AddComponent<Appliance>();
        obj2.GetComponent<Appliance>().kitchenNum = 2;
       
        OutlineEffect outlineEffect2 = obj2.AddComponent<OutlineEffect>();
        outlineEffect2.outlineObject = new GameObject();
        outlineEffect2.outlineObject.AddComponent<MeshRenderer>();
        outlineEffect2.outlineObject.GetComponent<MeshRenderer>().enabled = false;
        List<GameObject> appliances = new List<GameObject> {obj1, obj2};
        glowController.GlowAppliance(appliances);

        Assert.IsTrue(outlineEffect1.outlineObject.GetComponent<Renderer>().enabled);
        Assert.IsFalse(outlineEffect2.outlineObject.GetComponent<Renderer>().enabled);

    }

    [Test]
    public void getSingleLocation()
    {
        DishSO dish = ScriptableObject.CreateInstance<DishSO>();
        IngredientSO ingredient1 = ScriptableObject.CreateInstance<IngredientSO>();
        ingredient1.location = Location.Freezer;
        dish.recipe = new List<IngredientSO> {ingredient1};
        GameObject room1 = new GameObject();
        room1.name = "Freezer"; 
        glowController.kitchenPortals = new GameObject[]{room1};
        List<GameObject> locations = glowController.getLocation(dish);
        List<GameObject> rooms = new List<GameObject>{room1};
        Assert.AreEqual(rooms, locations);
    }

    [Test]
    public void getLocationNoMatch()
    {
        DishSO dish = ScriptableObject.CreateInstance<DishSO>();
        IngredientSO ingredient1 = ScriptableObject.CreateInstance<IngredientSO>();
        ingredient1.location = Location.Freezer;
        dish.recipe = new List<IngredientSO> {ingredient1};
        GameObject room1 = new GameObject();
        room1.name = "Fridge"; 
        glowController.kitchenPortals = new GameObject[]{room1};
        List<GameObject> locations = glowController.getLocation(dish);
        List<GameObject> rooms = new List<GameObject>{};
        Assert.AreEqual(rooms, locations);
    }

    [Test]
    public void getMultipleLocations()
    {
        DishSO dish = ScriptableObject.CreateInstance<DishSO>();
        IngredientSO ingredient1 = ScriptableObject.CreateInstance<IngredientSO>();
        ingredient1.location = Location.Fridge;
        IngredientSO ingredient2 = ScriptableObject.CreateInstance<IngredientSO>();
        ingredient2.location = Location.Freezer;
        IngredientSO ingredient3 = ScriptableObject.CreateInstance<IngredientSO>();
        ingredient3.location = Location.Pantry;

        dish.recipe = new List<IngredientSO> {ingredient1, ingredient2, ingredient3};
        GameObject room1 = new GameObject();
        room1.name = "Freezer"; 
        glowController.kitchenPortals = new GameObject[]{room1};
        List<GameObject> locations = glowController.getLocation(dish);
        List<GameObject> rooms = new List<GameObject>{room1};

        Assert.AreEqual(rooms, locations);
    }
    [Test]
    public void getDifferentLocations()
    {
        DishSO dish = ScriptableObject.CreateInstance<DishSO>();
        IngredientSO ingredient1 = ScriptableObject.CreateInstance<IngredientSO>();
        ingredient1.location = Location.Freezer;
        IngredientSO ingredient2 = ScriptableObject.CreateInstance<IngredientSO>();
        ingredient2.location = Location.Fridge;
        IngredientSO ingredient3 = ScriptableObject.CreateInstance<IngredientSO>();
        ingredient3.location = Location.Pantry;

        dish.recipe = new List<IngredientSO> {ingredient1, ingredient2, ingredient3};
        GameObject room1 = new GameObject();
        room1.name = "Freezer"; 
        GameObject room2 = new GameObject();
        room2.name = "Fridge"; 
        GameObject room3 = new GameObject();
        room3.name = "Pantry"; 
        glowController.kitchenPortals = new GameObject[]{room1, room2, room3};
        List<GameObject> locations = glowController.getLocation(dish);
        List<GameObject> rooms = new List<GameObject>{room1, room2, room3};

        Assert.AreEqual(rooms, locations);
    }

    [Test]
    public void getDifferentRepeatedLocations()
    {
        DishSO dish = ScriptableObject.CreateInstance<DishSO>();
        IngredientSO ingredient1 = ScriptableObject.CreateInstance<IngredientSO>();
        ingredient1.location = Location.Freezer;
        IngredientSO ingredient2 = ScriptableObject.CreateInstance<IngredientSO>();
        ingredient2.location = Location.Fridge;
        IngredientSO ingredient3 = ScriptableObject.CreateInstance<IngredientSO>();
        ingredient3.location = Location.Freezer;

        dish.recipe = new List<IngredientSO> {ingredient1, ingredient2, ingredient3};
        GameObject room1 = new GameObject();
        room1.name = "Freezer"; 
        GameObject room2 = new GameObject();
        room2.name = "Fridge"; 
        glowController.kitchenPortals = new GameObject[]{room1, room2};
        List<GameObject> locations = glowController.getLocation(dish);
        List<GameObject> rooms = new List<GameObject>{room1, room2, room1};

        Assert.AreEqual(rooms, locations);
    }


    [Test]
    public void RecipeCardClose()
    {
        glowController.RecipeCardClose();
        Assert.IsTrue(glowController.firstClick);
    }


   


    [Test]
    public void StopGlowingAllSingle()
    {
        GameObject obj1 = new GameObject();
        obj1.AddComponent<Appliance>();
        OutlineEffect outlineEffect = obj1.AddComponent<OutlineEffect>();
        outlineEffect.outlineObject = new GameObject();
        outlineEffect.outlineObject.AddComponent<MeshRenderer>();
        outlineEffect.outlineObject.GetComponent<MeshRenderer>().enabled = false;
        obj1.GetComponent<Appliance>().kitchenNum = 1;
        List <GameObject> objs = new List <GameObject> {obj1};
        glowController.appliancesToGlow = objs;
        glowController.teamNumber = 1;
        glowController.StopGlowingAll("dish");

        Assert.IsFalse(outlineEffect.outlineObject.GetComponent<Renderer>().enabled);
        Assert.AreEqual(0, glowController.appliancesToGlow.Count);
        Assert.AreEqual("", glowController.currentGlowingDishName);

    }


    [Test]
    public void StopGlowingAllSingleWrongTeam()
    {
        GameObject obj1 = new GameObject();
        obj1.AddComponent<Appliance>();
        OutlineEffect outlineEffect = obj1.AddComponent<OutlineEffect>();
        outlineEffect.outlineObject = new GameObject();
        outlineEffect.outlineObject.AddComponent<MeshRenderer>();
        outlineEffect.outlineObject.GetComponent<MeshRenderer>().enabled = true;
        obj1.GetComponent<Appliance>().kitchenNum = 1;
        List <GameObject> objs = new List <GameObject> {obj1};
        glowController.appliancesToGlow = objs;
        glowController.teamNumber = 2;
        glowController.StopGlowingAll("dish");

        Assert.IsTrue(outlineEffect.outlineObject.GetComponent<Renderer>().enabled);
        Assert.AreEqual(0, glowController.appliancesToGlow.Count);
        Assert.AreEqual("", glowController.currentGlowingDishName);

    }




    [Test]
    public void StopGlowingAllMultiple()
    {
        GameObject obj1 = new GameObject();
        obj1.AddComponent<Appliance>();
        OutlineEffect outlineEffect1 = obj1.AddComponent<OutlineEffect>();
        outlineEffect1.outlineObject = new GameObject();
        outlineEffect1.outlineObject.AddComponent<MeshRenderer>();
        outlineEffect1.outlineObject.GetComponent<MeshRenderer>().enabled = true;
        obj1.GetComponent<Appliance>().kitchenNum = 1;
        GameObject obj2 = new GameObject();
        obj2.AddComponent<Appliance>();
        OutlineEffect outlineEffect2 = obj2.AddComponent<OutlineEffect>();
        outlineEffect2.outlineObject = new GameObject();
        outlineEffect2.outlineObject.AddComponent<MeshRenderer>();
        outlineEffect2.outlineObject.GetComponent<MeshRenderer>().enabled = true;
        obj2.GetComponent<Appliance>().kitchenNum = 1;
        
        List <GameObject> objs = new List <GameObject> {obj1, obj2};
        glowController.appliancesToGlow = objs;
        glowController.teamNumber = 1;
        glowController.StopGlowingAll("dish");

        Assert.IsFalse(outlineEffect1.outlineObject.GetComponent<Renderer>().enabled);
        Assert.IsFalse(outlineEffect2.outlineObject.GetComponent<Renderer>().enabled);

        Assert.AreEqual(0, glowController.appliancesToGlow.Count);
        Assert.AreEqual("", glowController.currentGlowingDishName);

    }

    [Test]
    public void StopGlowingAllMultipleMixedTeams()
    {
        GameObject obj1 = new GameObject();
        obj1.AddComponent<Appliance>();
        OutlineEffect outlineEffect1 = obj1.AddComponent<OutlineEffect>();
        outlineEffect1.outlineObject = new GameObject();
        outlineEffect1.outlineObject.AddComponent<MeshRenderer>();
        outlineEffect1.outlineObject.GetComponent<MeshRenderer>().enabled = true;
        obj1.GetComponent<Appliance>().kitchenNum = 2;
        GameObject obj2 = new GameObject();
        obj2.AddComponent<Appliance>();
        OutlineEffect outlineEffect2 = obj2.AddComponent<OutlineEffect>();
        outlineEffect2.outlineObject = new GameObject();
        outlineEffect2.outlineObject.AddComponent<MeshRenderer>();
        outlineEffect2.outlineObject.GetComponent<MeshRenderer>().enabled = true;
        obj2.GetComponent<Appliance>().kitchenNum = 1;
        
        List <GameObject> objs = new List <GameObject> {obj1, obj2};
        glowController.appliancesToGlow = objs;
        glowController.teamNumber = 1;
        glowController.StopGlowingAll("dish");

        Assert.IsTrue(outlineEffect1.outlineObject.GetComponent<Renderer>().enabled);
        Assert.IsFalse(outlineEffect2.outlineObject.GetComponent<Renderer>().enabled);

        Assert.AreEqual(0, glowController.appliancesToGlow.Count);
        Assert.AreEqual("", glowController.currentGlowingDishName);

    }



  
  
}
