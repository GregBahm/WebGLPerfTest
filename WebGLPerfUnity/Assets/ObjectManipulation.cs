﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectManipulation : MonoBehaviour
{
    public Transform CameraOrbitTransform;
    public Camera Camera;

    public float XRotationSpeed;
    public float YRotationSpeed;

    [Range(0, 1)]
    public float Zoom;
    public float MaxCameraDistance;
    public float MinCameraDistance;
    private Vector3 cameraStartPosition;
    private Quaternion cameraStartRotation;
    public float MousewheelZoomSpeed;

    private void Start()
    {
        cameraStartPosition = CameraOrbitTransform.position;
        cameraStartRotation = CameraOrbitTransform.rotation;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DoReset();
        }

        bool uiHovered = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
        if (!uiHovered)
        {
            UpdateZoom();
            HandleCameraOrbit();
        }
        UpdateCameraForZoom();
    }

    private void UpdateZoom()
    {
        float delta = Input.mouseScrollDelta.y* MousewheelZoomSpeed;
        Zoom = Mathf.Clamp01(Zoom + delta);
    }

    private void UpdateCameraForZoom()
    {
        float cameraZ = Mathf.Lerp(MinCameraDistance, MaxCameraDistance, Zoom);
        Camera.transform.localPosition = new Vector3(0, 0, cameraZ);
    }

    private void DoReset()
    {
        CameraOrbitTransform.rotation = cameraStartRotation;
        Zoom = 1f;
    }

    private Vector2 startOrbitMouse;
    private Vector3 startOrbitCamera;

    private void HandleCameraOrbit()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startOrbitMouse = Input.mousePosition;
            startOrbitCamera = CameraOrbitTransform.eulerAngles;
        }
        if (Input.GetMouseButton(0))
        {
            float xDelta = startOrbitMouse.x - Input.mousePosition.x;
            float xRot = xDelta * XRotationSpeed;
            float yDelta = startOrbitMouse.y - Input.mousePosition.y;
            float yRot = yDelta * YRotationSpeed;
            CameraOrbitTransform.rotation = Quaternion.Euler(startOrbitCamera.x + yRot, startOrbitCamera.y + xRot, 0);
        }
    }
}
