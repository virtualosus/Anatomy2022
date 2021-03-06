using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class MuscleLearningScenarioSetup : MonoBehaviour
{
    [Header("Scripts")]
    public SceneAndScoreManager sceneAndScoreManager;
    public Timer timer;
    public OVRScreenFade ovrScreenFade;

    [Header("Origin Setup")]
    public GameObject[] muscleGroup;
    public List<string> muscleGroupList = new List<string>();
    public List<int> usedMuscleGroupsIntList = new List<int>();
    public List<string> usedMuscleGroupsStringList = new List<string>();
    public GameObject[] muscleOrigin;
    public int muscleOriginCounter;

    [Header("Scoring")]
    public int muscleLearningMaxScore;
    public int muscleLearningScore;
    public TMP_Text scoringText;

    [Header("Celebration")]
    public ParticleSystem[] confetti;
    public AudioSource[] celebrateSFX;


    private void Awake()
    {
        muscleOriginCounter = 0;
        GatherAllMuscleGroups();
        timer = GameObject.FindGameObjectWithTag("Scriptholder").GetComponent<Timer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        sceneAndScoreManager = GameObject.FindGameObjectWithTag("SceneAndScoreManager").GetComponent<SceneAndScoreManager>();
        ApplyRandomMuscleGroupToOrigin();
        muscleLearningMaxScore = muscleOrigin.Length;
        scoringText.text = "Place the first muscle group to the model to begin the timer and scoring.";
    }



    public void GatherAllMuscleGroups()
    {
        for (int i = 0; i < muscleGroup.Length; i++)
        {
            muscleGroupList.Add(muscleGroup[i].name);
            muscleGroup[i].SetActive(false);
        }
    }

    public void ApplyRandomMuscleGroupToOrigin()
    {
        int randomNum = Random.Range(0, muscleGroup.Length);
        if (usedMuscleGroupsIntList.Contains(randomNum))
        {
            ApplyRandomMuscleGroupToOrigin();
        }
        else
        {
            if(muscleOriginCounter == muscleOrigin.Length)
            {
                return;
            }
            else
            {
                usedMuscleGroupsIntList.Add(randomNum);
                usedMuscleGroupsStringList.Add(muscleGroup[randomNum].name);
                muscleGroup[randomNum].SetActive(true);
                muscleGroup[randomNum].GetComponent<SelectedObject>().origin = muscleOrigin[muscleOriginCounter].transform.position;
                muscleGroup[randomNum].transform.position = muscleOrigin[muscleOriginCounter].transform.position;
                muscleOriginCounter++;
                ApplyRandomMuscleGroupToOrigin();
            }           
        }
    }

    public void LearningMuscleCount()
    {
        timer.StartTimer();
        muscleLearningScore++;
        scoringText.text = "You have placed " + muscleLearningScore + " of " + muscleLearningMaxScore + " muscle groups.";
        if (muscleLearningScore == muscleOrigin.Length)
        {
            AllMusclesAdded();
        }
    }

    public void AllMusclesAdded()
    {
        StartCoroutine(QuizComplete());

    }


    public IEnumerator QuizComplete()
    {

        timer.StopTimer();
        yield return new WaitForSeconds(0.5f);
        Celebration();
        SetMasterScore();
        yield return new WaitForSeconds(10f);
        ovrScreenFade.FadeOut();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("SportScienceMuscleTesting_EnglishVersion");
    }

    public void Celebration()
    {
        for (int i = 0; i < celebrateSFX.Length; i++)
        {
            celebrateSFX[i].Play();
        }

        for (int i = 0; i < confetti.Length; i++)
        {
            confetti[i].Play();
        }
    }

    public void SetMasterScore()
    {
        sceneAndScoreManager.muscleLearningMaxScore = muscleLearningMaxScore;
        sceneAndScoreManager.muscleLearningScore = muscleLearningScore;
        sceneAndScoreManager.muscleLearningTime = timer.currentTime;
    }
}
