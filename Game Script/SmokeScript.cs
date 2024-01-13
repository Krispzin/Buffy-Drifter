using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeScript : MonoBehaviour
{

    public ParticleSystem[] smoke;
    private PlayerScript PlayerScript;

    private void Start()
    {
        PlayerScript = gameObject.GetComponent<PlayerScript>();

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
