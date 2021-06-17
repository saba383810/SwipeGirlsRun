using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
       Rigidbody rb;
       private Camera maniCamera;
       [SerializeField] private int score;
       private float playerSpeed = 0.1f;
       private void Start ()
       {
           maniCamera = Camera.main;
           rb = GetComponent<Rigidbody>();
       }

       private void Update()
       {
           var playerPos = rb.position;
           playerPos.x = maniCamera.ScreenToWorldPoint(Input.mousePosition + maniCamera.transform.forward * 3).x*1.5f;
           playerPos.z += playerSpeed;
           rb.MovePosition(playerPos);
           
           if (Input.GetKeyDown(KeyCode.Space))
           {
               score += 240;
               Score.UpdateCoin(score);
           }
       }
}
