using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
//using outlineEffect;

//public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, 
public class Hover : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    //public Database database;
    public GameObject tooltip;
    private SpriteRenderer sprite;
    private bool mouse_over = false;
    public TextMeshProUGUI dish;
    public DisplayTicket displayticket;
    public Sprite img;
    public GameObject RecipeCard;
    public Texture2D cursorClickable;


    public void OnPointerClick(PointerEventData eventData)
    {
        bool click = true;
        // Debug.Log("Mouse enter");
          RecipeCard.SetActive(true);
            
            // Debug.Log("clicked on" + dish.text);
            if (displayticket.dishes.ContainsKey(dish.text))
            {
                //hoverImage.sprite = displayticket.dishes[dish.text];
                RecipeCard.GetComponent<Image>().sprite = displayticket.dishes[dish.text];
                GlowAll(dish.text);
           
            }
            //tooltip.SetActive(true);
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
        Cursor.SetCursor(cursorClickable, Vector2.zero, CursorMode.ForceSoftware);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
    }
    public DishSO dishInFocus;
    public void GlowAll(string dishName){
       dishInFocus = Database.GetDishByName(dishName);
        GlowLocation(dishInFocus);
        GlowAppliance(dishName);
        //GlowIngredient();        
    }

    public GameObject[] appliancesToGlow;
    public GameObject glowAppliance;
    
    public void GlowAppliance(string dishName){
       
        string dishTag = dishInFocus.toCook;
        // Debug.Log(dishTag);
        // GlowLocation(dishInFocus);
        appliancesToGlow = GameObject.FindGameObjectsWithTag(dishTag);

        foreach (GameObject glowAppliance in appliancesToGlow)
        {
            //turn on the glow script
           OutlineEffect ce = glowAppliance.GetComponent<OutlineEffect>();
           if (!ce){
                ce.enabled = true;
            }
    
           
        }
    }

    public GameObject IngredientSpawner;
    public string glowLocation;
    public void GlowLocation(DishSO dishInFocus){
    
        foreach (IngredientSO ingredient in dishInFocus.recipe)
            {   
                Debug.Log(dishInFocus.recipe.Count);
                GameObject location = GameObject.Find(ingredient.location.ToString());
                Debug.Log(location.name);
                ParticleSystem PS = location.GetComponentInChildren<ParticleSystem>();
                var main = PS.main;
                public Color originalColor = main.startColor;
                main.startColor = new Color(243,255,28,255);
                
                Debug.Log(ingredient.name);
                string spawnerName = ingredient.name.ToString();
                string name = spawnerName + "Spawner";
                Debug.Log(name);
                
                foreach (Transform child in location.transform){
                    foreach (Transform childInChild in child){
                        if (childInChild.name == name){
                        
                        IngredientSpawner = child.gameObject;
                        GlowIngredient(IngredientSpawner);
                        
                        Debug.Log(IngredientSpawner.name);
                    }
                    }
                    
                }
                
            }
    }

    public void GlowIngredient(GameObject IngredientSpawnerToGlow){
        OutlineEffect ce = IngredientSpawnerToGlow.GetComponent<OutlineEffect>();
        if (!ce){
            ce.enabled = true;
        }
    }

    public void StopGlowingAll(){

    }
    
}
