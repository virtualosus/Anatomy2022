using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnatomyKeyPrefs : MonoBehaviour
{

    public bool setKeysBool, getKeysBool;    

    public string aPIKeyString;
    public string appKeyString;
    public string tableNameString;

    public string aPIKeyStringPrevious;
    public string appKeyStringPrevious;
    public string tableNameStringPrevious;

    public float skeletalSceneTime;
    public float muscleLearningSceneTime;
    public float muscleTestingSceneTime;

    public float skeletalSceneTimePrevious;
    public float muscleLearningSceneTimePrevious;
    public float muscleTestingSceneTimePrevious;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (setKeysBool)
        {
            SetAppKeys();
            setKeysBool = false;
        }

        if (getKeysBool)
        {
            RetrieveAppKeys();
            getKeysBool = false;
        }
    }


    public void SetAppKeys()
    {
        SetString("API Key", aPIKeyString);
        SetString("App Key", appKeyString);
        SetString("Table Name", tableNameString);
    }

    public void RetrieveAppKeys()
    {
        aPIKeyStringPrevious = GetString("API Key");
        appKeyStringPrevious = GetString("App Key");
        tableNameStringPrevious = GetString("Table Name");
    }

    public void SetTimes()
    {
        SetFloat("Skeletal Scene Time", skeletalSceneTime);
        SetFloat("Muscle Learning Scene Time", muscleLearningSceneTime);
        SetFloat("Muscle Testing Scene Time", muscleTestingSceneTime);
    }

    public void GetTimes()
    {
        skeletalSceneTimePrevious = GetFloat("Skeletal Scene Time");
        muscleLearningSceneTimePrevious = GetFloat("Muscle Learning Scene Time");
        muscleTestingSceneTimePrevious = GetFloat("Muscle Testing Scene Time");
    }


    public void SetString(string KeyName, string Value)
    {
        PlayerPrefs.SetString(KeyName, Value);
    }

    public string GetString(string KeyName)
    {
        return PlayerPrefs.GetString(KeyName);
    }

    public void SetFloat(string KeyName, float Value)
    {
        PlayerPrefs.SetFloat(KeyName, Value);
    }

    public float GetFloat(string KeyName)
    {
        return PlayerPrefs.GetFloat(KeyName);
    }
}
