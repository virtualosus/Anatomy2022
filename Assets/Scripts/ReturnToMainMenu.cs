using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour
{
    public bool demoMode;

    public OVRScreenFade ovrScreenFade;

    //private GameObject gameObjectToDestroy;

    private void Awake()
    {
        ovrScreenFade = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<OVRScreenFade>();
    }

    public void GoToMainMenu()
    {
        StartCoroutine(GoToMainMenuRoutine());
    }

    public IEnumerator GoToMainMenuRoutine()
    {
        ovrScreenFade.FadeOut();
        yield return new WaitForSeconds(2f);
        GameObject gameObjectToDestroy = GameObject.FindGameObjectWithTag("SceneAndScoreManager");
        Destroy(gameObjectToDestroy);
        if (demoMode)
        {

            SceneManager.LoadScene("Urdd_LanguageSelect");

        }
        else
        {     

            SceneManager.LoadScene("SportScienceMainMenu_EnglishVersion");
        }
    }
}
