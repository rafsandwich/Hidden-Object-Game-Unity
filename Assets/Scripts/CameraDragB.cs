using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDragB : MonoBehaviour
{
    private Vector3 origin;
    private Vector3 difference;
    private Vector3 resetCamera;

    private bool dragging = false;

    private void Start()
    {
        resetCamera = Camera.main.transform.position;
    }

    //called every frame, if the behaviour is enabled. Called after all Update funcs have been called
    private void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            //ScreenToWorldPoint transforms a point from screen space into world space, world space is defined as the coordinate system at top of game hierarchy
            difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            if (dragging == false)
            {
                dragging = true;
                origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else 
        {
            dragging = false;        
        }

        if (dragging) 
        {
            Camera.main.transform.position = origin - difference;
        }

        //if (Input.GetMouseButton(1))
        //    Camera.main.transform.position = resetCamera;
    }
}
