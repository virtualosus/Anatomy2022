using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using VRKeyboard.Utils;
using UnityEngine.UI;


public class MainMenuControl : MonoBehaviour
{
    public KeyboardManager keyboardManager;
    public OVRScreenFade ovrScreenFade;
    public AirtableManager airtableManager;
    public GameObject keyboardCanvas, studentNumberCanvas, mainMenuCanvas, uniSelectCanvas;
    public Animator mainMenuCanvasAni, studentNumberCanvasAni, keyboardAni, uniSelectAni;
    public string sceneToLoad;
    public TMP_Text studentNumberTMP;

    public Button loadTestSceneButton;


    public void LoadSkeletalScene()
    {
        mainMenuCanvasAni.Play("AirTableInfoFadeOut");
        sceneToLoad = "SportScienceSkeletal_EnglishVersion";
        StartCoroutine(SceneLoader());
    }

    public void LoadMuscleTrainingScene()
    {
        mainMenuCanvasAni.Play("AirTableInfoFadeOut");
        sceneToLoad = "SportScienceMuscleLearning_EnglishVersion";
        StartCoroutine(SceneLoader());
        

    }


    public void LoadUniSelectInput()
    {
        mainMenuCanvasAni.Play("AirTableInfoFadeOut");
        StartCoroutine(LoadUniSelectMenu());
    }

    public void LoadStudentNumberInput()
    {
        uniSelectAni.Play("AirTableInfoFadeOut");
        StartCoroutine(LoadMuscleTestingMenu());
    }

    public void LoadMuscleTestingScene()
    {
        studentNumberCanvasAni.Play("AirTableInfoFadeOut");
        keyboardAni.Play("KeyboardFadeOut");
        sceneToLoad = "SportScienceMuscleTesting_EnglishVersion";
        StartCoroutine(SceneLoader());
    }

    public IEnumerator SceneLoader()
    {
        yield return new WaitForSeconds(1f);
        ovrScreenFade.FadeOut();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneToLoad);
    }

    public IEnumerator LoadUniSelectMenu()
    {
        mainMenuCanvasAni.Play("AirTableInfoFadeOut");
        yield return new WaitForSeconds(0.5f);
        mainMenuCanvas.SetActive(false);
        uniSelectCanvas.SetActive(true);
    }

    public IEnumerator LoadMuscleTestingMenu()
    {
        uniSelectAni.Play("AirTableInfoFadeOut");
        yield return new WaitForSeconds(0.5f);
        uniSelectCanvas.SetActive(false);
        studentNumberCanvas.SetActive(true);
    }

    public void OpenKeyboard()
    {
        keyboardCanvas.SetActive(true);
    }

    public void Update()
    {
        studentNumberTMP.text = keyboardManager.Input;
        airtableManager.studentNumber = studentNumberTMP.text.ToString();

        if (studentNumberCanvas.activeSelf)
        {
            if (studentNumberTMP.text.Length <= 5)
            {
                loadTestSceneButton.enabled = false;
            }
            else
            {
                loadTestSceneButton.enabled = true;
            }
        }
    }
}
