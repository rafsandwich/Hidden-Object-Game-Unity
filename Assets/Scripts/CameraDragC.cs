using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDragC : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    //could use camera.main but is bad practice as it searches for camera every time it's called

    [SerializeField]
    private float zoomStep, minCamSize, maxCamSize;

    private Vector3 dragOrigin;

    private void Update()
    {
        PanCamera();
    }

    private void PanCamera()
    {
        //save position of mouse in world space when first clicked

        if (Input.GetMouseButtonDown(1))

            //keep track of where drag started
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);

        //for every frame held calculate distance between drag origin and new position
        if (Input.GetMouseButton(1))
        {
            //check mouse position and compare to origin
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);

            print("origin" + dragOrigin + " newPosition " + cam.ScreenToWorldPoint(Input.mousePosition) + " =difference " + difference);

            //move camera by that distance
            cam.transform.position += difference;
        }  
    }

    //public allows them to be called via a button click
    public void ZoomIn()
    {
        float newSize = cam.orthographicSize - zoomStep;

        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);
    }

    public void ZoomOut()
    {
        float newSize = cam.orthographicSize + zoomStep;

        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);
    }


}
