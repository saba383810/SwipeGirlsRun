using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private GameObject player;
    private Vector3 playerPos;


    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        var cameraTransform = transform;
        //cameraTransform.position = new Vector3(cameraTransform.position.x,playerPos.y+3,playerPos.z-3);
        cameraTransform.position = new Vector3(playerPos.x,playerPos.y+3,playerPos.z-3);
    }

    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }
}
