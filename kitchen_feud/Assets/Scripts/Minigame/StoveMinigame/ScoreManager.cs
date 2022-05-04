using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        // if game has not ended count the points
        if (!StoveMinigameCounter.end && StoveMinigameCounter.collisionCounter < 20)
        {
            stoveMinigameCounter.AddCollisionCounter();

            if (target.tag.ToString() == "Ingredient")
            {
                Destroy(target.gameObject);

                stoveScore.AddScore();
                stoveMinigameCounter.AddCorrectIngredient();

            }
            gameObject.GetComponent<AudioSource>().clip = diffSounds[Random.Range(0, diffSounds.Length)];
            gameObject.GetComponent<AudioSource>().pitch = Random.Range(pitchMin, pitchMax);
            gameObject.GetComponent<AudioSource>().volume = Random.Range(volumeMin, volumeMax);
            gameObject.GetComponent<AudioSource>().Play();
        }
    }

    private void Update()
    {
        score.text = "Caught: " + StoveMinigameCounter.collisionCounter + "/" + StoveScore.maximum;

        if (StoveMinigameCounter.collisionCounter >= 20)
        {
            backbutton.SetActive(true);

            // end game as failsafe
            stoveMinigameCounter.EndGame();
        }
    }
}



