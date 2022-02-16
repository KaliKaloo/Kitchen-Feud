using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
	public Button yourButton;
	//to change, access canvas from stove
	public GameObject canvas;
    public GameObject minigameCanvas;
	public CookingBar cookingBar;
	public Slider slider;

	void Start () {
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
		cookingBar = slider.GetComponent<CookingBar>();
	}

	void TaskOnClick(){
		Debug.Log ("You have clicked the button!");
        //SceneManager.LoadScene("kitchens_miniGame");
		slider.value = -30;
        cookingBar.keyHeld = false;
        cookingBar.done = false;

		canvas.gameObject.SetActive(true);
        minigameCanvas.gameObject.SetActive(false);
	}
}
