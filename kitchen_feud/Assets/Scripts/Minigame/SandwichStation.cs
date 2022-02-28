using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SandwichStation: Interactable
{
    public GameObject canvas;
    public GameObject minigameCanvas;

    public override void Interact()
    {
        canvas.gameObject.SetActive(false);
        minigameCanvas.gameObject.SetActive(true);
    }


}
