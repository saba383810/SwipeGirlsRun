using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TItleButtonManager : MonoBehaviour
{
   [SerializeField] private CanvasGroup startButton =default;
   [SerializeField] private GameObject whiteBack=default;
   [SerializeField] private GameObject stageButtonGroup =default;
   [SerializeField] private CanvasGroup stageSelectView = default;
   [SerializeField] private GameObject modeSelectHeader = default;
   [SerializeField] private GameObject stageSelectHeader = default;
   [SerializeField] private CanvasGroup[] modeSelectButtons = new CanvasGroup[3];
   [SerializeField] private CanvasGroup[] stageSelectButtons = new CanvasGroup[5];
   [SerializeField] private Transform mainCamera =default;

   private void Start()
   {
      var playerNum = PlayerPrefs.GetInt("PlayerCharacterNum");
      mainCamera.position = playerNum switch
      {
         0 => new Vector3(6, 0.8f, 0),
         1 => new Vector3(8, 0.8f, 0),
         2 => new Vector3(10, 0.8f, 0),
         3 => new Vector3(12, 0.8f, 0),
         _ => throw new ArgumentOutOfRangeException()
      };
   }

   private void Update()
   {
      Debug.Log(PlayerPrefs.GetInt("PlayerCharacterNum"));
   }

   public  void OnStartButtonClicked()
   {
      StartCoroutine(nameof(StartButtonClickedMove));
   }
   public  void OnExitButtonClicked(int num)
   {
      StartCoroutine(ExitButtonClickedMove(num));
   }

   public void OnStageSelectButtonClicked()
   {
      StartCoroutine(nameof(StageSelectButtonClickedMove));
   }

   public void OnLeftArrowButtonClicked()
   {
      StartCoroutine(OnLeftArrowButtonClickedMove());
   }
   
   public void OnRightArrowButtonClicked()
   {
      StartCoroutine(OnRightArrowButtonClickedMove()); 
   }

   private IEnumerator StartButtonClickedMove()
   {
      startButton.DOFade(0, 0.2f);
      
      whiteBack.transform.position = new Vector3(645, -906, 0);
      whiteBack.transform.DOMove(new Vector3(645, -1006, 0),0.2f).OnComplete(() =>
      {
         whiteBack.transform.DOMove(new Vector3(645, 1344, 0),0.8f);
      });
      startButton.gameObject.SetActive(false);
     
      yield return new WaitForSeconds(1f);
      modeSelectHeader.SetActive(true);
      modeSelectHeader.transform.position =  new Vector3(2493, 2694, 0);
      modeSelectHeader.transform.DOMove(new Vector3(447, 2333, 0),0.8f);
      yield return new WaitForSeconds(0.3f);
      foreach (var button in modeSelectButtons)
      {
         button.gameObject.SetActive(true);
         button.DOFade(1.0f, 0.3f);
         yield return new WaitForSeconds(0.3f);
      }
   }

   private IEnumerator ExitButtonClickedMove(int num)
   {
      switch (num)
      {
         //ModeSelect to start
         case 0:
         {
            foreach (var button in modeSelectButtons)
            {
               button.DOFade(0f, 0.3f);
            }

            foreach (var button in modeSelectButtons)
            {
               button.gameObject.SetActive(false);
            }

            modeSelectHeader.transform.DOMove(new Vector3(2493, 2694, 0), 0.4f).OnComplete(() =>
            {
               modeSelectHeader.SetActive(false);
            });

            startButton.gameObject.SetActive(true);
            startButton.DOFade(1.0f, 0.4f);
            whiteBack.transform.DOMove(new Vector3(645, -906, 0), 0.8f);
            break;
         }
         // StageSelect to modeSelect
         case 1:
            stageSelectView.DOFade(0, 0.5f);
            stageSelectHeader.transform.DOMove(new Vector3(2493, 2694, 0),0.8f).OnComplete(() =>
            {
               stageSelectHeader.SetActive(false);
            });
            
            foreach (var button in stageSelectButtons)
            {
               button.gameObject.SetActive(false);
               button.DOFade(0f, 0.3f);
            }
            stageButtonGroup.SetActive(false);
            
            foreach (var button in modeSelectButtons) {button.gameObject.SetActive(true); }
            foreach (var button in modeSelectButtons)
            {
               button.DOFade(1f, 0.3f);
               yield return new WaitForSeconds(0.3f);
            }
            
            break;
      }

      yield return null;
   }

   private IEnumerator StageSelectButtonClickedMove()
   {
      foreach (var button in modeSelectButtons)
      {
         button.DOFade(0f, 0.3f);
      }

      
      foreach (var button in modeSelectButtons) {button.gameObject.SetActive(false); }
      
      stageSelectHeader.SetActive(true);
      stageSelectHeader.transform.position =  new Vector3(2493, 2694, 0);
      stageSelectHeader.transform.DOMove(new Vector3(447, 2333, 0),0.8f);
      
      stageButtonGroup.SetActive(true);
      stageSelectView.DOFade(1.0f, 0.5f);
      foreach (var button in stageSelectButtons)
      {
         button.gameObject.SetActive(true);
         button.DOFade(1.0f, 0.3f);
         yield return new WaitForSeconds(0.3f);
      }
      
      yield return null;
   }

   private IEnumerator OnLeftArrowButtonClickedMove()
   {
       var playerCharacterNum= PlayerPrefs.GetInt("PlayerCharacterNum");

       switch (playerCharacterNum)
       {
          case 0:
             mainCamera.transform.DOMoveX(12, 1);
             PlayerPrefs.SetInt("PlayerCharacterNum",3);
             break;
          case 1:
             mainCamera.transform.DOMoveX(6, 1);
             PlayerPrefs.SetInt("PlayerCharacterNum",0);
             break;
          case 2:
             mainCamera.transform.DOMoveX(8, 1);
             PlayerPrefs.SetInt("PlayerCharacterNum",1);
             break;
          case 3:
             mainCamera.transform.DOMoveX(10, 1);
             PlayerPrefs.SetInt("PlayerCharacterNum",2);
             break;
       }

       yield return null;
   }
   private IEnumerator OnRightArrowButtonClickedMove()
   {
      var playerCharacterNum= PlayerPrefs.GetInt("PlayerCharacterNum");
      switch (playerCharacterNum)
      {
         case 0:
            mainCamera.transform.DOMoveX(8,1);
            PlayerPrefs.SetInt("PlayerCharacterNum",1);
            break;
         case 1:
            mainCamera.transform.DOMoveX(10,1);
            PlayerPrefs.SetInt("PlayerCharacterNum",2);
            break;
         case 2:
            mainCamera.transform.DOMoveX(12,1);
            PlayerPrefs.SetInt("PlayerCharacterNum",3);
            break;
         case 3:
            mainCamera.transform.DOMoveX(6,1);
            PlayerPrefs.SetInt("PlayerCharacterNum",0);
            break;
      }

      yield return null;
   }
}
