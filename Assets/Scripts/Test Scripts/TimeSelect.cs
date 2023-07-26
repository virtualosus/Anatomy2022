using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeSelect : MonoBehaviour
{
    public SceneAndScoreManager sceneAndScoreManager;
    public Button fiveMinBtn, tenMinBtn, unlimitedTimeBtn;
    public TMP_Text fiveMinText, tenMinText, unlimitedTimeText;


    void Start()
    {
        TenMinutes();        
    }

    public void FiveMinutes()
    {
        sceneAndScoreManager.boneSceneMaxTime = 5;
        fiveMinBtn.enabled = false;
        tenMinBtn.enabled = true;
        unlimitedTimeBtn.enabled = true;
        fiveMinText.color = Color.green;
        tenMinText.color = Color.white;
        unlimitedTimeText.color = Color.white;
    }

    public void TenMinutes()
    {
        sceneAndScoreManager.boneSceneMaxTime = 10;
        fiveMinBtn.enabled = true;
        tenMinBtn.enabled = false;
        unlimitedTimeBtn.enabled = true;
        fiveMinText.color = Color.white;
        tenMinText.color = Color.green;
        unlimitedTimeText.color = Color.white;
    }

    public void UnlimitedMinutes()
    {
        sceneAndScoreManager.boneSceneMaxTime = 999;
        fiveMinBtn.enabled = true;
        tenMinBtn.enabled = true;
        unlimitedTimeBtn.enabled = false;
        fiveMinText.color = Color.white;
        tenMinText.color = Color.white;
        unlimitedTimeText.color = Color.green;
    }


}
