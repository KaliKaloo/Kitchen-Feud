using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ParseScore
{
    private static int score1 = 0;
    private static int score2 = 0;

    public void UpdateScores(int newScore1, int newScore2)
    {
        score1 = newScore1;
        score2 = newScore2;
    }

    public int GetScore1()
    {
        return score1;
    }

    public int GetScore2()
    {
        return score2;
    }
}

public class FoodModifier
{
    int currentScore = 0;

    // default base score = 100;
    int baseScore = 100;
    int ingredients = 0;

    public FoodModifier(int passedBaseScore)
    {
        baseScore = passedBaseScore;
    }

    public int GetBaseScore()
    {
        return 1000;
    }

    // adds base ingredient
    public void AddIngredientToDish()
    {
        if (ingredients <= 2) {
            ingredients += 1;
            currentScore += AlterScore((int)(baseScore * 0.05));
        }
    }

    public void OvercookDish()
    {
        currentScore = AlterScore((int)(baseScore * 0.9));
    }

    public void UndercookDish()
    {
        currentScore = AlterScore((int)(baseScore * 0.8));
    }

    public void CookDishProperly()
    {
        currentScore = AlterScore(baseScore);
    }

    // based on how many ingredients there are modifys score
    private int AlterScore(int score)
    {
        if (ingredients == 0)
        {
            return 0;
        } else if (ingredients == 1)
        {
            return (int)(score * 0.5);
        } else
        {
            return score;
        }
    }

    public int GetCurrentScore()
    {
        return AlterScore(currentScore);
    }

    public void ResetScore()
    {
        currentScore = 0;
    }

}

public class scoreController : MonoBehaviour
{
    [SerializeField] private Text score1Text;
    [SerializeField] private Text score2Text;

    [SerializeField] private Text foodRating1;
    [SerializeField] private Text foodRating2;

    [SerializeField] private Text timerText;

    // How long the game lasts in seconds
    private int timer = 10;

    private int score1 = 0;
    private int score2 = 0;
    float elapsed = 0f;
    
    // updates end scores to compare in game over scene
    private static ParseScore endScores = new ParseScore();

    private FoodModifier mushroomSoup1 = new FoodModifier(1000);
    private FoodModifier mushroomSoup2 = new FoodModifier(1000);

    // Start is called before the first frame update
    void Start()
    {
        // start scores at 0
        score1Text.text = ConvertScoreToString(score1);
        score2Text.text = ConvertScoreToString(score2);
        timerText.text = ConvertSecondToMinutes(timer);
    }

    // Converts an integer to a string with proper comma notation
    private string ConvertScoreToString(int score)
    {
        return String.Format("{0:n0}", score);
    }

    private string ConvertSecondToMinutes(int seconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        string str = time.ToString(@"mm\:ss");
        return str;
    }

    public void AddToDish1()
    {
        mushroomSoup1.AddIngredientToDish();
    }

    public void CookDish1()
    {
        mushroomSoup1.CookDishProperly();
    }

    public void OvercookDish1()
    {
        mushroomSoup1.OvercookDish();
    }

    public void UndercookDish1()
    {
        mushroomSoup1.UndercookDish();
    }

    public void AddDishToScore1()
    {
        score1 += mushroomSoup1.GetCurrentScore();
    }

    public void AddToDish2()
    {
        mushroomSoup2.AddIngredientToDish();
    }

    public void CookDish2()
    {
        mushroomSoup2.CookDishProperly();
    }

    public void OvercookDish2()
    {
        mushroomSoup2.OvercookDish();
    }

    public void UndercookDish2()
    {
        mushroomSoup2.UndercookDish();
    }

    public void AddDishToScore2()
    {
        score2 += mushroomSoup2.GetCurrentScore();
    }

    public void Add100ToScore1()
    {
            score1 += 100;
    }

    public void Add100ToScore2()
    {
        score2 += 100;
    }

    // Update is called once per frame
    void Update()
    {
        // update scores every frame
        score1Text.text = ConvertScoreToString(score1);
        score2Text.text = ConvertScoreToString(score2);

        //foodRating1.text = mushroomSoup1.GetCurrentScore().ToString();
        //foodRating2.text = mushroomSoup2.GetCurrentScore().ToString();

        // increment every second
        elapsed += Time.deltaTime;
        if (elapsed >= 1f)
        {
            elapsed = elapsed % 1f;
            OutputTime();
        }
    }

    // OutputTime is called once per second
    void OutputTime()
    {
        if (timer != 0)
        {
            // updates timer and text in timer
            timer = timer - 1;
            timerText.text = ConvertSecondToMinutes(timer);
        }
        else
        {
            // load game over screen and send final scores
            endScores.UpdateScores(score1, score2);
            SceneManager.LoadScene("gameOver");
        }
    }
}
