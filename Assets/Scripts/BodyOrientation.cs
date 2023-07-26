using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyOrientation : MonoBehaviour
{
    public SceneAndScoreManager sceneAndScoreManager;
    public GameObject wholeBody;
    public GameObject table;


    private void Awake()
    {
        sceneAndScoreManager = GameObject.FindGameObjectWithTag("SceneAndScoreManager").GetComponent<SceneAndScoreManager>();

    }

    // Start is called before the first frame update
    void Start()
    {
        if(sceneAndScoreManager.bodyOrientation == "left")
        {
            HeadLeftOrientation();
        }
        else if(sceneAndScoreManager.bodyOrientation == "right")
        {
            HeadRightOrientation();
        }
        else if(sceneAndScoreManager.bodyOrientation == "central")
        {
            CentralOrientation();
        }
        else if(sceneAndScoreManager.bodyOrientation == "vertical")
        {
            VerticalOrientation();
        }
        else
        {
            CentralOrientation();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HeadLeftOrientation()
    {
        table.SetActive(true);

        Vector3 newRotation = new Vector3(0, 270, 0);
        wholeBody.transform.eulerAngles = newRotation;
        Vector3 newPosition = new Vector3(1.75f, 0, 1.5f);
        wholeBody.transform.position = newPosition;
        sceneAndScoreManager.bodyOrientation = "left";
        
    }

    public void HeadRightOrientation()
    {
        table.SetActive(true);

        Vector3 newRotation = new Vector3(0, 90, 0);
        wholeBody.transform.eulerAngles = newRotation;
        Vector3 newPosition = new Vector3(-1.75f, 0, 1.5f);
        wholeBody.transform.position = newPosition;
        sceneAndScoreManager.bodyOrientation = "right";

    }

    public void CentralOrientation()
    {
        table.SetActive(true);
        Vector3 newRotation = new Vector3(0, 0, 0);
        wholeBody.transform.eulerAngles = newRotation;
        Vector3 newPosition = new Vector3(0, 0, 0);
        wholeBody.transform.position = newPosition;
        sceneAndScoreManager.bodyOrientation = "central";

    }

    public void VerticalOrientation()
    {
        table.SetActive(false);
        Vector3 newRotation = new Vector3(270, 35, 0);
        wholeBody.transform.eulerAngles = newRotation;
        Vector3 newPosition = new Vector3(1.8f, -0.75f, 2.5f);
        wholeBody.transform.position = newPosition;
        sceneAndScoreManager.bodyOrientation = "vertical";

    }
}
