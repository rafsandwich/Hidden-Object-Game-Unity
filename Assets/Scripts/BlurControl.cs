using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BlurControl : MonoBehaviour
{
    public PostProcessProfile blurProfile;

    private void Start()
    {
        PostProcessVolume postProcessVolume = Camera.main.GetComponent<PostProcessVolume>();

        postProcessVolume.profile = blurProfile;
        Debug.Log("post processing successful ");
    }
}
