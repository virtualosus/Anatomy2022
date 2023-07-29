using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;


public class CustomPointer : MonoBehaviour
{
    [Header ("Hand And Controllers Option")]
    public bool leftHand;
    public XRController xrController;
    public float thumbstickDeadzone = 0.2f;
    private bool thumbstickUsed = false;
    public bool onboardingScene;

    [Header ("Scripts")]
    public SceneAndScoreManager sceneAndScoreManager;
    public OnboardingSceneManager onboardingSceneManager;
    public OVRScreenFade ovrScreenFade;
    public BoneNameQuiz boneNameQuiz;
    public XRInteractorLineVisual xRInteractorLineVisual;

    [Header ("Pointer Specific")]
    public LineRenderer lineRenderer;
    public float flexibleLineLength;
    public bool linePointerOn;
    public bool objectHit = false;
    public bool holdingObject;
    public GameObject pointObject;
    public GameObject currenHighlightedObject;
    public string currentHighlightedObjectName;
    public Vector3 endPosition;

    [Header("Buttons, Quiz and Timer")]
    public QuizButton[] quizButton;
    public LanguageButton[] languageButton;        
    public float countdownTimer;
    public bool coroutineRunning;

    [Header ("Events")]
    public UnityEvent handSelect;
    public UnityEvent handMoveTowards;
    public UnityEvent handDeselect;
    public UnityEvent handReturnOrigin;




    // Start is called before the first frame update
    void Start()
    {
        sceneAndScoreManager = GameObject.FindGameObjectWithTag("SceneAndScoreManager").GetComponent<SceneAndScoreManager>();
        countdownTimer = 3f;
        Vector3[] startLinePositions = new Vector3[2] { Vector3.zero, Vector3.zero };
        linePointerOn = true;
        coroutineRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool isButtonPressed;

        if(GetButtonValue(InputHelpers.Button.PrimaryButton, out isButtonPressed) && isButtonPressed)
        {
            handDeselect.Invoke();
        }

        if(GetButtonValue(InputHelpers.Button.SecondaryButton, out isButtonPressed) && isButtonPressed)
        {
            handReturnOrigin.Invoke();
            handDeselect.Invoke();
        }

        if(leftHand && GetButtonValue(InputHelpers.Button.MenuButton, out isButtonPressed) && isButtonPressed)
        {
            countdownTimer -= Time.deltaTime;
            if (countdownTimer < 0 && !coroutineRunning)
            {
                StartCoroutine(RestartApp());                
            }
        }
        else
        {
            countdownTimer = 3f;
        }

        if (onboardingScene)
        {
            HandleThumbstickMovement();
        }

        if (linePointerOn)
        {
            xRInteractorLineVisual.enabled = true;
            ActiveLineRenderer(transform.position, transform.forward, flexibleLineLength);
        }
        else
        {
            xRInteractorLineVisual.enabled = false;
        }
    }

    private void HandleThumbstickMovement()
    {
        Vector2 thumbstickValue;
        if (xrController.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out thumbstickValue))
        {
            if (leftHand)
            {
                if (thumbstickValue.magnitude > thumbstickDeadzone)
                {
                    if (!thumbstickUsed)
                    {
                        onboardingSceneManager.MoveLeftThumbstick();
                    }
                }
            }
            else
            {
                if (thumbstickValue.magnitude > thumbstickDeadzone)
                {
                    if (!thumbstickUsed)
                    {
                        onboardingSceneManager.MoveRightThumbstick();
                    }
                }
            }            
        }
    }

    private bool GetButtonValue(InputHelpers.Button button, out bool value)
    {
        switch (button)
        {
            case InputHelpers.Button.Trigger:
                return xrController.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out value);
            case InputHelpers.Button.PrimaryButton:
                return xrController.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out value);
            case InputHelpers.Button.GripButton:
                return xrController.inputDevice.TryGetFeatureValue(CommonUsages.gripButton, out value);
            case InputHelpers.Button.SecondaryButton:
                return xrController.inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out value);
            case InputHelpers.Button.MenuButton:
                return xrController.inputDevice.TryGetFeatureValue(CommonUsages.menuButton, out value);
        }

        value = false;
        return false;
    }

    public void ActiveLineRenderer(Vector3 targetPosition, Vector3 direction, float length)
    {
        bool triggerButtonPressed;

        RaycastHit hit;

        Ray lineRendererOut = new Ray(targetPosition, direction);

        endPosition = targetPosition + (length * direction);

        if (Physics.Raycast(lineRendererOut, out hit))
        {
            endPosition = hit.point;

            pointObject = hit.collider.gameObject;

            if (pointObject.GetComponent<SelectedObject>())
            {
                if (!boneNameQuiz.quizAvailable)
                {
                    currentHighlightedObjectName = pointObject.GetComponent<SelectedObject>().thisGameObjectName;
                    currenHighlightedObject = GameObject.Find(currentHighlightedObjectName);

                    if (leftHand)
                    {
                       if (GetButtonValue(InputHelpers.Button.Trigger, out triggerButtonPressed) && triggerButtonPressed)
                       {
                            if (!holdingObject)
                            {
                                currenHighlightedObject.GetComponent<SelectedObject>().leftHandSelect = true;
                                currenHighlightedObject.GetComponent<SelectedObject>().ActivateSelect();
                                holdingObject = true;
                                handMoveTowards.Invoke();
                            }
                       }
                    }
                    else
                    {
                        if (GetButtonValue(InputHelpers.Button.Trigger, out triggerButtonPressed) && triggerButtonPressed)
                        {
                            if (!holdingObject)
                            {
                                currenHighlightedObject.GetComponent<SelectedObject>().rightHandSelect = true;
                                currenHighlightedObject.GetComponent<SelectedObject>().ActivateSelect();
                                holdingObject = true;
                                handMoveTowards.Invoke();
                            }
                        }
                    }
                }
            }

            else if (pointObject.GetComponent<QuizButton>())
            {
                currentHighlightedObjectName = pointObject.GetComponent<QuizButton>().thisGameObjectName;
                currenHighlightedObject = GameObject.Find(currentHighlightedObjectName);

                for (int i = 0; i < quizButton.Length; i++)
                {
                    if (leftHand)
                    {
                        if (quizButton[i].name == currentHighlightedObjectName)
                        {
                            quizButton[i].leftHandSelect = true;
                        }
                    }
                    else
                    {
                        if (quizButton[i].name == currentHighlightedObjectName)
                        {
                            quizButton[i].rightHandSelect = true;
                        }
                    }
                }

                if (GetButtonValue(InputHelpers.Button.Trigger, out triggerButtonPressed) && triggerButtonPressed)
                {
                    pointObject.GetComponent<QuizButton>().ButtonSelect();
                }

            }
        }
    }

    public IEnumerator RestartApp()
    {
        coroutineRunning = true;
        ovrScreenFade.FadeOut();
        sceneAndScoreManager.ResetMasterScores();
        yield return new WaitForSeconds(2f);    
        SceneManager.LoadScene(0);
    }
}
