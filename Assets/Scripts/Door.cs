using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    new Camera camera;
    public GameObject nextCameraLocation;
    Vector3 cameraStartPosition;
    public float cameraRelocateSpeed;
    float cameraRelocatingProgress;
    public bool cameraRelocating;
    void Start()
    {
        camera = Camera.main;
        cameraStartPosition = camera.transform.position.normalized;
        cameraRelocating = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (cameraRelocating)
        {
            RelocateCamera();
        }
        //Debug.Log(cameraRelocating);
    }

    void RelocateCamera()
    {
        camera.transform.position = Vector3.Lerp(cameraStartPosition, nextCameraLocation.transform.position, cameraRelocatingProgress);
        cameraRelocatingProgress += cameraRelocateSpeed;
        if (camera.transform.position == nextCameraLocation.transform.position)
        {
            //cameraRelocating = false;
        }
    }
}
