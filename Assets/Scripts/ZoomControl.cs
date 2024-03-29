using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomControl : MonoBehaviour
{
    public float ZoomChange;
    public float SmoothChange;
    public float MinSize, MaxSize;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if(Input.mouseScrollDelta.y > 0)
            cam.orthographicSize -= ZoomChange * Time.deltaTime * SmoothChange;
        if (Input.mouseScrollDelta.y < 0)
            cam.orthographicSize += ZoomChange * Time.deltaTime * SmoothChange;
        
        //clamps given value between min float and max float
        //returns given value if it is within the range
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize,MinSize, MaxSize);
    }

}
