using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialText : MonoBehaviour
{

    [SerializeField] private GameObject tutorialText;
    private TextMeshProUGUI textBar;
    private UITextWriter textWriter;

    // Start is called before the first frame update
    void Start()
    {
        textBar = tutorialText.GetComponent<TextMeshProUGUI>();
        textWriter = tutorialText.GetComponent<UITextWriter>();
    }

    void ChangeText(string instruction)
    {
        textBar.text = instruction;
        // start text writer
    }

}
