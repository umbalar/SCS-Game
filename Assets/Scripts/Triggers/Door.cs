using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Camera _camera;
    public GameObject _nextCameraLocation;
    private Vector3 _cameraStartPosition;
    public float _cameraRelocateSpeed;
    private float _cameraRelocatingProgress;
    public bool _cameraRelocating;
    void Start()
    {
        _camera = Camera.main;
        _cameraStartPosition = _camera.transform.position.normalized;
        _cameraRelocating = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_cameraRelocating)
        {
            RelocateCamera();
        }
        //Debug.Log(cameraRelocating);
    }

    void RelocateCamera()
    {
        _camera.transform.position = Vector3.Lerp(_cameraStartPosition, _nextCameraLocation.transform.position, _cameraRelocatingProgress);
        _cameraRelocatingProgress += _cameraRelocateSpeed;
        if (_camera.transform.position == _nextCameraLocation.transform.position)
        {
            //cameraRelocating = false;
        }
    }
}
