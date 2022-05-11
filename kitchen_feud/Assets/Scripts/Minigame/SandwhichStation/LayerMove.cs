using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

    public class LayerMove : MonoBehaviour, IPointerClickHandler
    {

        public SandwichMove SandwichMove;
    
        public void OnPointerClick(PointerEventData eventData)
        {
         
            if ((!SandwichMove.stopped) && (SandwichMove.SandwichController.checkStoppedID(SandwichMove.LayerID))){
                SandwichMove.stopped = true;
                SandwichMove.SandwichController.moving = false;
                SandwichMove.SandwichController.CountStopped++;
                SandwichMove.StopMove();
            }

        }
    }

