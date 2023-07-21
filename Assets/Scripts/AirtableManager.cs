using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using TMPro;



public class AirtableManager : MonoBehaviour
{
    [Header("Airtable")]
    public string airtableEndpoint = "https://api.airtable.com/v0/";
    public string baseId = "YOUR_BASE_ID";
    public string tableName = "YOUR_TABLE_NAME";
    public string accessToken = "YOUR_ACCESS_TOKEN";
    private string dataToParse;
    private string selectedUniversity;
    public string tableToBeUsedFromAirtable;

    [Header("Assesment Information")]
    public SceneAndScoreManager sceneAndScoreManager;
    public string dateTime;
    public string studentNumber;
    public string testScore;
    public string testTime;
    public bool extraTime;
    public string extraTimeString;
    public TMP_Text extraTimeButtonTMP;


    public void CreateRecord()
    {
        dateTime = System.DateTime.Now.ToString("dd.MM.yyyy HH.mm");

        // Create the URL for the API request
        string url = airtableEndpoint + baseId + "/" + tableToBeUsedFromAirtable;

        // Create the data to be sent in the request
        string jsonFields = "{\"fields\": {" +
                                    "\"Date and Time\":\"" + dateTime + "\", " +
                                    "\"Student Number\":\"" + studentNumber + "\", " +
                                    "\"Test Score\":\"" + testScore + "\", " +
                                    "\"Time Remaining\":\"" + testTime + "\", " +
                                    "\"Extra Time Added\":\"" + extraTimeString + "\"" +
                                    "}}";
        // Start the coroutine to send the API request
        StartCoroutine(SendRequest(url, "POST", response =>
        {
            Debug.Log("Record created: " + response);
        }, jsonFields));
    }

    public void TestCreateRecord()
    {
        dateTime = System.DateTime.Now.ToString("dd.MM.yyyy HH.mm");

        // Create the URL for the API request
        string url = airtableEndpoint + baseId + "/" + tableName;

        Debug.Log(url);

        // Create the data to be sent in the request
        string jsonFields = "{\"fields\": {" +
                                    "\"Date and Time\":\"" + dateTime + "\", " +
                                    "\"Extra Time Added\":\"" + extraTimeString + "\"" +
                                    "}}";
        string jsonData = "{\"fields\": " + jsonFields + "}";

        Debug.Log(jsonData);

        // Start the coroutine to send the API request
        StartCoroutine(SendRequest(url, "POST", response =>
        {
            Debug.Log("Record created: " + response);
        }, jsonData));
    }

    // Unity coroutine to make API requests
    private IEnumerator SendRequest(string url, string method, Action<string> callback, string jsonData = "")
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = method;
        request.ContentType = "application/json";
        request.Headers["Authorization"] = "Bearer " + accessToken;

        if (!string.IsNullOrEmpty(jsonData))
        {
            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(jsonData);
            }
        }

        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        {
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string jsonResponse = reader.ReadToEnd();
                if (callback != null)
                {
                    callback(jsonResponse);
                }
            }
        }

        yield return null;
    }


    public void TableSelector(string tableToSelect)
    {
        selectedUniversity = tableToSelect;
        Debug.Log(selectedUniversity);
        CallGetCellValue();
    }

    public void CallGetCellValue()
    {
        RetrieveRecord("recWq2NPUgOhAqx3w", "App Table References");
    }


    // Example method to retrieve a record from Airtable based on record ID
    public void RetrieveRecord(string recordId, string readTableName)
    {
        // Create the URL for the API request
        string url = airtableEndpoint + baseId + "/" + readTableName + "/" + recordId;

        // Start the coroutine to send the API request
        StartCoroutine(SendRequest(url, "GET", response =>
        {
            // Parse the JSON response
            var responseObject = JsonUtility.FromJson<Dictionary<string, object>>(response);

            dataToParse = response;
            JSONParse();

        }));
    }

    public void JSONParse()
    {
        if(selectedUniversity == "Swansea")
        {
            string source = dataToParse;
            dynamic data = JObject.Parse(source);

            tableToBeUsedFromAirtable = data.fields.SwanseaTableName;

            Debug.Log("Table to be used: " + tableToBeUsedFromAirtable);
        }

        if(selectedUniversity == "Canberra")
        {
            string source = dataToParse;
            dynamic data = JObject.Parse(source);

            tableToBeUsedFromAirtable = data.fields.CanberraTableName;

            Debug.Log("Table to be used: " + tableToBeUsedFromAirtable);
        }
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

    //public void Test(string university)
    //{
    //    TableSelector(university);
    //    CallGetCellValue();
    //}
}
