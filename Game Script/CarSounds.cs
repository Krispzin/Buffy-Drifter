using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSounds : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
    public float currentSpeed;

    public Rigidbody Rigidbody;
    public AudioSource carAudio;

    public float minPitch;
    public float maxPitch;
    private float pitchFromCar;

    private void Start()
    {
        carAudio = GetComponent<AudioSource>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        currentSpeed = Rigidbody.velocity.magnitude;
        pitchFromCar = Rigidbody.velocity.magnitude / 50f;

        if (currentSpeed < minSpeed)
        {
            carAudio.pitch = minPitch;
        }

        if(currentSpeed > minSpeed && currentSpeed < maxSpeed)
        {
            carAudio.pitch = minPitch + pitchFromCar;
        }

        if (currentSpeed > maxSpeed)
        {
            carAudio.pitch = maxPitch;
        }
    }
}
