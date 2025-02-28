using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    //declares Vector3 variable to store distance we want b/w Main Camera and Player Capsule
    public Vector3 camOffset = new Vector3(0f, 1.2f, -2.6f);

    //creates variable to hold the Player Capsule's Transform info 
    private Transform target;

    private void Start()
    {
        //GameObject.Find locates the capsule by name and retrieves its Transform property
        target = GameObject.Find("Player").transform;
    }

    //Method that executes after Update
    void LateUpdate()
    {
        //Sets the camera's position to target.TransformPoint(camOffset) for each frame
        this.transform.position = target.TransformPoint(camOffset);

        //Updates the capsule's rotation in every frame
        this.transform.LookAt(target);
    }
}
//Think of it like this:
// 1) We created an offset position for the camera.
// 2) We found and stored the player capsule's position.
// 3) We manually updated its position and rotation every frame so that it's always following at a set distance and looking at the player 