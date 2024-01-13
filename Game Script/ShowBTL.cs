using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowBTL : MonoBehaviour
{
    public Text TimeLabel;
    public Checkpoint checkpoint;

    private void FixedUpdate()
    {
        float nbestLapTime = checkpoint.bestLapTime;

        if (TimeLabel != null)
        {
            string formattedBestTime = $"Your Best Time Lap: {Mathf.FloorToInt(nbestLapTime / 60)}:{nbestLapTime % 60:00.000}";
            TimeLabel.text = formattedBestTime;
        }
    }
}
