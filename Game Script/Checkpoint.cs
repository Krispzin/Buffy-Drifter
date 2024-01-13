using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("You Win Panel")]
    public GameObject youWinPanel;

    [Header("Checkpoints")]
    public GameObject start;
    public GameObject end;
    public GameObject[] checkpoints;

    [Header("Settings")]
    public float laps = 1;

    [Header("Information")]
    private float currentCheckpoint;
    private float currentLap;
    private bool started;
    private bool finished;

    private float currentLapTime;
    public float bestLapTime;
    private float bestLap;

    private void Start()
    {
        currentCheckpoint = 0;
        currentLap = 1;

        started = false;
        finished = false;

        currentLapTime = 0;
        bestLapTime = 0;
        bestLapTime = 0;
    }

    private void Update()
    {
        if (started && !finished)
        {
            currentLapTime += Time.deltaTime;

            if (bestLap == 0)
            {
                bestLap = 1;
            }
        }

        if (started)
        {
            if (bestLap == currentLap)
            {
                bestLapTime = currentLapTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            GameObject thisCheckpoint = other.gameObject;


            if (thisCheckpoint == start && !started)
            {
                print("Started");
                started = true;
            }
            else if (thisCheckpoint == end && started)
            {
                if (currentLap == laps) 
                { 
                    if (currentCheckpoint == checkpoints.Length)
                    {
                        if (currentLapTime < bestLapTime)
                        {
                            bestLap = currentLap;
                        }


                        finished = true;
                        print("Finished");
                        youWinPanel.SetActive(true);
                    }
                    else
                    {
                        print("Did not go through all checkpoints");
                    }
                }
                else if (currentLap < laps)
                {
                    if (currentCheckpoint == checkpoints.Length)
                    {
                        if (currentLapTime < bestLapTime)
                        {
                            bestLap = currentLap;
                            bestLapTime = currentLapTime;
                        }

                        currentLap++;
                        currentCheckpoint = 0;
                        currentLapTime = 0;
                        print($"Started lap {currentLap} - {Mathf.FloorToInt(currentLapTime / 60)}:{currentLapTime % 60:00.000}");
                    }
                }
                else
                {
                    print("Did not go through all the checkpoints");
                }
            }

            // Loop through checkpoints and compare and check which one the player passed through
            for (int i = 0; i < checkpoints.Length; i++)
            {
                if (finished)
                    return;
                
                // If the 
                if (thisCheckpoint == checkpoints[i] && i == currentCheckpoint)
                {
                    print($"Correct checkpoint - {Mathf.FloorToInt(currentLapTime / 60)}:{currentLapTime % 60:00.000}");
                    currentCheckpoint++;
                }
                else if (thisCheckpoint == checkpoints[i] && i != currentCheckpoint)
                {
                    print("incorrect checkpoint");
                }
            }
        }
    }

    private void OnGUI()
    {
        // Current Time 
        string formattedCurrentTime = $"Current: {Mathf.FloorToInt(currentLapTime / 60)}:{currentLapTime % 60:00.000} - (Lap {currentLap})";
        GUI.Label(new Rect(50, 10, 250, 100), formattedCurrentTime);

        string formattedBestTime = $"Best: {Mathf.FloorToInt(bestLapTime / 60)}:{bestLapTime % 60:00.000} - (Lap {bestLap})";
        GUI.Label(new Rect(250, 10, 250, 100), formattedBestTime);
    }
}
