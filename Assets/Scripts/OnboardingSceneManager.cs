using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OnboardingSceneManager : MonoBehaviour
{
    //public GameObject onboardingCanvas;
    public TMP_Text onboardingCanvasText;
    public string stringForOnboardingCanvasText;
    public Animator leftThumbstickMoveAni, rightThumbstickTurnAni, selectBoneAni, returnBoneToOriginAni, deselectBoneAni, attachBoneAni, answerQuizAni;
    public Animator textAni, fadeOutCanvas;
    public bool complete;


    public GameObject[] completionIndicators;
    public int completionIndicatorCounter;

    public int completeCounter = 0;


    private void Start()
    {
        InitialSetup();
        completionIndicatorCounter = 0;
    }

    public void InitialSetup()
    {
        onboardingCanvasText.text = "Use the Left Thumbstick to move - you can move through the table and skeleton";
    }

    public void MoveLeftThumbstick()
    {
        if(completeCounter == 0)
        {
            leftThumbstickMoveAni.Play("Complete");
            stringForOnboardingCanvasText = "Use the Right Thumbstick to turn";
            StartCoroutine(UpdateOnboardingCanvas());
        }
    }

    public void MoveRightThumbstick()
    {
        if (completeCounter == 1)
        {
            rightThumbstickTurnAni.Play("Complete");
            stringForOnboardingCanvasText = "Select an object by pointing and pressing a Trigger button";
            StartCoroutine(UpdateOnboardingCanvas());
        }
    }

    public void SelectObject()
    {
        if(completeCounter == 2)
        {
            selectBoneAni.Play("Complete");
            stringForOnboardingCanvasText = "When an object is selected, press'A' or 'Y' to deselect it";
            StartCoroutine(UpdateOnboardingCanvas());

        }
    }

    public void DeselectObject()
    {
        if (completeCounter == 3)
        {
            deselectBoneAni.Play("Complete");
            stringForOnboardingCanvasText = "When an object is selected, Press 'B' or 'Z' to put it back on the wall";
            StartCoroutine(UpdateOnboardingCanvas());

        }
    }

    public void ReturnToOrigin()
    {
        if(completeCounter == 4)
        {
            returnBoneToOriginAni.Play("Complete");
            stringForOnboardingCanvasText = "Find the bone highlighted in blue on the skeleton and place it in position";
            StartCoroutine(UpdateOnboardingCanvas());
        }
    }

    public void AttachToSkeleton()
    {
        if(completeCounter == 5)
        {
            attachBoneAni.Play("Complete");
            stringForOnboardingCanvasText = "Answer the quiz by pointing to answer and pressing a Trigger button";
            StartCoroutine(UpdateOnboardingCanvas());
        }
    }

    public void AnswerQuiz()
    {
        if(completeCounter == 6)
        {
            answerQuizAni.Play("Complete");
            complete = true;
            StartCoroutine(UpdateOnboardingCanvas());

        }
    }

    public IEnumerator UpdateOnboardingCanvas()
    {
        if (complete)
        {
            yield return new WaitForSeconds(0.5f);
            fadeOutCanvas.Play("fadeOutCanvas");
        }
        else
        {
            completeCounter++;
            yield return new WaitForSeconds(0.75f);
            textAni.Play("fadeOutText");
            yield return new WaitForSeconds(0.15f);

            onboardingCanvasText.text = stringForOnboardingCanvasText;
            completionIndicators[completionIndicatorCounter].SetActive(false);
            completionIndicatorCounter++;
            completionIndicators[completionIndicatorCounter].SetActive(true);
            textAni.Play("fadeInText");

            yield return new WaitForSeconds(0.5f);
        }

    }
}
