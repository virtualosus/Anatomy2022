using System;
using UnityEngine;


public class LookAtTarget : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        target = GameObject.FindWithTag("MainCamera").transform;


        if (target != null)
        {
            transform.LookAt(2 * transform.position - target.transform.position);
        }
    }

}