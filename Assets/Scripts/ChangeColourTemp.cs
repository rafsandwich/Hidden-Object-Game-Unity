using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColourTemp : MonoBehaviour
{
    public SpriteRenderer actor;

    void Start()
    {
        actor = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        actor.color = Random.ColorHSV();
        print("colour changed! ");
    }
}
