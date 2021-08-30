using System.Collections;
using Ads;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

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
    private static readonly int IsClear = Animator.StringToHash("isClear");
    private CanvasGroup resultWindow;
    private GameObject inGameUIObj;
    private GameObject newRecordUIObj;
    private CanvasGroup gameOverText;
    private CanvasGroup gameClearText;
    private CanvasGroup missionFailedText;
    private GameObject nextButton;
    private GameObject retryButton;
    private string stageName;
    private AudioManager audioManager;
    
    private void Awake()
    {
        mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>(); 
        resultWindow = GameObject.Find("Result").GetComponent<CanvasGroup>();
        retryButton = GameObject.Find("RetryButton");
        nextButton = GameObject.Find("NextButton");
        gameOverText = GameObject.Find("GameOverText").GetComponent<CanvasGroup>();
        gameClearText = GameObject.Find("StageClearText").GetComponent<CanvasGroup>();
        missionFailedText = GameObject.Find("MissionFailedText").GetComponent<CanvasGroup>();

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        
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
        newRecordUIObj.SetActive(false);
        stageName = SceneManager.GetActiveScene().name;
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
       }

       if (other.gameObject.CompareTag("ClearLine"))
       {
           StartCoroutine(GameClear());
           isGameOver = true;
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

   private IEnumerator GameOver()
   {
      
       anim.SetBool(IsDown,true);
       inGameUIObj.SetActive(false);
       gameOverCamera.gameObject.SetActive(true);
       mainCamera.gameObject.SetActive(false);

       gameOverText.DOFade(1, 1f).OnComplete(()=> gameOverText.DOFade(0,1f));
       yield return new WaitForSeconds(2f);
       resultWindow.gameObject.SetActive(true);
       nextButton.gameObject.SetActive(false);
       retryButton.gameObject.SetActive(true);
       
       resultWindow.transform.DOLocalMoveX(0, 1);
      
      
       var stageName = SceneManager.GetActiveScene().name;
       if (PlayerPrefs.GetInt(stageName+"HighScore") < score)
       {
           PlayerPrefs.SetInt(stageName+"HighScore",score); 
           newRecordUIObj.SetActive(true);
           ScoreRanking.SendPlayScore(score);
           Debug.Log("ScoreSet");
       }
       
       ResultHighScore.UpdateScore(PlayerPrefs.GetInt(stageName+"HighScore"));

       yield return new WaitForSeconds(0.5f);
       
       //スコア反映
       ResultScore.UpdateScore(score);
       
       //広告を表示  
       audioManager.BGMStop();
       yield return StartCoroutine(InterstitialAds.ShowInterstitial());
       audioManager.BGMPlay(1);
       
   }

   private IEnumerator GameClear()
   {
      
       var isMissionClear = false;
       switch (stageName)
       {
           case "Stage1":
               if (score >= 2000)
               {
                   isMissionClear = true;
                   if (PlayerPrefs.GetInt("ClearStage") < 1) PlayerPrefs.SetInt("ClearStage", 2);
               }

               break;
           case "Stage2":
               if (score >= 4000) 
               {
                   isMissionClear = true;
                   if (PlayerPrefs.GetInt("ClearStage") < 2) PlayerPrefs.SetInt("ClearStage", 3);
               }
               break;
           case "Stage3":
               if (score >= 8000)
               {
                   isMissionClear = true;
                   if (PlayerPrefs.GetInt("ClearStage") < 3) PlayerPrefs.SetInt("ClearStage", 4);
               }
               break;
           case "Stage4":
               if (score >= 16000)
               {
                   isMissionClear = true;
                   if (PlayerPrefs.GetInt("ClearStage") < 4) PlayerPrefs.SetInt("ClearStage", 5);
               }
               break;
           case "Stage5":
               if (score >= 32000)
               {
                   isMissionClear = true;
                   if (PlayerPrefs.GetInt("ClearStage") < 5) PlayerPrefs.SetInt("ClearStage", 6);
               }
               break;
       }

       if (!isMissionClear)
       {
           StartCoroutine(MissionFailed());
           yield break;
       }
       
       anim.SetBool(IsClear, true);
       inGameUIObj.SetActive(false);
       gameOverCamera.transform.localPosition = new Vector3(0, 1.3f, 1.5f);
       gameOverCamera.transform.localRotation = Quaternion.Euler(20, 180, 0);
       gameOverCamera.gameObject.SetActive(true);
       mainCamera.gameObject.SetActive(false);

       gameClearText.DOFade(1, 1f).OnComplete(() => gameClearText.DOFade(0, 1f));
       yield return new WaitForSeconds(2f);
       resultWindow.gameObject.SetActive(true);
       nextButton.SetActive(true);
       retryButton.SetActive(false);
       

       resultWindow.transform.DOMove(new Vector3(622, 1344, 0), 1);

       if (PlayerPrefs.GetInt(stageName + "HighScore") < score)
       {
           PlayerPrefs.SetInt(stageName + "HighScore", score);
           newRecordUIObj.SetActive(true);
       }

       ResultHighScore.UpdateScore(PlayerPrefs.GetInt(stageName + "HighScore"));

       yield return new WaitForSeconds(0.5f);

       //スコア反映
       ResultScore.UpdateScore(score);
   
       //広告を表示  
       audioManager.BGMStop();
       yield return StartCoroutine(InterstitialAds.ShowInterstitial());
       audioManager.BGMPlay(1);
       
   }
   private IEnumerator MissionFailed()
   {
       
       anim.SetBool(IsDown,true);
       inGameUIObj.SetActive(false);
       gameOverCamera.gameObject.SetActive(true);
       mainCamera.gameObject.SetActive(false);

       missionFailedText.DOFade(1, 1f).OnComplete(()=> missionFailedText.DOFade(0,1f));
       yield return new WaitForSeconds(2f);
       resultWindow.gameObject.SetActive(true);
       nextButton.gameObject.SetActive(false);
       retryButton.gameObject.SetActive(true);
       
       
       resultWindow.transform.DOMove(new Vector3(622, 1344, 0), 1);
      
       if (PlayerPrefs.GetInt(stageName+"HighScore") < score)
       {
           PlayerPrefs.SetInt(stageName+"HighScore",score); 
           newRecordUIObj.SetActive(true);
       }
       
       ResultHighScore.UpdateScore(PlayerPrefs.GetInt(stageName+"HighScore"));

       yield return new WaitForSeconds(0.5f);
       
       //スコア反映
       ResultScore.UpdateScore(score);
       
       //広告を表示  
       audioManager.BGMStop();
       yield return StartCoroutine(InterstitialAds.ShowInterstitial());
       
       audioManager.BGMPlay(1);
       
   }
}
