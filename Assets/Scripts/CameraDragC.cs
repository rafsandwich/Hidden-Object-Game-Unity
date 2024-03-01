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

    [SerializeField]
    private SpriteRenderer mapRenderer;

    private float mapMinX, mapMaxX, mapMinY, mapMaxY;



    private Vector3 dragOrigin;

    private void Awake()
    {
        mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x / 2f;
        mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2f ;

        mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y / 2f;
        mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y / 2f;
    }

    private void Update()
    {
        PanCamera();
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            ZoomIn();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            ZoomOut();
        }
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

            //DEBUG -> print("origin" + dragOrigin + " newPosition " + cam.ScreenToWorldPoint(Input.mousePosition) + " =difference " + difference);

            //move camera by that distance

            cam.transform.position = ClampCamera(cam.transform.position + difference);
        }  
    }

    //public allows them to be called via a button click
    public void ZoomIn()
    {
        float newSize = cam.orthographicSize - zoomStep;

        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);

        cam.transform.position = ClampCamera(cam.transform.position);

    }

    public void ZoomOut()
    {
        float newSize = cam.orthographicSize + zoomStep;

        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);

        cam.transform.position = ClampCamera(cam.transform.position);
    }

    //size of camera changes every time zoom, calculate final positions where camera can move from scratch each time
    //should be called every time we want to move camera / zoom has occurred 
    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);
    }
}
