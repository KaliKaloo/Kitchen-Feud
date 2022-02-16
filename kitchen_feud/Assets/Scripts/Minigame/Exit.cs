using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
	public Button yourButton;
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
		canvas.gameObject.SetActive(true);
        minigameCanvas.gameObject.SetActive(false);
	}
}
