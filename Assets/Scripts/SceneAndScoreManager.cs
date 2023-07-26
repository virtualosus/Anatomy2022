using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAndScoreManager : MonoBehaviour
{
    public float boneSceneScore, boneSceneMaxScore;

    public float boneSceneTime, boneSceneMaxTime;

    public int muscleLearningScore, muscleLearningMaxScore;

    public float muscleLearningTime, muscleLearningMaxTime;

    public int muscleTestingScore, muscleTestingMaxScore;

    public float muscleTestingTime, muscleTestingMaxTime;

    public bool skeletalScene, muscleLearningScene, muscleTestingScene;

    public string bodyOrientation;


    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        //skeletalScene = false;
        //muscleLearningScene = false;
        //muscleTestingScene = false;
    }



    public void ResetMasterScores()
    {
        boneSceneScore = 0;
        boneSceneMaxScore = 0;
        boneSceneTime = 0;

        muscleLearningScore = 0;
        muscleLearningMaxScore = 0;
        muscleLearningTime = 0;

        muscleTestingScore = 0;
        muscleTestingMaxScore = 0;
        muscleTestingTime = 0;
    }
}
