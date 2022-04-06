using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowController : MonoBehaviour
{

    public bool newClick;
    public bool firstClick = false;
    public string currentGlowingDishName;
    // public string dishName;
    public int teamNumber;
    private int kitchenNum;

    public DishSO dishInFocus;
    public GameObject[] kitchenPortals;

    void Start(){
        teamNumber = gameObject.GetComponent<CanvasController>().teamNumber;
        string doorTag;

        if (teamNumber == 1){
            doorTag = "Kitchen1Portals";
        } else {
            doorTag = "Kitchen2Portals";
        }
         kitchenPortals = GameObject.FindGameObjectsWithTag(doorTag);
    }

    public void GlowAll(string dishName){

        if(dishName == currentGlowingDishName){
            newClick = false;
        }

        if (newClick == true){
            if ((firstClick == false)){
                StopGlowingAll(currentGlowingDishName);
            }

            firstClick = false;
            dishInFocus = Database.GetDishByName(dishName);
            appliancesToGlow = getListOfAppliancesToGlow(dishName);
        
            GlowAppliance(appliancesToGlow);

            GlowLocation(dishInFocus);
            newClick = false;
        }

    }

    public List<GameObject> appliancesToGlow = new List<GameObject>();

    public List<GameObject> getListOfAppliancesToGlow(string dishNameToGlow){
        currentGlowingDishName = dishNameToGlow ;
        string dishTag = dishInFocus.toCook;
    
        GameObject[] objs = GameObject.FindGameObjectsWithTag(dishTag);
        foreach (GameObject obj in objs){
            appliancesToGlow.Add(obj);
        }
        return appliancesToGlow;
    }

    
    public void GlowAppliance(List<GameObject> appliances)
    {
     
        foreach (GameObject glowAppliance in appliances){
           
            if (glowAppliance.GetComponent<Appliance>().kitchenNum == teamNumber ){
                if (glowAppliance.GetComponent<OutlineEffect>()){
                    Debug.Log("??");
                    glowAppliance.GetComponent<OutlineEffect>().startGlowing();
                }   
            }
        }
    }
    
    private string glowLocation;
    public List<GameObject> getLocation(DishSO dish){
        List<GameObject> locations = new List<GameObject>();

        foreach (IngredientSO ingredient in dish.recipe){   
            
            foreach (GameObject room in kitchenPortals){
                if (room.name == ingredient.location.ToString()){
                   locations.Add(room);
                }
            }
        }
        return locations;
    }
   
    public void GlowLocation(DishSO dishInFocus){
        List<GameObject> locationsToGlow = getLocation(dishInFocus);

            foreach (GameObject location in locationsToGlow){
                if (location){
                    ParticleSystem PS = location.GetComponentInChildren<ParticleSystem>();
                    var main = PS.main;
                    main.startColor = new Color(243,255,28,255);
                }
            }
    }

    public void RecipeCardClose(){
        firstClick = true;
        if (currentGlowingDishName != ""){
            StopGlowingAll(currentGlowingDishName);
        }
    }

    public void StopGlowingAll(string dishToStopGlowing){

        foreach (GameObject glowAppliance in appliancesToGlow){
           
           kitchenNum = glowAppliance.GetComponent<Appliance>().kitchenNum;
           if (kitchenNum == teamNumber){
                if (glowAppliance.GetComponent<OutlineEffect>()){
                        OutlineEffect outlineScript = glowAppliance.GetComponent<OutlineEffect>();
                        outlineScript.stopGlowing();
                       
                }
            }
        }

        appliancesToGlow.Clear();
        currentGlowingDishName = "";

        List<GameObject> locationToStop = getLocation(dishInFocus);
        foreach (GameObject location in locationToStop){
            ParticleSystem PS1 = location.GetComponentInChildren<ParticleSystem>();
            Transform PSChild = PS1.gameObject.transform.GetChild(1);
            ParticleSystem PS2 = PSChild.GetComponentInChildren<ParticleSystem>();
            var main = PS1.main;
            var main2  = PS2.main;

            main.startColor = main2.startColor;
        }
            
    }
   
}
