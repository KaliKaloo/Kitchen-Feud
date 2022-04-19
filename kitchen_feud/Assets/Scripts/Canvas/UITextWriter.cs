using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// attach to UI Text component (with the full text already there)

public class UITextWriter : MonoBehaviour
{
	// how long for each letter to get displayed
	private float typeDelay = 0.05f;

	private Text txt;
	private string line;

	void Awake()
	{
		txt = GetComponent<Text>();
		line = txt.text;
		txt.text = "";

		// TODO: add optional delay when to start
		StartCoroutine("PlayText");
	}

	IEnumerator PlayText()
	{
		foreach (char c in line)
		{
			txt.text += c;
			yield return new WaitForSeconds(typeDelay);
		}
	}

}
