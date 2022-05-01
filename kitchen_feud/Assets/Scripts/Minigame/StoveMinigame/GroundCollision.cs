using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundCollision : MonoBehaviour
{
    StoveScore stoveScore = new StoveScore();
    [SerializeField] public GameObject backbutton;

    StoveMinigameCounter stoveMinigameCounter = new StoveMinigameCounter();

    void OnTriggerEnter2D(Collider2D target)
    {
        Destroy(target.gameObject);

        if (StoveMinigameCounter.collisionCounter >= 20)
        {
            backbutton.SetActive(true);
        }
    }
}
