using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    new Camera camera;
    public Grisha grisha;
    public GameObject nextCameraLocation;
    Vector3 cameraStartPosition;
    public float cameraRelocateSpeed;
    float cameraRelocatingProgress;
    bool cameraRelocating;
    [SerializeField] float waitTime;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision = grisha.GetComponent<Collider2D>();
        Debug.Log("Door");
        //camera.transform.position = nextCameraLocation.transform.position;
        cameraRelocating = true;
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        Debug.Log("Wait");  
        grisha.isRuning = false;
        yield return new WaitForSeconds(waitTime);
        grisha.isRuning = true;
    }
}
