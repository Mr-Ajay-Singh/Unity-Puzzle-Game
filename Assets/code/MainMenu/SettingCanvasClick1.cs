using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SettingCanvasClick1 : MonoBehaviour, IPointerClickHandler
{

    [SerializeField]
    public Animator animator1;
    [SerializeField]
    public Animator animator2;

    // Start is called before the first frame update
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        PlayAnimationAndContinue();
    }

    private IEnumerator PlayAnimationCoroutine()
    {
        // Play the animation

        animator1.SetTrigger("run");
        animator2.SetTrigger("run");

        // Wait for the animation to complete
        //while (animator1.GetCurrentAnimatorStateInfo(0).IsName("SlideInAnimation2") && animator1.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        //{
        //    yield return null;
        // }
        //while (animator2.GetCurrentAnimatorStateInfo(0).IsName("SlideInAnimation") && animator2.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        //{
        //    yield return null;
        //}
        yield return new WaitForSeconds(2f);

        // Animation completed, continue with the function

        SceneManager.LoadScene(1);
    }

    public void PlayAnimationAndContinue()
    {
        StartCoroutine(PlayAnimationCoroutine());
    }

    
}
