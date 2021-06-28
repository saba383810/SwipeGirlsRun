using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
       Rigidbody rb;
       private Camera mainCamera;
       private float playerSpeed = 0.1f;
       private int atkPoint =10;
       
       
       private void Start ()
       {
           mainCamera = Camera.main;
           rb = GetComponent<Rigidbody>();
           AttackPoint.UpdateAttackPoint(atkPoint);
           StartCoroutine(RunControl());
       }

       private void Update()
       {
          
       }


       private IEnumerator RunControl()
       {
           var playerPos = rb.position;

           while (true)
           {
               if (Input.GetMouseButtonDown(0))
               {
                   var prevPosX = mainCamera.ScreenToWorldPoint(Input.mousePosition + mainCamera.transform.forward * 10).x;
                   while (Input.GetMouseButton(0))
                   {
                       var forward = mainCamera.transform.forward;
                       var direction = mainCamera.ScreenToWorldPoint(Input.mousePosition + forward * 10).x - prevPosX;
                       prevPosX = mainCamera.ScreenToWorldPoint(Input.mousePosition + forward * 10).x;
                       playerPos.x += direction*0.5f;
                       if (playerPos.x >= 1.5f) playerPos.x = 1.5f;
                       if (playerPos.x <= -1.5f) playerPos.x = -1.5f;
                       
                       playerPos.z += playerSpeed;
                       rb.MovePosition(playerPos);
                       yield return null;
                   }
               }
               playerPos.z += playerSpeed;
               rb.MovePosition(playerPos);
               yield return null;
           }
       }

       private void OnCollisionEnter(Collision other)
       {
           if (other.gameObject.CompareTag("bread"))
           {
               Destroy(other.gameObject);
               atkPoint += 10;
               AttackPoint.UpdateAttackPoint(atkPoint);
               Debug.Log("scoreup");
           }
       }
}
