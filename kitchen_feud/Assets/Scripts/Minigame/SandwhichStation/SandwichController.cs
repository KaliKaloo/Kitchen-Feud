using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SandwichController : MonoBehaviour
{
    public DishSO dish;
    // Start is called before the first frame update
    public GameObject StartButton;
    public GameObject GameUI;
    public Image Ingredient;
    [SerializeField] private Text score;
    //private 
    
    void Start()
    {
        //set bacground to correct team

    }

    public void Update()
    {
        //if click and box collider intersect, get gameobject of box collider and call the StoppedOnClick method for that component
    }

    public void StartGame()
    {
        StartButton.SetActive(false);
        GameUI.SetActive(true);
        DisplayRandomIngredient();
        
        //StartSpawning();
        //ingredients spawn in the middle and start moving left and rigt
        //images of ingredients start appeairng in intervals on the left
        //player has to click on the correct object corresponding to the image 
        //it has to stop in the center.
    }

    public void DisplayRandomIngredient()
    {
        Ingredient.sprite = dish.recipe[0].img;
    }

    

    
}
