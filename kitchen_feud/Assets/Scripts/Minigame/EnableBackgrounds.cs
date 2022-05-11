using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Changes the background of minigames based on what kitchen player is in
public class EnableBackgrounds : MonoBehaviour
{
    [SerializeField] public GameObject canvas;
    [SerializeField] public GameObject team1Background;
    [SerializeField] public GameObject team2Background;

    void OnEnable()
    {
        if (canvas.tag == "Team1")
        {
            team1Background.SetActive(true);
            team2Background.SetActive(false);
        }
        else if (canvas.tag == "Team2")
        {
            team1Background.SetActive(false);
            team2Background.SetActive(true);
        }
    }
}
