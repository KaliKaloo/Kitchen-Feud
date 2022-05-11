using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// controls the animations for the models in game over screen
public class GameOverAnimationController : MonoBehaviour
{
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

        //get scores and compare them
        overMenu.CompareScore(endScores.GetScore1(), endScores.GetScore2());

        // wait for canvas animations to finish
        StartCoroutine(WaitForStart());

        // assign animators attached to the models
        CatModelAnimator1 = CatModel1.GetComponent<Animator>();
        CatModelAnimator2 = CatModel2.GetComponent<Animator>();
        CatModelAnimator3 = CatModel3.GetComponent<Animator>();

        PandaModelAnimator1 = PandaModel1.GetComponent<Animator>();
        PandaModelAnimator2 = PandaModel2.GetComponent<Animator>();
        PandaModelAnimator3 = PandaModel3.GetComponent<Animator>();

        ResetAnimations();
    }

    // enables game over controller script after 6 seconds
    private IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(6);

        GameOverController.SetActive(true);
    }

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
