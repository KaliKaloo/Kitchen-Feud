using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverAnimationController : MonoBehaviour
{
    [SerializeField] private GameObject GameOverController;

    [SerializeField] private GameObject player1Model;
    [SerializeField] private GameObject player2Model;
    [SerializeField] private GameObject player3Model;

    private Animator player1ModelAnimator;
    private Animator player2ModelAnimator;
    private Animator player3ModelAnimator;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForStart());

        player1ModelAnimator = player1Model.GetComponent<Animator>();
        player2ModelAnimator = player2Model.GetComponent<Animator>();
        player2ModelAnimator = player2Model.GetComponent<Animator>();

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
