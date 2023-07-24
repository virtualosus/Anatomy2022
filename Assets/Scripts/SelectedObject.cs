using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class SelectedObject : MonoBehaviour
{
    [Header("Scene Specific")]
    public string sceneName;

    [Header("Scripts")]
    private CustomPointer rightCustomPointer, leftCustomPointer;
    private Outline outline;
    //public OnboardingManager onboardingManager;
    public OnboardingSceneManager onboardingSceneManager;
    public bool onboardingScene;
    public GameObject onboardingHolder;

    [Header("This Game Object")]
    public GameObject thisGameObject;
    public string thisGameObjectName;

    [Header("Attach Specific")]
    public GameObject rightHand; 
    public GameObject rightHandAttach;
    public GameObject leftHand;
    public GameObject leftHandAttach;
    public bool leftHandSelect;
    public bool rightHandSelect;

    public Quaternion originalRotation;
    public Vector3 origin;

    public bool atAttachPoint, atOriginPoint;
    public bool selected;
         


    private void Awake()
    {
        rightHand = GameObject.FindWithTag("PlayerRightHand");
        rightHandAttach = GameObject.FindWithTag("PlayerRightHandAttach");

        leftHand = GameObject.FindWithTag("PlayerLeftHand");
        leftHandAttach = GameObject.FindWithTag("PlayerLeftHandAttach");

        rightCustomPointer = rightHand.GetComponent<CustomPointer>();
        leftCustomPointer = leftHand.GetComponent<CustomPointer>();

        onboardingHolder = GameObject.Find("---ONBOARDING ---");
        //onboardingManager = onboardingHolder.GetComponent<OnboardingManager>();
        onboardingSceneManager = onboardingHolder.GetComponent<OnboardingSceneManager>();

        outline = GetComponentInChildren<Outline>();

        thisGameObject = transform.gameObject;
        thisGameObjectName = transform.gameObject.name;

        outline = thisGameObject.GetComponentInChildren<Outline>();

        originalRotation = thisGameObject.transform.rotation;

    }


    // Start is called before the first frame update
    void Start()
    {
        rightCustomPointer.handDeselect.AddListener(DeactivateSelect);
        rightCustomPointer.handReturnOrigin.AddListener(ReturnToOrigin);
        rightCustomPointer.handMoveTowards.AddListener(MoveToAttach);
        
        leftCustomPointer.handDeselect.AddListener(DeactivateSelect);
        leftCustomPointer.handReturnOrigin.AddListener(ReturnToOrigin);
        leftCustomPointer.handMoveTowards.AddListener(MoveToAttach);

        SetUpOutline();
    }

    // Update is called once per frame
    void Update()
    {
        if (atAttachPoint)
        {
            if (rightHandSelect)
            {
                thisGameObject.transform.position = rightHand.transform.position;
                thisGameObject.transform.rotation = rightHandAttach.transform.rotation;
            }
            else if(leftHandSelect)
            {
                thisGameObject.transform.position = leftHand.transform.position;
                thisGameObject.transform.rotation = leftHandAttach.transform.rotation;
            }

        }
    }

    public void SetUpOutline()
    {
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = new Color(0f, 255f, 250f);
        outline.OutlineWidth = 0f;

    }


    public void ActivateSelect()
    {
        if (onboardingScene)
        {
            onboardingSceneManager.SelectObject();
        }

        //if (!onboardingManager.selectBone)
        //{
        //    onboardingManager.selectBone = true;
        //    onboardingManager.UpdateChecklist();
        //}



        outline.OutlineWidth = 5;

        selected = true;
        rightCustomPointer.linePointerOn = false;
        leftCustomPointer.linePointerOn = false;
    }

    public void DeactivateSelect()
    {
        if (onboardingScene)
        {
            onboardingSceneManager.DeselectObject();
        }

        //onboardingManager.deselectBone = true;
        //onboardingManager.UpdateChecklist();
        outline.OutlineWidth = 0;
        atAttachPoint = false;
        leftHandSelect = false;
        rightHandSelect = false;
        rightCustomPointer.holdingObject = false;
        leftCustomPointer.holdingObject = false;
        selected = false;
        rightCustomPointer.linePointerOn = true;
        leftCustomPointer.linePointerOn = true;
    }


    public void MoveToAttach()
    {
        if (!atAttachPoint && selected)
        {
            if (rightHandSelect)
            {
                thisGameObject.transform.position = rightHand.transform.position;
                //onboardingManager.moveBone = true;
                //onboardingManager.UpdateChecklist();
            }
            else if (leftHandSelect)
            {
                thisGameObject.transform.position = leftHand.transform.position;
                //onboardingManager.moveBone = true;
                //onboardingManager.UpdateChecklist();
            }


        }
        else
        {

        }
    }

    public void ReturnToOrigin()
    {
        if (!atOriginPoint && selected)
        {
            atAttachPoint = false;
            thisGameObject.transform.position = origin;
            thisGameObject.transform.rotation = originalRotation;

            if (onboardingScene)
            {
                onboardingSceneManager.ReturnToOrigin();
            }

            //onboardingManager.returnBoneToOrigin = true;
            //onboardingManager.UpdateChecklist();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerRightHand" && rightHandSelect)
        {
            atAttachPoint = true;
        }

        if(other.tag == "PlayerLeftHand" && leftHandSelect)
        {
            atAttachPoint = true;
        }

       
        if (other.tag == "Origin")
        {
            atOriginPoint = true;
        }
        else
        {
            atOriginPoint = false;
        }
    }
}

