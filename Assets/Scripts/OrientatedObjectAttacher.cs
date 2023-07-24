using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientatedObjectAttacher : MonoBehaviour
{
    [Header("Scripts")]
    public CustomPointer rightCustomPointer;
    public CustomPointer leftCustomPointer;
    public GameObject rightHand;
    public GameObject leftHand;
    public BoneNameQuiz boneNameQuiz;
    //public OnboardingManager onboardingManager;
    public OnboardingSceneManager onboardingSceneManager;
    public GameObject onboardingHolder;
    public MuscleLearningScenarioSetup muscleLearningScenarioSetup;


    [Header("Scene Identifier")]
    public bool onboardingScene;
    public bool skeletalScene;
    public bool muscleLearningScene;
    public bool muscleTestingScene;

    [Header("This Game Object")]
    public GameObject thisGameObject;
    public string thisGameObjectName;
    public bool startOfSequence, endOfSequence;

    [Header("Skeletal Scene")]
    public GameObject skeletonAttachObject;
    public GameObject skeletonReplaceObject;
    public GameObject skeletonInvalidObject;
    public GameObject[] nextInSequenceSkeletonTurnOn;
    public GameObject[] nextInSequenceSkeletonTurnOff;

    [Header("Audio Feedback")]
    public GameObject audiosourceHolder;
    public AudioSource audioSource;

    [Header("Collider Elements")]
    public MaleToFemaleColliderChecker[] maleToFemaleColliderCheckers;
    public int currentEnteredCollidersInt;
    public int totalNumberOfColliders;





    private void Awake()
    {
        rightHand = GameObject.FindWithTag("PlayerRightHand");
        leftHand = GameObject.FindWithTag("PlayerLeftHand");
        rightCustomPointer = rightHand.GetComponent<CustomPointer>();
        leftCustomPointer = leftHand.GetComponent<CustomPointer>();


        thisGameObject = transform.gameObject;
        thisGameObjectName = transform.name;

        if (onboardingScene)
        {
            onboardingHolder = GameObject.Find("---ONBOARDING ---");
            //onboardingManager = onboardingHolder.GetComponent<OnboardingManager>();
            onboardingSceneManager = onboardingHolder.GetComponent<OnboardingSceneManager>();
        }

        if (skeletalScene)
        {
            boneNameQuiz = FindObjectOfType<BoneNameQuiz>();
            skeletonAttachObject = GameObject.Find(thisGameObjectName + " Attach");
            skeletonReplaceObject = GameObject.Find(thisGameObjectName + " Replace");
            skeletonInvalidObject = GameObject.Find(thisGameObjectName + " Invalid");

            skeletonReplaceObject.SetActive(false);
        }

        if (muscleLearningScene)
        {
            muscleLearningScenarioSetup = GameObject.FindGameObjectWithTag("Scriptholder").GetComponent<MuscleLearningScenarioSetup>();
        }


        audiosourceHolder = GameObject.FindGameObjectWithTag("AttachSFX");
        audioSource = audiosourceHolder.GetComponent<AudioSource>();

        totalNumberOfColliders = maleToFemaleColliderCheckers.Length;
    }

    public void Start()
    {
        if (skeletalScene)
        {
            if (startOfSequence)
            {
                skeletonAttachObject.SetActive(true);
                skeletonReplaceObject.SetActive(false);
                skeletonInvalidObject.SetActive(false);
            }
            else
            {
                skeletonAttachObject.SetActive(false);
                skeletonReplaceObject.SetActive(false);
                skeletonInvalidObject.SetActive(true);
            }
        }
    }

    public void CheckColliders()
    {
        //Debug.LogError("Collider check started");

        if (currentEnteredCollidersInt == maleToFemaleColliderCheckers.Length)
        {
            ConnectMaleToFemale();
            Debug.LogError(thisGameObjectName + " all colliders detected");
        }
    }


    public void ConnectMaleToFemale()
    {
        rightCustomPointer.holdingObject = false;
        rightCustomPointer.linePointerOn = true;

        leftCustomPointer.holdingObject = false;
        leftCustomPointer.linePointerOn = true;

        thisGameObject.SetActive(false);

        audioSource.Play();

        if (onboardingScene)
        {
            skeletonAttachObject.SetActive(false);
            skeletonReplaceObject.SetActive(true);
            boneNameQuiz.lastBoneConnected = thisGameObjectName;
            boneNameQuiz.GenerateQuiz();
            onboardingSceneManager.AttachToSkeleton();
            //onboardingManager.attachBone = true;
            //onboardingManager.UpdateChecklist();
            if (!endOfSequence)
            {
                for (int i = 0; i < nextInSequenceSkeletonTurnOff.Length; i++)
                {
                    nextInSequenceSkeletonTurnOff[i].SetActive(false);
                }

                for (int i = 0; i < nextInSequenceSkeletonTurnOn.Length; i++)
                {
                    nextInSequenceSkeletonTurnOn[i].SetActive(true);
                }
            }
        }

        if (skeletalScene)
        {
            skeletonAttachObject.SetActive(false);
            skeletonReplaceObject.SetActive(true);
            boneNameQuiz.lastBoneConnected = thisGameObjectName;
            boneNameQuiz.GenerateQuiz();
            if (!endOfSequence)
            {
                for (int i = 0; i < nextInSequenceSkeletonTurnOff.Length; i++)
                {
                    nextInSequenceSkeletonTurnOff[i].SetActive(false);
                }

                for (int i = 0; i < nextInSequenceSkeletonTurnOn.Length; i++)
                {
                    nextInSequenceSkeletonTurnOn[i].SetActive(true);
                }
            }
        }

        if (muscleLearningScene)
        {
            muscleLearningScenarioSetup.LearningMuscleCount();
        }
    }
}