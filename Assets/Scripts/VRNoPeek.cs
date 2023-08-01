using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRNoPeek : MonoBehaviour
{
    public OVRScreenFade screenFade;


    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Wall")
        {
            screenFade.fadeTime = 0.2f;
            screenFade.FadeOut();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Wall")
        {
            screenFade.fadeTime = 0.2f;
            screenFade.FadeIn();
            screenFade.fadeTime = 2f;
        }
    }
}
