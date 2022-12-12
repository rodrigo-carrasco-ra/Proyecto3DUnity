using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator myAnimator;
    int horizontal;
    int vertical;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical") ; //se referencian los float horizontal y vertical del animator
    }
    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement, bool isSprinting)
    {
        //Snapping

        float snapHorizontal;
        float snapVertical;


        #region Snap Horizontal
        if (horizontalMovement > 0 && horizontalMovement < 0.55f)
        {
            snapHorizontal = 0.5f;  //horizontal siempre sera 0.5f
        }
        else if (horizontalMovement > 0.55f)
        {
            snapHorizontal = 1f;
        }
        else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
        {
            snapHorizontal = -0.5f;
        }
        else if (horizontalMovement < -0.55f)
        {
            snapHorizontal = 1f;
        }

        else
        {
            snapHorizontal = 0;
        }
        #endregion
        #region Snap Vertical
        if (verticalMovement > 0 && verticalMovement < 0.55f)
        {
            snapVertical = 0.5f;  //horizontal siempre sera 0.5f
        }
        else if (verticalMovement > 0.55f)
        {
            snapVertical = 1f;
        }
        else if (verticalMovement < 0 && verticalMovement > -0.55f)
        {
            snapVertical = -0.5f;
        }
        else if (verticalMovement < -0.55f)
        {
            snapVertical = 1f;
        }

        else
        {
            snapVertical = 0;
        }
        #endregion

        if (isSprinting)
        {
            snapHorizontal = horizontalMovement;
            snapVertical = 2;
        }
        myAnimator.SetFloat(horizontal, snapHorizontal, 0.1f, Time.deltaTime);
        myAnimator.SetFloat(vertical, snapVertical, 0.1f, Time.deltaTime);

    }

    public void PlayTargetAnimation(string targetAnimation, bool isInteracting)
    {
        myAnimator.SetBool("isInteracting", isInteracting);
        myAnimator.CrossFade(targetAnimation,0.2f);
    }
}
