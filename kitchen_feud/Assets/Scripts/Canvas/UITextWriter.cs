using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

// attach to UI Text component (with the full text already there)

public class UITextWriter : MonoBehaviour
{
	// how long for each letter to get displayed
	private float typeDelay = 0.05f;
	private float waitTime;

	private TextMeshProUGUI txt;
	private string line;

	public void writeText()
	{
		txt = GetComponent<TextMeshProUGUI>();
		line = txt.text;
		txt.text = "";

		// TODO: add optional delay when to start
		StopCoroutine("PlayText");
		StartCoroutine("PlayText");
	}

	public void writeText2(string nextLine, float newWaitTime)
	{
		txt = GetComponent<TextMeshProUGUI>();
		line = txt.text;
		txt.text = "";

		waitTime = newWaitTime;
		// TODO: add optional delay when to start
		StartCoroutine("PlayText2", nextLine);
	}

	IEnumerator PlayText()
	{
		foreach (char c in line)
		{
			txt.text += c;
			yield return new WaitForSeconds(typeDelay);
		}
	}

	IEnumerator PlayText2(string nextLine)
	{
		foreach (char c in line)
		{
			txt.text += c;
			yield return new WaitForSeconds(typeDelay);
		}

		yield return new WaitForSeconds(waitTime);

		txt.text = nextLine;
		writeText();

	}

}
