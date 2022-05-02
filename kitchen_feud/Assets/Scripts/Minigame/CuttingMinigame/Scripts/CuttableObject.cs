using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttableObject : MonoBehaviour
{
    public bool correctIngredient;
    public cutController CutController;
    public spawnCutBurst spawnCutBurst;
    public AudioSource source;

    public void OnCollisionEnter2D (Collision2D collision)
    {
        //identify if object is being cut by knife
        if (collision.gameObject.tag == "Cut") {
            spawnCutBurst = GameObject.Find("spawnCutBurst").transform.GetComponent<spawnCutBurst>();
            spawnCutBurst.BurstEffect(gameObject);
           
            
            Destroy(gameObject);
            CutController = GameObject.Find("CutController").transform.GetComponent<cutController>();
            //SOUND -----------------------------------
            CutController.source.clip = CutController.diffSounds[Random.Range(0, CutController.diffSounds.Length)];
            CutController.source.pitch = Random.Range(CutController.pitchMin, CutController.pitchMax);
            CutController.source.volume = Random.Range(CutController.volumeMin, CutController.volumeMax);
            CutController.source.Play();
            // ----------------------------------------
            
            CutController.Ingredient += 1;

            if (!correctIngredient)
            {
                CutController.Score -= 10;
            }
           
               
            if (CutController.Ingredient == 15)
            {
                CutController.calculateScore();
            }
            
        }
    }



}
