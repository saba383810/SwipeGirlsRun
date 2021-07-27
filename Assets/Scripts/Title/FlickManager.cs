using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickManager : MonoBehaviour
{
       private Vector3 touchStartPos;                //タッチの始点取得変数
       private Vector3 touchEndPos;                  //タッチの終点取得変数
       private string Direction;                     //フリックの方向取得変数
       private bool isMove;
       private TItleButtonManager titleButtonManager;
   
   
       // Start is called before the first frame update
       private void Start()
       {
           titleButtonManager = gameObject.GetComponent<TItleButtonManager>();
       }
   
       // Update is called once per frame
       private void Update()
       {
           //タッチの始点を取得
           if (Input.GetKeyDown(KeyCode.Mouse0))
           {
               touchStartPos = new Vector3(Input.mousePosition.x,
                                           Input.mousePosition.y,
                                           Input.mousePosition.z);
           }
           //タッチの終点を取得
           if (Input.GetKeyUp(KeyCode.Mouse0))
           {
               touchEndPos = new Vector3(Input.mousePosition.x,
                                         Input.mousePosition.y,
                                         Input.mousePosition.z);
               GetDirection();
           }
       }
   
       //ピースの移動メソッド
       private void FixedUpdate()
       {
           if (!isMove||titleButtonManager.flickLock) return;
           switch (Direction)
           {
               case "right":
                   titleButtonManager.OnLeftArrowButtonClicked();
                   isMove = false;
                   break;
               case "left":
                   titleButtonManager.OnRightArrowButtonClicked();
                   isMove = false;
                   break;
               case "up":
                   break;
               case "down":
                   break;
   
           }
       }
   
       //フリックの方向を取得
       void GetDirection()
       {
           float directionX = touchEndPos.x - touchStartPos.x;
           float directionY = touchEndPos.y - touchStartPos.y;
   
           if (Mathf.Abs(directionY) < Mathf.Abs(directionX))
           {
               if (30 < directionX)
               {
                   //右向きにフリック
                   Direction = "right";
               }
               else if (-30 > directionX)
               {
                   //左向きにフリック
                   Direction = "left";
               }
   
           }
           else if (Mathf.Abs(directionX) < Mathf.Abs(directionY))
           {
               if (30 < directionY)
               {
                   //上向きにフリック
                   Direction = "up";
               }
               else if (-30 > directionY)
               {
                   //下向きのフリック
                   Direction = "down";
               }
           }
           else
           {
               //タッチを検出
               Direction = "touch";
           }
           isMove = true;
       }
}
