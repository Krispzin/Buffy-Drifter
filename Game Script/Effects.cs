using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Effects : MonoBehaviour
{
    public ParticleSystem[] smoke;
    private PlayerScript controller;

    private void Start()
    {
        controller = GetComponent<PlayerScript>();
    }

    private void FixedUpdate()
    {
        if (controller.playPauseSmoke)
        {
            startSmoke();
        }
        else
        {
            stopSmoke();
        }
    }

    public void startSmoke()
    {
        for (int i = 0; i < smoke.Length; i++)
        {
            smoke[i].Play();
        }
    }

    public void stopSmoke()
    {
        for (int i = 0;i < smoke.Length; i++)
        {
            smoke[i].Stop();
        }
    }
}
