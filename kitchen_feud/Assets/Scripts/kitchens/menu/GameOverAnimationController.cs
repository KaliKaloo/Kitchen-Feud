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

    [SerializeField] private GameObject CatModel1;
    [SerializeField] private GameObject CatModel2;
    [SerializeField] private GameObject CatModel3;

    [SerializeField] private GameObject PandaModel1;
    [SerializeField] private GameObject PandaModel2;
    [SerializeField] private GameObject PandaModel3;

    private Animator CatModelAnimator1;
    private Animator CatModelAnimator2;
    private Animator CatModelAnimator3;

    private Animator PandaModelAnimator1;
    private Animator PandaModelAnimator2;
    private Animator PandaModelAnimator3;


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

        CatModelAnimator1 = CatModel1.GetComponent<Animator>();
        CatModelAnimator2 = CatModel2.GetComponent<Animator>();
        CatModelAnimator3 = CatModel3.GetComponent<Animator>();

        PandaModelAnimator1 = PandaModel1.GetComponent<Animator>();
        PandaModelAnimator2 = PandaModel2.GetComponent<Animator>();
        PandaModelAnimator3 = PandaModel3.GetComponent<Animator>();

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
        CatModelAnimator1.SetInteger("Dance", 0);
        CatModelAnimator2.SetInteger("Dance", 0);
        CatModelAnimator3.SetInteger("Dance", 0);

        PandaModelAnimator1.SetInteger("Dance", 0);
        PandaModelAnimator2.SetInteger("Dance", 0);
        PandaModelAnimator3.SetInteger("Dance", 0);

    }
}
