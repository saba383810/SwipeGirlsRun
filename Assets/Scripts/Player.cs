using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{ 
    Rigidbody rb;
    [SerializeField] private Camera mainCamera =default;
    private float playerSpeed = 0.1f;
    private int atkPoint =10;
    [SerializeField] private bool isAttack = false;
    private Enemy enemy;

    private void Start ()
    {
       rb = GetComponent<Rigidbody>();
       AttackPoint.UpdateAttackPoint(atkPoint);
    }

   public void PlayerRun()
   {
       StartCoroutine(RunControl());
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
                   
                   if(!isAttack)playerPos.z += playerSpeed;
                   
                   rb.MovePosition(new Vector3(playerPos.x,transform.position.y,playerPos.z));
                   yield return null;
               }
           }
           if(!isAttack) playerPos.z += playerSpeed;
           
           rb.MovePosition(new Vector3(playerPos.x,transform.position.y,playerPos.z));
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
   
   private void OnCollisionStay(Collision other)
   {
       if (other.gameObject.CompareTag("enemy"))
       {
           isAttack = true;
           enemy = other.gameObject.GetComponent<Enemy>();
           enemy.HpDamage(1);
           atkPoint -= 1;
           AttackPoint.UpdateAttackPoint(atkPoint);
           if (enemy.GetHp() > 0) return;
           Destroy(other.gameObject);
           isAttack = false;
       }
   }

   private void OnCollisionExit(Collision other)
   {
       if (other.gameObject.CompareTag("enemy"))
       {
           isAttack = false;
       }
   }
}
