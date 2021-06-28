using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private GameObject player=default;
    private Vector3 playerPos;


    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        var cameraTransform = transform;
        cameraTransform.position = new Vector3(cameraTransform.position.x,playerPos.y+5,playerPos.z-5);
    }
}
