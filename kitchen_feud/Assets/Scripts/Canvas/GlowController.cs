using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowController : MonoBehaviour
{
    public bool newClick = false;
    public bool firstClick = false;
    public string currentGlowingDishName;

    public DishSO dishInFocus;

    public void GlowAll(string dishName){
        
        if ((newClick == true) && (!firstClick)){
            StopGlowingAll(currentGlowingDishName);
            Debug.Log("tostop:" + currentGlowingDishName + "just clicked: " + dishName);
            newClick = false;
        }
        
        if (firstClick == true){
            firstClick = false;
            newClick = false;
        }

        Debug.Log("dishname v=bf db" + dishName);
        dishInFocus = Database.GetDishByName(dishName);
        currentGlowingDishName = dishName;
        
        GlowAppliance(dishName);
        GlowLocation(dishInFocus);
    }

    public GameObject[] appliancesToGlow;
    public GameObject glowAppliance;
    
    public void GlowAppliance(string dishName){
        string dishTag = dishInFocus.toCook;
        appliancesToGlow = GameObject.FindGameObjectsWithTag(dishTag);
        int teamNumber = gameObject.GetComponent<CanvasController>().teamNumber;
        
        foreach (GameObject glowAppliance in appliancesToGlow)
        {
           int kitchenNum = glowAppliance.GetComponent<Appliance>().kitchenNum;

           if (kitchenNum == teamNumber){
                if (glowAppliance.GetComponent<OutlineEffect>()){
                        glowAppliance.GetComponent<OutlineEffect>().enabled = true;
                        //glowAppliance.GetComponent<OutlineEffect>().startGlowing();
                    }
           }
        }
    }
    
    public GameObject IngredientSpawner;
    public string glowLocation;
   
    public void GlowLocation(DishSO dishInFocus){
    
        foreach (IngredientSO ingredient in dishInFocus.recipe)
            {   
                GameObject location = GameObject.Find(ingredient.location.ToString());
                
                Debug.Log(location.name);
                ParticleSystem PS = location.GetComponentInChildren<ParticleSystem>();
                var main = PS.main;
                main.startColor = new Color(243,255,28,255);
                
                // Debug.Log(ingredient.name);
                // string spawnerName = ingredient.name.ToString();
                // string name = spawnerName + "Spawner";
                // Debug.Log(name);
                
                // foreach (Transform child in location.transform){
                //     foreach (Transform childInChild in child){
                //         if (childInChild.name == name){
                        
                //         IngredientSpawner = child.gameObject;
                //         GlowIngredient(IngredientSpawner);
                        
                //         Debug.Log(IngredientSpawner.name);
                //     }
                //     }
                    
                // }
                
            }
    }

    // public void GlowIngredient(GameObject IngredientSpawnerToGlow){
    //     OutlineEffect ce = IngredientSpawnerToGlow.GetComponent<OutlineEffect>();
    //     if (!ce){
    //         ce.enabled = true;
    //     }
    // }

    public void RecipeCardClose(){
        firstClick = true;
        StopGlowingAll(currentGlowingDishName);
    }

    public void StopGlowingAll(string dishNameToStop){
        Debug.Log(dishNameToStop + "stop glowing");

        foreach (GameObject glowAppliance in appliancesToGlow)
        {
           if (glowAppliance.GetComponent<OutlineEffect>()){
                OutlineEffect outlineScript = glowAppliance.GetComponent<OutlineEffect>();
                //outlineScript.enabled = false;
                outlineScript.destroyClone();
            }
    
           
        }

        foreach (IngredientSO ingredient in dishInFocus.recipe)
            {   
                //Debug.Log(dishInFocus.recipe.Count);
                GameObject location = GameObject.Find(ingredient.location.ToString());
               // Debug.Log(location.name);
                ParticleSystem PS1 = location.GetComponentInChildren<ParticleSystem>();
                Transform PSChild = PS1.gameObject.transform.GetChild(2);;
                ParticleSystem PS2 = PSChild.GetComponentInChildren<ParticleSystem>();
                var main = PS1.main;
                var main2  = PS2.main;

                main.startColor = main2.startColor;

                PS1.Simulate(0.0f,true, true);
                PS1.Play();
                
            
            }
    }
   
}
