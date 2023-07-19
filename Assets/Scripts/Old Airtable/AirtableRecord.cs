using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRKeyboard.Utils;
using TMPro;


public class AirtableRecord : MonoBehaviour
{
    public KeyboardManager keyboardManager;
    public SceneAndScoreManager sceneAndScoreManager;
    public LoadAirtableSettings loadAirtableSettings;
    public SetEnvironment setEnvironment;
    public CreateRecord createRecord;

    public bool recordData, studentNumberTest, connectionTest, extraTime;

    public string dateTime, studentNumber, testScore, testTime;
    public string extraTimeString;


    public string JSONString;

    public TMP_Text extraTimeButtonTMP;

    private void Awake()
    {
        sceneAndScoreManager = GetComponent<SceneAndScoreManager>();
        setEnvironment = GetComponent<SetEnvironment>();
        createRecord = GetComponent<CreateRecord>();
        extraTime = false;
        extraTimeString = "No";
    }

    private void Start()
    {

    }

#if UNITY_EDITOR
    
    public void Update()
    {
        if (recordData)
        {
            SendToAirtable();
            if (recordData)
            {
                recordData = false;
            }
        }

        if (studentNumberTest)
        {
            SendResultsToAirtable();
            if (studentNumberTest)
            {
                studentNumberTest = false;
            }
        }

        if (connectionTest)
        {
            TestAirtableConnection();
            if (connectionTest)
            {
                connectionTest = false;
            }
        }

    }
#endif

    public void AttemptConnect()
    {
        StartCoroutine(AttemptConnectCoroutine());
    }

    public IEnumerator AttemptConnectCoroutine()
    {
        setEnvironment.PrepareEnvironment();
        yield return new WaitForSeconds(1f);
        createRecord.CreateAirtableRecord();
    }

    public void SendToAirtable()
    {
        dateTime = System.DateTime.Now.ToString("dd.MM.yyyy HH.mm");

        JSONString = "{\"fields\": {" +
                                    "\"Date and Time\":\"" + dateTime + "\", " +
                                    "\"Bone Scene Time Remaining\":\"" + sceneAndScoreManager.boneSceneTime + "\", " +
                                    "\"Bone Scene Score\":\"" + sceneAndScoreManager.boneSceneScore + "\", " +
                                    "\"Bone Scene Max Score\":\"" + sceneAndScoreManager.boneSceneMaxScore + "\", " +
                                    "\"Muscle Learning Time Remaining\":\"" + sceneAndScoreManager.muscleLearningTime + "\", " +
                                    "\"Muscle Learning Score\":\"" + sceneAndScoreManager.muscleLearningScore + "\", " +
                                    "\"Muscle Learning Max Score\":\"" + sceneAndScoreManager.muscleLearningMaxScore + "\", " +
                                    "\"Muscle Testing Time Remaining\":\"" + sceneAndScoreManager.muscleTestingTime + "\", " +
                                    "\"Muscle Testing Score\":\"" + sceneAndScoreManager.muscleTestingScore + "\", " +
                                    "\"Muscle Testing Max Score\":\"" + sceneAndScoreManager.muscleTestingMaxScore + "\"" +
                                    "}}";
        createRecord.NewRecordJson = JSONString;
        AttemptConnect();
    }

    public void SendStudentNumberTestToAirtable()
    {
        dateTime = System.DateTime.Now.ToString("dd.MM.yyyy HH.mm");

        JSONString = "{\"fields\": {" +
                                    "\"Date and Time\":\"" + dateTime + "\", " +
                                    "\"Student Number\":\"" + studentNumber + "\", " +
                                    "\"Test Time\":\"" + 123456 + "\"" +
                                    "}}";
        createRecord.NewRecordJson = JSONString;
        AttemptConnect();
    }

    public void SendResultsToAirtable()
    {
        dateTime = System.DateTime.Now.ToString("dd.MM.yyyy HH.mm");

        JSONString = "{\"fields\": {" +
                                    "\"Date and Time\":\"" + dateTime + "\", " +
                                    "\"Student Number\":\"" + studentNumber + "\", " +
                                    "\"Test Score\":\"" + testScore + "\", " +
                                    "\"Time Remaining\":\"" + testTime + "\", " +
                                    "\"Extra Time Added\":\"" + extraTimeString + "\"" +
                                    "}}";
        createRecord.NewRecordJson = JSONString;
        AttemptConnect();
    }

    public void TestAirtableConnection()
    {
        dateTime = System.DateTime.Now.ToString("dd.MM.yyyy HH.mm");

        JSONString = "{\"fields\": {" +
                                    "\"Date and Time\":\"" + dateTime + "\", " +
                                    "\"Student Number\":\"" + studentNumber + "\", " +
                                    "\"Test Check\":\"" + "Test Connection Success" + "\"" +
                                    "}}";
        createRecord.NewRecordJson = JSONString;
        StartCoroutine(TestAirtableConnectionRoutine());
    }

    public IEnumerator TestAirtableConnectionRoutine()
    {
        createRecord.TableName = "ConnectionTester_DO_NOT_DELETE";
        setEnvironment.PrepareEnvironment();
        yield return new WaitForSeconds(0.5f);
        createRecord.CreateAirtableRecord();
        yield return new WaitForSeconds(0.5f);
        createRecord.TableName = loadAirtableSettings.airtableTitle;
    }

    public void ClearStudentNumber()
    {
        keyboardManager.Input = "";
        studentNumber = "";
    }

    public void ExtraTime()
    {
        if (!extraTime)
        {
            extraTime = true;
            extraTimeButtonTMP.text = "Remove Extra Time";
            sceneAndScoreManager.muscleTestingMaxTime = 12.5f;
            extraTimeString = "Yes";
        }
        else
        {
            extraTime = false;
            extraTimeButtonTMP.text = "Add Extra Time";
            sceneAndScoreManager.muscleTestingMaxTime = 10;
            extraTimeString = "No";
        }
    }
}

