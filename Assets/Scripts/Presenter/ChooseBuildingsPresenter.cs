using System;
using UnityEngine;

public class ChooseBuildingsPresenter : MonoBehaviour
{
    public ScalingAnimation openAnimation;
    public ScalingAnimation closeAnimation;

    private void OnEnable()
    {
        openAnimation.enabled = true;
    }

    private void Update()
    {
        if (Input.GetButton("ExitMenu"))
        {
            Exit();
        }
    }

    private void Exit()
    {
        closeAnimation.enabled = true;
    }
    
    public void CloseFinished()
    {
        gameObject.SetActive(false);
    }
}