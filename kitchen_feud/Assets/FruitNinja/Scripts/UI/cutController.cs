using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cutController : MonoBehaviour
{
    public Text scoretext;
    public Text numtext;
   
    private int score = 0;
    public int Score{
        get { return score;  } 
        set {
            score = value;
            scoretext.text = "Score: " + score;
        }
    }

    private int num = 0;
    public int Ingredient {
        get { return num; }
        set
        {
            num = value;
            numtext.text = num + "/15";
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Score = 100;
        Ingredient = 0;
    }

    void Update()
    {
        if (Ingredient == 15)
        {
            Debug.Log("Game Over");
        }
    }

}
