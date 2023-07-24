using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;


public class CustomPointer : MonoBehaviour
{
    [Header ("Hand Option")]
    public bool leftHand;

    [Header ("Scripts")]
    public SceneAndScoreManager sceneAndScoreManager;
    //public OnboardingManager onboardingManager;
    public OnboardingSceneManager onboardingSceneManager;
    public bool onboardingScene;
    public OVRScreenFade ovrScreenFade;
    public BoneNameQuiz boneNameQuiz;
    public XRInteractorLineVisual xRInteractorLineVisual;

    [Header ("Pointer Specific")]
    public LineRenderer lineRenderer;
    private float flexibleLineLength;
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
        if (OVRInput.Get(OVRInput.Button.One))
        {
            handDeselect.Invoke();

        }

        if (OVRInput.Get(OVRInput.Button.Two))
        {
            handReturnOrigin.Invoke();
            handDeselect.Invoke();
        }

        if (OVRInput.Get(OVRInput.Button.Three))
        {
            handDeselect.Invoke();

        }

        if (OVRInput.Get(OVRInput.Button.Four))
        {
            handReturnOrigin.Invoke();
            handDeselect.Invoke();
        }


        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp))
        {
            if (onboardingScene)
            {
                onboardingSceneManager.MoveLeftThumbstick();
            }

            //if (!onboardingManager.leftThumbstickMove)
            //{
            //    onboardingManager.leftThumbstickMove = true;
            //    onboardingManager.UpdateChecklist();
            //}

        }

        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown))
        {
            if (onboardingScene)
            {
                onboardingSceneManager.MoveLeftThumbstick();
            }

            //if (!onboardingManager.leftThumbstickMove)
            //{
            //    onboardingManager.leftThumbstickMove = true;
            //    onboardingManager.UpdateChecklist();
            //}

        }

        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft))
        {
            if (onboardingScene)
            {
                onboardingSceneManager.MoveLeftThumbstick();
            }

            //if (!onboardingManager.leftThumbstickMove)
            //{
            //    onboardingManager.leftThumbstickMove = true;
            //    onboardingManager.UpdateChecklist();
            //}
        }

        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight))
        {
            if (onboardingScene)
            {
                onboardingSceneManager.MoveLeftThumbstick();
            }

            //if (!onboardingManager.leftThumbstickMove)
            //{
            //    onboardingManager.leftThumbstickMove = true;
            //    onboardingManager.UpdateChecklist();
            //}
        }

        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickLeft))
        {
            if (onboardingScene)
            {
                onboardingSceneManager.MoveRightThumbstick();
            }

            //if (!onboardingManager.rightThumbstickTurn)
            //{
            //    onboardingManager.rightThumbstickTurn = true;
            //    onboardingManager.UpdateChecklist();
            //}

        }

        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickRight))             
        {
            if (onboardingScene)
            {
                onboardingSceneManager.MoveRightThumbstick();
            }

            //if (!onboardingManager.rightThumbstickTurn)
            //{
            //    onboardingManager.rightThumbstickTurn = true;
            //    onboardingManager.UpdateChecklist();
            //}
        }

        if (OVRInput.Get(OVRInput.Button.Start))
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

        if (linePointerOn)
        {
            xRInteractorLineVisual.enabled = true;

            //lineRenderer.enabled = true;
            ActiveLineRenderer(transform.position, transform.forward, flexibleLineLength);
        }
        else
        {
            xRInteractorLineVisual.enabled = false;
            //lineRenderer.enabled = false;
        }
    }

 

    public void ActiveLineRenderer(Vector3 targetPosition, Vector3 direction, float length)
    {
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
                        //if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
                        //{
                        //    if (!holdingObject)
                        //    {
                        //        currenHighlightedObject.GetComponent<SelectedObject>().leftHandSelect = true;
                        //        currenHighlightedObject.GetComponent<SelectedObject>().ActivateSelect();
                        //        holdingObject = true;
                        //        handMoveTowards.Invoke();
                        //    }
                        //}
                        //else if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
                        //{
                        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
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
                        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
                        {
                            if (!holdingObject)
                            {
                                currenHighlightedObject.GetComponent<SelectedObject>().rightHandSelect = true;
                                currenHighlightedObject.GetComponent<SelectedObject>().ActivateSelect();
                                holdingObject = true;
                                handMoveTowards.Invoke();
                            }
                        }
                        //else if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
                        //{
                        //    if (!holdingObject)
                        //    {
                        //        currenHighlightedObject.GetComponent<SelectedObject>().rightHandSelect = true;
                        //        currenHighlightedObject.GetComponent<SelectedObject>().ActivateSelect();
                        //        holdingObject = true;
                        //        handMoveTowards.Invoke();
                        //    }
                        //}
                    }
                }
            }

            else if (pointObject.GetComponent<QuizButton>())
            {
                currentHighlightedObjectName = pointObject.GetComponent<QuizButton>().thisGameObjectName;
                currenHighlightedObject = GameObject.Find(currentHighlightedObjectName);
                //lineRenderer.material = highlighted;

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
                if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
                {
                    pointObject.GetComponent<QuizButton>().ButtonSelect();
                }

                if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
                {
                    pointObject.GetComponent<QuizButton>().ButtonSelect();
                }
            }


            //else if (pointObject.GetComponent<LanguageButton>())
            //{
            //    currentHighlightedObjectName = pointObject.GetComponent<LanguageButton>().thisGameObjectName;
            //    currenHighlightedObject = GameObject.Find(currentHighlightedObjectName);
            //    for (int i = 0; i < languageButton.Length; i++)
            //    {
            //        if (languageButton[i].name == currentHighlightedObjectName)
            //        {
            //            languageButton[i].rightHandRay = true;
            //        }
            //    }

            //    if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
            //    {
            //        pointObject.GetComponent<LanguageButton>().ButtonSelect();
            //    }
            //}

            else if (!pointObject.GetComponent<QuizButton>() && !pointObject.GetComponent<SelectedObject>() && !pointObject.GetComponent<LanguageButton>())
            {
                for (int i = 0; i < quizButton.Length; i++)
                {
                    quizButton[i].rightHandSelect = false;
                    quizButton[i].leftHandSelect = false;

                }

                currentHighlightedObjectName = null;
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
