using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverAnimationController : MonoBehaviour
{
    // SCORES GO HERE
    private int team1Score = 0;
    private int team2Score = 0;


    [SerializeField] private GameObject GameOverController;

    [SerializeField] private GameObject player1Model;
    [SerializeField] private GameObject player2Model;
    [SerializeField] private GameObject player3Model;

    private Animator player1ModelAnimator;
    private Animator player2ModelAnimator;
    private Animator player3ModelAnimator;


    private gameOverMenu overMenu;


    // receives scores from score screen
    private static ParseScore endScores = new ParseScore();

    // Start is called before the first frame update
    void Start()
    {
        overMenu = GameOverController.GetComponent<gameOverMenu>();

        //get scores from score screen
        team1Score = endScores.GetScore1();
        team2Score = endScores.GetScore2();

        overMenu.CompareScore(team1Score, team2Score);

        StartCoroutine(WaitForStart());

        player1ModelAnimator = player1Model.GetComponent<Animator>();
        player2ModelAnimator = player2Model.GetComponent<Animator>();
        player3ModelAnimator = player3Model.GetComponent<Animator>();

        ResetAnimations();

    }

    private IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(6);

        GameOverController.SetActive(true);
    }

    // Update is called once per frame
    void ResetAnimations()
    {
        player1ModelAnimator.SetInteger("Dance", 0);
        player2ModelAnimator.SetInteger("Dance", 0);
        player3ModelAnimator.SetInteger("Dance", 0);
    }
}
