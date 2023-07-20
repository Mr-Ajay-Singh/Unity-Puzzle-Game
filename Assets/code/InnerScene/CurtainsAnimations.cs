using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainsAnimations : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Animator animator1,animator2;
    float originalSpeed;

    private void Start()
    {

        // Store the original speed of the animation
        originalSpeed = animator1.speed;
    }

    public void PlayAnimationInReverse()
    {
        // Reverse the animation by changing the time scale and direction
        animator1.speed = -originalSpeed;
        animator1.Play("backgroundRev", -1, 1);

        animator2.speed = -originalSpeed;
        animator1.Play("background2Rev", -1, 1);
        // Replace "YourAnimationName" with the name of your animation clip
    }

    public void ResetAnimation()
    {
        // Reset the animation to its normal forward playback
        animator1.speed = originalSpeed;
        animator1.Play("backgroundRev", -1, 0);

        animator1.speed = originalSpeed;
        animator2.Play("background2Rev", -1, 0); // Replace "YourAnimationName" with the name of your animation clip
    }
}
