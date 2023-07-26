using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PositionAndRotationCompare : MonoBehaviour
{
    [Header("Scripts")]
    public CustomPointer rightHandPointer;
    public GameObject rightHand;
    public MuscleTestingScenarioSetup muscleTestingScenarioSetup;

    [Header("This Game Object")]
    public GameObject thisGameObject;
    public string thisGameObjectName;
    public GameObject refAttachCube;

    [Header("Muscle Attach Object")]
    public string attachObjectName;
    public GameObject attachObject;

    [Header("Position And Rotation Compare Logic")]
    //public Vector3 thisGameObjectRotation;
    //public Vector3 thisGameObjectPosition;

    private Vector3 attachObjectPosition;
    private Vector3 attachObjectRotation;

    private Vector3 refAttachCubePosition;
    private Vector3 refAttachCubeRotation;

    private float roundedAttachObjectPosX, roundedAttachObjectPosY, roundedAttachObjectPosZ;
    private float roundedRefAttachCubePosX, roundedRefAttachCubePosY, roundedRefAttachCubePosZ;

    private float roundedAttachObjectRotX, roundedAttachObjectRotY, roundedAttachObjectRotZ;
    private float roundedRefAttachCubeRotX, roundedRefAttachCubeRotY, roundedRefAttachCubeRotZ;

    public Vector3 roundedAttachObjectPosition;
    public Vector3 roundedAttachObjectRotation;
    
    public Vector3 roundedRefAttachCubePosition;
    public Vector3 roundedRefAttachCubeRotation;


    [Header("Audio Feedback")]
    public GameObject audiosourceHolder;
    public AudioSource audioSource;



    private void Awake()
    {
        rightHand = GameObject.FindWithTag("PlayerRightHand");
        rightHandPointer = rightHand.GetComponent<CustomPointer>();
        muscleTestingScenarioSetup = GameObject.FindGameObjectWithTag("Scriptholder").GetComponent<MuscleTestingScenarioSetup>();

        thisGameObject = transform.gameObject;
        thisGameObjectName = transform.name;
        refAttachCube = GameObject.Find(thisGameObjectName + " RefAttachCube");

        attachObjectName = thisGameObjectName + " Ref";
        attachObject = GameObject.Find(attachObjectName);

        audiosourceHolder = GameObject.FindGameObjectWithTag("AttachSFX");
        audioSource = audiosourceHolder.GetComponent<AudioSource>();

        GetDestinationObjectValues();

        StartCoroutine(SetParent());

    }

    private void Update()
    {
        GetRefAttachObjectValues();
        //ComparePositionAndRotation();
    }

    public void ComparePositionAndRotation()
    {
        //Debug.LogError(thisGameObjectName + " position + rotation check done");

        if(roundedRefAttachCubePosX > roundedAttachObjectPosX - 0.05f && roundedRefAttachCubePosX < roundedAttachObjectPosX + 0.05f)
        {
            muscleTestingScenarioSetup.muscleTestingScore++;
           //Debug.Log("Object X position within bounds");
        }

        if (roundedRefAttachCubePosY > roundedAttachObjectPosY - 0.05f && roundedRefAttachCubePosY < roundedAttachObjectPosY + 0.05f)
        {
            muscleTestingScenarioSetup.muscleTestingScore++;
            //Debug.Log("Object Y position within bounds");
        }

        if (roundedRefAttachCubePosZ > roundedAttachObjectPosZ - 0.05f && roundedRefAttachCubePosZ < roundedAttachObjectPosZ + 0.05f)
        {
            muscleTestingScenarioSetup.muscleTestingScore++;
            //Debug.Log("Object Z position within bounds");
        }

        if (roundedRefAttachCubeRotX > roundedAttachObjectRotX - 5 && roundedRefAttachCubeRotX < roundedAttachObjectRotX + 5)
        {
            muscleTestingScenarioSetup.muscleTestingScore++;
            //Debug.Log("Object X rotation within bounds");
        }

        if (roundedRefAttachCubeRotY > roundedAttachObjectRotY - 5 && roundedRefAttachCubeRotY < roundedAttachObjectRotY + 5)
        {
            muscleTestingScenarioSetup.muscleTestingScore++;
            //Debug.Log("Object Y rotation within bounds");
        }

        if (roundedRefAttachCubeRotZ > roundedAttachObjectRotZ - 5 && roundedRefAttachCubeRotZ < roundedAttachObjectRotZ + 5)
        {
            muscleTestingScenarioSetup.muscleTestingScore++;
            //Debug.Log("Object Z rotation within bounds");
        }
    }

    public void GetDestinationObjectValues()
    {
        attachObjectRotation = attachObject.transform.eulerAngles;
        attachObjectPosition = attachObject.transform.position;

        roundedAttachObjectPosX = attachObjectPosition.x;
        roundedAttachObjectPosY = attachObjectPosition.y;
        roundedAttachObjectPosZ = attachObjectPosition.z;

        roundedAttachObjectPosX = Mathf.Round(roundedAttachObjectPosX * 100) * 0.01f;
        roundedAttachObjectPosY = Mathf.Round(roundedAttachObjectPosY * 100) * 0.01f;
        roundedAttachObjectPosZ = Mathf.Round(roundedAttachObjectPosZ * 100) * 0.01f;
        roundedAttachObjectPosition = new Vector3(roundedAttachObjectPosX, roundedAttachObjectPosY, roundedAttachObjectPosZ);

        roundedAttachObjectRotX = Mathf.Round(attachObjectRotation.x);
        roundedAttachObjectRotY = Mathf.Round(attachObjectRotation.y);
        roundedAttachObjectRotZ = Mathf.Round(attachObjectRotation.z);
        roundedAttachObjectRotation = new Vector3(roundedAttachObjectRotX, roundedAttachObjectRotY, roundedAttachObjectRotZ);

    }

    public void GetRefAttachObjectValues()
    {
        refAttachCubePosition = refAttachCube.transform.position;
        refAttachCubeRotation = refAttachCube.transform.eulerAngles;

        roundedRefAttachCubePosX = refAttachCubePosition.x;
        roundedRefAttachCubePosY = refAttachCubePosition.y;
        roundedRefAttachCubePosZ = refAttachCubePosition.z;

        roundedRefAttachCubePosX = Mathf.Round(roundedRefAttachCubePosX * 100) * 0.01f;
        roundedRefAttachCubePosY = Mathf.Round(roundedRefAttachCubePosY * 100) * 0.01f;
        roundedRefAttachCubePosZ = Mathf.Round(roundedRefAttachCubePosZ * 100) * 0.01f;
        roundedRefAttachCubePosition = new Vector3(roundedRefAttachCubePosX, roundedRefAttachCubePosY, roundedRefAttachCubePosZ);

        roundedRefAttachCubeRotX = Mathf.Round(refAttachCubeRotation.x);
        roundedRefAttachCubeRotY = Mathf.Round(refAttachCubeRotation.y);
        roundedRefAttachCubeRotZ = Mathf.Round(refAttachCubeRotation.z);
        roundedRefAttachCubeRotation = new Vector3(roundedRefAttachCubeRotX, roundedRefAttachCubeRotY, roundedRefAttachCubeRotZ);
    }


    public IEnumerator SetParent()
    {
        yield return new WaitForSeconds(0.75f);
        thisGameObject.transform.parent = attachObject.transform;


    }
}
