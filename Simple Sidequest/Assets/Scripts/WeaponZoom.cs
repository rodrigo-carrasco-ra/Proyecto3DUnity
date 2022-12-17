using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera fpsCamera;
    [SerializeField] PlayerController playerController;
    [SerializeField] float zoomedOutFOV = 60f;
    [SerializeField] float zoomedInFOV = 20f;
    [SerializeField] float zoomOutSensitivity = 30f;
    [SerializeField] float zoomInSensitivity = 60f;

    bool zoomedInToogle = false;

    private void OnDisable()
    {
        ZoomOut();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (zoomedInToogle==false)
            {
                ZoomIn();
            }
            else
            {
                ZoomOut();
            }
        }
    }


    private void ZoomIn()
    {
        zoomedInToogle = true;
        fpsCamera.fieldOfView = zoomedInFOV;
        //playerController.rotationSensibility = zoomInSensitivity;
    }

    private void ZoomOut()
    {
        zoomedInToogle = false;
        fpsCamera.fieldOfView = zoomedOutFOV;
        //playerController.rotationSensibility = zoomOutSensitivity;
    }


}
