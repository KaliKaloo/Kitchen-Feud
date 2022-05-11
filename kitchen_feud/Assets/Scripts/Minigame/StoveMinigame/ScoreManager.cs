using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Manages the collision behaviour when ingredient collides with pot
public class ScoreManager : MonoBehaviour
{
    [SerializeField] public Text errorTextBomb;
    [SerializeField] public GameObject backbutton;
    [SerializeField] public Text score;

    public AudioSource source;
    public float pitchMin, pitchMax, volumeMin, volumeMax;
    public AudioClip[] diffSounds;


    StoveScore stoveScore = new StoveScore();
    public StoveMinigameCounter stoveMinigameCounter = new StoveMinigameCounter();

    // ingredients get counted if completely fall through top
    void OnTriggerExit2D(Collider2D target)
    {
        // only count points if game has not ended
        if (!StoveMinigameCounter.end && StoveMinigameCounter.collisionCounter < StoveMinigameCounter.amount)
        {
            stoveMinigameCounter.AddCollisionCounter();

            // add correct ingredient to counter
            if (target.tag.ToString() == "Ingredient")
            {
                Destroy(target.gameObject);

                stoveScore.AddScore();
                stoveMinigameCounter.AddCorrectIngredient();
            }

            // sounds
            gameObject.GetComponent<AudioSource>().clip = diffSounds[Random.Range(0, diffSounds.Length)];
            gameObject.GetComponent<AudioSource>().pitch = Random.Range(pitchMin, pitchMax);
            gameObject.GetComponent<AudioSource>().volume = Random.Range(volumeMin, volumeMax);
            gameObject.GetComponent<AudioSource>().Play();
        }
    }

    // continually check counters and update UI elements as well as checking if game has ended
    private void Update()
    {
        score.text = "Caught: " + StoveMinigameCounter.collisionCounter + "/" + StoveScore.maximum;

        if (StoveMinigameCounter.collisionCounter >= StoveMinigameCounter.amount)
        {
            backbutton.SetActive(true);
            // end game
            stoveMinigameCounter.EndGame();
        }
    }
}



