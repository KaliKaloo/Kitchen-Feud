using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowController : MonoBehaviour
{
    public Database Database;
    public bool newClick;
    public bool firstClick = false;
    public string currentGlowingDishName;
    // public string dishName;
    public int teamNumber;
    private int kitchenNum;

    public DishSO dishInFocus;

    public void GlowAll(string dishName){

        if(dishName == currentGlowingDishName){
            newClick = false;
        }

        if (newClick == true){
            if ((firstClick == false)){
                StopGlowingAll(currentGlowingDishName);
            }

            firstClick = false;
            Debug.Log("new dish:" + dishName);
            dishInFocus = Database.GetDishByName(dishName);
            if (dishName != ""){appliancesToGlow = getListOfAppliancesToGlow(dishName);}
        
            GlowAppliance(appliancesToGlow);

            GlowLocation(dishInFocus);
            newClick = false;
        }

    }

    public List<GameObject> appliancesToGlow = new List<GameObject>(); 
    //public GameObject glowAppliance;

    public List<GameObject> getListOfAppliancesToGlow(string dishNameToGlow){
        teamNumber = gameObject.GetComponent<CanvasController>().teamNumber;
        currentGlowingDishName = dishNameToGlow ;

        string dishTag = dishInFocus.toCook;
    
        GameObject[] objs = GameObject.FindGameObjectsWithTag(dishTag);

        foreach (GameObject obj in objs){
            appliancesToGlow.Add(obj);
            
            
        }

        //       foreach (GameObject obj in objs){
        //         if (obj.GetComponent<Appliance>().kitchenNum == teamNumber){
        //             appliancesToGlow.Add(obj);
        //         }
            
        // }

        return appliancesToGlow;

    }

    
    public void GlowAppliance(List<GameObject> appliances){
        // teamNumber = gameObject.GetComponent<CanvasController>().teamNumber;

        // currentGlowingDishName = dishNameToGlow ;
        // string dishTag = dishInFocus.toCook;
        
        foreach (GameObject glowAppliance in appliances)
        {
            if (glowAppliance.GetComponent<OutlineEffect>()){
                glowAppliance.GetComponent<OutlineEffect>().enabled = true;
                //glowAppliance.GetComponent<OutlineEffect>().startGlowing();
            }   
           
        }
    }
    
    public GameObject IngredientSpawner;
    public string glowLocation;
   
    public void GlowLocation(DishSO dishInFocus){
    
        foreach (IngredientSO ingredient in dishInFocus.recipe)
            {   
                GameObject location = GameObject.Find(ingredient.location.ToString());

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
        if (currentGlowingDishName != ""){
            StopGlowingAll(currentGlowingDishName);
        }
    }

    public void StopGlowingAll(string dishToStopGlowing){
        Debug.Log("dishToStopGlowing: " + dishToStopGlowing);

        foreach (GameObject glowAppliance in appliancesToGlow){
           //kitchenNum = glowAppliance.GetComponent<Appliance>().kitchenNum;
                if (glowAppliance.GetComponent<OutlineEffect>()){
                        OutlineEffect outlineScript = glowAppliance.GetComponent<OutlineEffect>();
                        outlineScript.destroyClone();
                        outlineScript.enabled = false;
                       
                }
            }

        

        appliancesToGlow.Clear();
        currentGlowingDishName = "";

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
