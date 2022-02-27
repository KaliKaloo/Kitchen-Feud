using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Cutting : Interactable
{
    public GameObject inputObj;
    public List<IngredientSO> itemsOnTheStove = new List<IngredientSO>();
    public GameObject canvas;
    public GameObject minigameCanvas;

    public bool isBeingInteractedWith = false;
    public Rigidbody playerRigidbody;

    public override void Interact()
    {
        canvas.gameObject.SetActive(false);
        minigameCanvas.gameObject.SetActive(true);
    }

   
}