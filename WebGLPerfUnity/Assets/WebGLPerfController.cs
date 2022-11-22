using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class WebGLPerfController : MonoBehaviour
{
    public TextMeshProUGUI FpsLabel;

    public const int MaxFrames = 30;
    private float[] frameTimes = new float[MaxFrames];
    private int frameCounterIndex;

    private void Update()
    {
        UpdateFps();
    }

    private void UpdateFps()
    {
        frameCounterIndex++;
        frameCounterIndex %= MaxFrames;
        frameTimes[frameCounterIndex] = Time.unscaledDeltaTime;
        float average = frameTimes.Sum();
        FpsLabel.text = ((int)(60 / average)).ToString();
    }
}
