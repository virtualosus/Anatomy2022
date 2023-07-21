using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class MuscleTestingScenarioSetup : MonoBehaviour
{
    [Header("Scripts")]
    public SceneAndScoreManager sceneAndScoreManager;
    public Timer timer;
    public OVRScreenFade ovrScreenFade;
    public AirtableManager airtableManager;

    [Header("Origin Setup")]
    public GameObject[] muscleGroup;
    public GameObject sceneAndScoreObject;
    public List<string> muscleGroupList = new List<string>();
    public List<int> usedMuscleGroupsIntList = new List<int>();
    public List<string> usedMuscleGroupsStringList = new List<string>();
    public GameObject[] muscleOrigin;
    public int muscleOriginCounter;

    [Header("Scoring")]
    public int muscleTestingMaxScore;
    public int muscleTestingScore;
    public TMP_Text scoringText;
    public Button completeButton;

    [Header("Celebration")]
    public ParticleSystem[] confetti;
    public AudioSource[] celebrateSFX;


    private void Awake()
    {
        //muscleOriginCounter = 0;
        GatherAllMuscleGroups();
        timer = GameObject.FindGameObjectWithTag("Scriptholder").GetComponent<Timer>();
        airtableManager = GameObject.FindGameObjectWithTag("SceneAndScoreManager").GetComponent<AirtableManager>();
        sceneAndScoreObject = GameObject.FindGameObjectWithTag("SceneAndScoreManager");
    }

    // Start is called before the first frame update
    void Start()
    {
        sceneAndScoreManager = GameObject.FindGameObjectWithTag("SceneAndScoreManager").GetComponent<SceneAndScoreManager>();
        ApplyRandomMuscleGroupToOrigin();
        muscleTestingMaxScore = muscleOrigin.Length * 6;
        //scoringText.text = "Place the first muscle group to the model to begin the timer and scoring.";
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


    public void TestingMuscleTimerStart()
    {
        timer.StartTimer();
    }

    public void GatherScores()
    {
        StartCoroutine(QuizComplete());
    }


    public IEnumerator QuizComplete()
    {
        completeButton.enabled = false;
        timer.StopTimer();

        for(int i = 0; i < usedMuscleGroupsStringList.Count; i++)
        {
            GameObject temp = GameObject.Find(usedMuscleGroupsStringList[i]);
            temp.GetComponent<PositionAndRotationCompare>().ComparePositionAndRotation(); 
        }

        yield return new WaitForSeconds(0.5f);
        Celebration();
        SetMasterScore();
        airtableManager.testScore = sceneAndScoreManager.muscleTestingScore.ToString();
        airtableManager.testTime = sceneAndScoreManager.muscleTestingTime.ToString();
        airtableManager.CreateRecord();
        yield return new WaitForSeconds(10f);
        ovrScreenFade.FadeOut();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("SportScienceScoreFeedback_EnglishVersion");
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
        sceneAndScoreManager.muscleTestingMaxScore = muscleTestingMaxScore;
        sceneAndScoreManager.muscleTestingScore = muscleTestingScore;
        sceneAndScoreManager.muscleTestingTime = timer.currentTime/60;
    }

    public void CompleteQuizAndSendToAirtable()
    {
        GatherScores();
        airtableManager.testScore = sceneAndScoreManager.muscleTestingScore.ToString();
        airtableManager.testTime = sceneAndScoreManager.muscleTestingTime.ToString();
        airtableManager.CreateRecord();
    }
}
