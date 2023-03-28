using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TMPro;
using UnityEngine;

public class WebGLPerfController : MonoBehaviour
{
    public TextMeshProUGUI FpsLabel;
    public TextMeshProUGUI DrawCallsLabel;

    public GameObject[] CubeSets;
    [SerializeField]
    private int batchSizeIndex;
    [SerializeField]
    private int totalCountIndex;
    public GameObject Root;

    public const int MaxFrames = 30;
    private float[] frameTimes = new float[MaxFrames];
    private int frameCounterIndex;


    private void Update()
    {
        UpdateFps();
    }

    public void SetBatchSize(int index)
    {
        batchSizeIndex = index;
    }

    public void SetTotalCountIndex(int index)
    {
        totalCountIndex = index;
    }

    public void SpawnCubes()
    {
        //Out with the old
        Destroy(Root);
        Root = new GameObject();

        int sideLength = GetSideSize();
        GameObject set = CubeSets[batchSizeIndex];
        Vector3 center = GetLocalPosition(sideLength, sideLength, sideLength) * .5f;
        for (int x = 0; x < sideLength; x++)
        {
            for (int y = 0; y < sideLength; y++)
            {
                for (int z = 0; z < sideLength; z++)
                {
                    GameObject obj = Instantiate(set);
                    obj.transform.SetParent(Root.transform, false);
                    obj.transform.localPosition = GetLocalPosition(x, y, z) - center;
                    obj.transform.localScale = new Vector3(.9f, .9f, .9f);
                }
            }
        }
        int objs = (int)Mathf.Pow(sideLength, 3);
        DrawCallsLabel.text = "Draw Calls: " + objs;
    }

    private Vector3 GetLocalPosition(int x, int y, int z)
    {
        int baseIncrement = (int)Mathf.Pow(2, batchSizeIndex);
        return new Vector3(-baseIncrement * x, -baseIncrement * y, baseIncrement * z);
    }

    private int GetSideSize()
    {
        int power = (totalCountIndex + 5) - batchSizeIndex;
        return (int)Mathf.Pow(2, power);
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
