using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BuildTargetManager : MonoBehaviour
{
    public ActionBasedController leftXRController, rightXRController;
    public bool quest2, pico4, questPro;

    public GameObject quest2Left, quest2Right, pico4Left, pico4Right, questProLeft, questProRight;

    private void Awake()
    {
        leftXRController = GameObject.FindWithTag("LeftHandController").GetComponent<ActionBasedController>();
        rightXRController = GameObject.FindWithTag("RightHandController").GetComponent<ActionBasedController>();

        if (quest2)
        {
            leftXRController.modelPrefab = quest2Left.transform;
            rightXRController.modelPrefab = quest2Right.transform;

        }

        if (pico4)
        {
            leftXRController.modelPrefab = pico4Left.transform;
            rightXRController.modelPrefab = pico4Right.transform;
        }

        if (questPro)
        {
            leftXRController.modelPrefab = questProLeft.transform;
            rightXRController.modelPrefab = questProRight.transform;
        }
    }
}
