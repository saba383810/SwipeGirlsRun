using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using DG.Tweening;

public class Player : MonoBehaviour
{ 
    Rigidbody rb;
    private Camera mainCamera;
    [SerializeField] private Camera gameOverCamera =default;
    [SerializeField] private Camera startCamera =default;
    private GameManager gameManager;
    private float playerSpeed = 0.2f;
    private int atkPoint =10;
    private int score=0;
    private bool isGameOver = false;
    private bool isAttack = false;
    private Enemy enemy;
    private Animator anim;
    private static readonly int IsDown = Animator.StringToHash("isDown");
    private CanvasGroup resultWindow;
    private GameObject inGameUIObj;
    private GameObject newRecordUIObj;

    private void Awake()
    {
        mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        resultWindow = GameObject.Find("Result").GetComponent<CanvasGroup>();
        inGameUIObj = GameObject.Find("Battle");
        newRecordUIObj = GameObject.Find("NewRecord");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameObject.Find("GameManager").GetComponent<PrefabGenerationManager>().SetPlayer(gameObject.transform);
        
    }

    private void Start ()
    {
        gameManager.SetPlayer(gameObject.GetComponent<Player>());
        gameManager.SetCharacterCamera(startCamera);
        mainCamera.gameObject.SetActive(false);
        gameOverCamera.gameObject.SetActive(false);
       rb = GetComponent<Rigidbody>();
       AttackPoint.UpdateAttackPoint(atkPoint);
       ResultScore.UpdateScore(0);
       anim = GetComponent<Animator>();
       mainCamera.GetComponent<CameraMove>().SetPlayer(gameObject);
       
    }

   public void PlayerRun()
   {
       StartCoroutine(RunControl());
   }

   private IEnumerator RunControl()
   {
       var playerPos = rb.position;

       while (!isGameOver)
       {
           if (Input.GetMouseButtonDown(0))
           {
               var prevPosX = mainCamera.ScreenToWorldPoint(Input.mousePosition + mainCamera.transform.forward * 10).x;
               while (Input.GetMouseButton(0))
               {
                   var forward = mainCamera.transform.forward;
                   var direction = mainCamera.ScreenToWorldPoint(Input.mousePosition + forward * 10).x - prevPosX;
                   prevPosX = mainCamera.ScreenToWorldPoint(Input.mousePosition + forward * 10).x;
                   playerPos.x += direction*0.7f;
                   if (playerPos.x >= 2.5f) playerPos.x = 2.5f;
                   if (playerPos.x <= -2.5f) playerPos.x = -2.5f;
                   
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
       if (isGameOver) return;
       if (other.gameObject.CompareTag("bread"))
       {
           Destroy(other.gameObject);
           atkPoint += 10;
           AttackPoint.UpdateAttackPoint(atkPoint);
           Debug.Log("scoreUp");
       }
   }
   
   private void OnCollisionStay(Collision other)
   {
       if (isGameOver) return ;
       if (other.gameObject.CompareTag("enemy"))
       {
           isAttack = true;
           enemy = other.gameObject.GetComponent<Enemy>();
           enemy.HpDamage(1);
           atkPoint -= 1;
           score += 10;
           Score.UpdateScore(score);
           AttackPoint.UpdateAttackPoint(atkPoint);
           if (atkPoint <= 0)
           {
               StartCoroutine(GameOver());
               isGameOver = true;
           }
           if (enemy.GetHp() > 0) return;
           other.gameObject.GetComponent<Enemy>().EnemyDestroy();
           isAttack = false;
       }
       else
       {
           isAttack = false;
       }
   }

   private IEnumerator GameOver(){
   
       resultWindow.gameObject.SetActive(true);
       resultWindow.transform.DOMove(new Vector3(622, 1344, 0), 1);
       inGameUIObj.SetActive(false);
       
       gameOverCamera.gameObject.SetActive(true);
       mainCamera.gameObject.SetActive(false);
       anim.SetBool(IsDown,true);

       if (PlayerPrefs.GetInt("HighScore") < score)
       {
           PlayerPrefs.SetInt("HighScore",score); 
           newRecordUIObj.SetActive(true);
       }
       
       ResultHighScore.UpdateScore(PlayerPrefs.GetInt("HighScore"));

       yield return new WaitForSeconds(0.5f);
       
       //スコア反映
       ResultScore.UpdateScore(score);
       
   }
    
}
