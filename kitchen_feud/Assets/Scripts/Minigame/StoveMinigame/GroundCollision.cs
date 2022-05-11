using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// controls behaviour when item collides with ground on stove minigame
public class GroundCollision : MonoBehaviour
{
    [SerializeField] public GameObject backbutton;


    StoveMinigameCounter stoveMinigameCounter = new StoveMinigameCounter();

    void OnTriggerEnter2D(Collider2D target)
    {
        // destroy ingredient after touching ground
        Destroy(target.gameObject);

        // if game collision counter more than max amount then show back button
        if (StoveMinigameCounter.collisionCounter >= StoveMinigameCounter.amount)
        {
            backbutton.SetActive(true);
        }
    }
}
