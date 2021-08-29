using System;
using System.Collections;
using System.Collections.Generic;
using Ads;
using UnityEngine;
using DG.Tweening;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TItleButtonManager : MonoBehaviour
{
   [SerializeField] private CanvasGroup startButton =default;
   [SerializeField] private CanvasGroup slideButton =default;
   [SerializeField] private GameObject whiteBack=default;
   [SerializeField] private GameObject stageButtonGroup =default;
   [SerializeField] private CanvasGroup stageSelectView = default;
   [SerializeField] private GameObject modeSelectHeader = default;
   [SerializeField] private GameObject stageSelectHeader = default;
   [SerializeField] private GameObject endlessModeHeader = default;
   [SerializeField] private GameObject yourWorldDataPanel = default;
   [SerializeField] private CanvasGroup[] endlessModeButtons = new CanvasGroup[2];
   [SerializeField] private CanvasGroup[] modeSelectButtons = new CanvasGroup[3];
   [SerializeField] private CanvasGroup[] stageSelectButtons = new CanvasGroup[5];
   [SerializeField] private Text[] playerDataText =default;
   [SerializeField] private Button[] stageButtons = new Button[5];
   [SerializeField] private Transform mainCamera =default;
   [SerializeField] private Transform text1 =default;
   [SerializeField] private CanvasGroup playerNameRegisterPanel = default;
   [SerializeField] private InputField playerNameInputField = default;
   [SerializeField] private InputField settingRenameInputField = default;
   [SerializeField] private CanvasGroup settingPanelCanvasGroup =default;
   [SerializeField] private Text errorText =default;
   [SerializeField] private Text settingErrorText = default;
   [SerializeField] private CanvasGroup backGroundImg =default;
   [SerializeField] private CanvasGroup splashImg = default;
   [SerializeField] private AudioManager audioManager =default;
   [SerializeField] private CanvasGroup rewardPanel =default;
   [SerializeField] private CanvasGroup rewardConfirmWindow =default;
   private int misakiGet = 0;
   private int tokoGet = 0;
   private int kohakuGet = 0;
   private int yukoGet = 0;
   
   private bool nameEnable = false;
   private static bool firstLoaded = false;
   private int playerCharacterNum;
   
   public bool flickLock =false;

   IEnumerator Start()
   {
      misakiGet = PlayerPrefs.GetInt("MisakiGet");
      tokoGet = PlayerPrefs.GetInt("TokoGet");
      kohakuGet = PlayerPrefs.GetInt("KohakuGet");
      yukoGet = PlayerPrefs.GetInt("YukoGet");
      
      playerCharacterNum= PlayerPrefs.GetInt("PlayerCharacterNum");
      
      CharaRewardCheck(playerCharacterNum);
      
      if (!firstLoaded)
      {
         flickLock = true;
         backGroundImg.gameObject.SetActive(true);
         backGroundImg.alpha = 1;
         if (PlayerPrefs.GetString("PlayerName") == "")
         {
            PlayerPrefs.SetInt("MisakiGet",1);
            yield return new WaitForSeconds(1f);
            playerNameRegisterPanel.gameObject.SetActive(true);
            playerNameRegisterPanel.DOFade(1, 1);
         }
         else
         {
            StartCoroutine(SplashStart());
         }
         firstLoaded = true;
      }

      playerCharacterNum = PlayerPrefs.GetInt("PlayerCharacterNum");
      switch (playerCharacterNum)
      {
         case 0:
            mainCamera.position = new Vector3(6, 0.75f, 0);
            text1.GetComponent<Text>().text = "Misaki";
            break;
         case 1:
            mainCamera.position = new Vector3(8, 0.75f, 0);
            text1.GetComponent<Text>().text = "Toko";
            break;
         case 2:
            mainCamera.position = new Vector3(10, 0.75f, 0);
            text1.GetComponent<Text>().text = "Kohaku";
            break;
         case 3:
            mainCamera.position = new Vector3(12, 0.75f, 0);
            text1.GetComponent<Text>().text = "Yuko";
            break;
         default:
            throw new ArgumentOutOfRangeException();
      }
      SetStageButtonInteractable();
      
      
   }

   public void OnRegisterButtonClicked()
   {
      audioManager.SePlay(0);
      StartCoroutine(OnRegisterButtonClickedMove());
   }
   

   public  void OnStartButtonClicked()
   {
      flickLock = true;
      audioManager.SePlay(0);
      StartCoroutine(nameof(StartButtonClickedMove));
   }
   public  void OnExitButtonClicked(int num)
   {
      audioManager.SePlay(0);
      StartCoroutine(ExitButtonClickedMove(num));
      flickLock = false;
   }

   public void OnStageSelectButtonClicked()
   {
      audioManager.SePlay(0);
      StartCoroutine(nameof(StageSelectButtonClickedMove));
   }

   public void OnLeftArrowButtonClicked()
   {
      audioManager.SePlay(0);
      StartCoroutine(OnLeftArrowButtonClickedMove());
   }
   
   public void OnRightArrowButtonClicked()
   {
      audioManager.SePlay(0);
      StartCoroutine(OnRightArrowButtonClickedMove()); 
   }

   public void OnEndlessButtonClicked()
   {
      audioManager.SePlay(0);
      StartCoroutine(OnEndlessButtonClickedMove());
   }
  

   public void OnStageButtonClicked(int stageNum)
   {
      audioManager.SePlay(0);
      StartCoroutine(OnStageButtonClickedMove(stageNum));
   }

   private IEnumerator StartButtonClickedMove()
   {
      startButton.DOFade(0, 0.2f).OnComplete(()=>
      {
         startButton.gameObject.SetActive(false);
         slideButton.gameObject.SetActive(false);
      });
      
      whiteBack.transform.localPosition = new Vector3(0, -2100, 0);
      whiteBack.transform.DOLocalMove(new Vector3(0, -2200, 0),0.2f).OnComplete(() =>
      {
         whiteBack.transform.DOLocalMove(new Vector3(0, 0, 0),0.8f);
      });
     
      
     
      yield return new WaitForSeconds(1f);
      modeSelectHeader.SetActive(true);
      modeSelectHeader.transform.localPosition =  new Vector3(2000, 1370, 0);
      modeSelectHeader.transform.DOLocalMove(new Vector3(-200, 1000, 0),0.8f);
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

            modeSelectHeader.transform.DOLocalMove(new Vector3(2000, 1370, 0), 0.4f).OnComplete(() =>
            {
               modeSelectHeader.SetActive(false);
            });
            startButton.alpha = 0;
            startButton.gameObject.SetActive(true);
            startButton.DOFade(1.0f, 0.4f);
            slideButton.alpha = 0;
            slideButton.gameObject.SetActive(true);
            slideButton.DOFade(1.0f, 0.4f);
            
            whiteBack.transform.DOLocalMove(new Vector3(0, -2100, 0), 0.8f);
            break;
         }
         // StageSelect to modeSelect
         case 1:
            stageSelectView.DOFade(0, 0.5f);
            stageSelectHeader.transform.DOLocalMove(new Vector3(2000, 1370, 0),0.8f).OnComplete(() =>
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
         case 2:
            foreach (var button in endlessModeButtons)
            {
               button.DOFade(0f, 0.3f).OnComplete(()=>button.gameObject.SetActive(false));
               yield return new WaitForSeconds(0.15f);
            }
            endlessModeHeader.transform.DOLocalMove(new Vector3(2000, 1370, 0),0.8f).OnComplete(()=>endlessModeHeader.SetActive(false));
            yourWorldDataPanel.transform.DOLocalMove(new Vector3(2500,-125,0), 0.8f).OnComplete(()=>yourWorldDataPanel.SetActive(false));
            
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
      stageSelectHeader.transform.localPosition =  new Vector3(2000, 1370, 0);
      stageSelectHeader.transform.DOLocalMove(new Vector3(-200, 1000, 0),0.8f);
      
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

   // ReSharper disable Unity.PerformanceAnalysis
   private IEnumerator OnLeftArrowButtonClickedMove()
   {
       var playerCharacterNum= PlayerPrefs.GetInt("PlayerCharacterNum");

       switch (playerCharacterNum)
       {
          case 0:
             mainCamera.transform.DOMoveX(12, 0.5f);
             PlayerPrefs.SetInt("PlayerCharacterNum",3);
             text1.GetComponent<Text>().text = "Yuko";
             
             if (yukoGet == 0)
             {
                startButton.interactable = false;
                rewardPanel.alpha = 0;
                rewardPanel.gameObject.SetActive(true);
                rewardPanel.DOFade(1, 0.4f);
             }
             else
             {
                startButton.interactable = true;
                rewardPanel.gameObject.SetActive(false);
             }
             break;
          case 1:
             mainCamera.transform.DOMoveX(6, 0.5f);
             PlayerPrefs.SetInt("PlayerCharacterNum",0);
             text1.GetComponent<Text>().text = "Misaki";
             if (misakiGet == 0)
             {
                startButton.interactable = false;
                rewardPanel.alpha = 0;
                rewardPanel.gameObject.SetActive(true);
                rewardPanel.DOFade(1, 0.4f);
             }
             else
             {
                startButton.interactable = true;
                rewardPanel.gameObject.SetActive(false);
             }
             break;
          case 2:
             mainCamera.transform.DOMoveX(8, 0.5f);
             PlayerPrefs.SetInt("PlayerCharacterNum",1);
             text1.GetComponent<Text>().text = "Toko";
             if (tokoGet == 0)
             {
                startButton.interactable = false;
                rewardPanel.alpha = 0;
                rewardPanel.gameObject.SetActive(true);
                rewardPanel.DOFade(1, 0.4f);
             }
             else
             {
                startButton.interactable = true;
                rewardPanel.gameObject.SetActive(false);
             }
             break;
          case 3:
             mainCamera.transform.DOMoveX(10, 0.5f);
             PlayerPrefs.SetInt("PlayerCharacterNum",2);
             text1.GetComponent<Text>().text = "Kohaku";
             if (kohakuGet == 0)
             {
                startButton.interactable = false;
                rewardPanel.alpha = 0;
                rewardPanel.gameObject.SetActive(true);
                rewardPanel.DOFade(1, 0.4f);
             }
             else
             {
                startButton.interactable = true;
                rewardPanel.gameObject.SetActive(false);
             }
             break;
       }

       yield return null;
   }
   // ReSharper disable Unity.PerformanceAnalysis
   private IEnumerator OnRightArrowButtonClickedMove()
   {
      playerCharacterNum= PlayerPrefs.GetInt("PlayerCharacterNum");
      switch (playerCharacterNum)
      {
         case 0:
            mainCamera.transform.DOMoveX(8,0.5f);
            PlayerPrefs.SetInt("PlayerCharacterNum",1);
            text1.GetComponent<Text>().text = "Toko";
            if (tokoGet == 0)
            {
               startButton.interactable = false;
               rewardPanel.alpha = 0;
               rewardPanel.gameObject.SetActive(true);
               rewardPanel.DOFade(1, 0.4f);
            }
            else
            {
               startButton.interactable = true;
               rewardPanel.gameObject.SetActive(false);
            }
            break;
         case 1:
            mainCamera.transform.DOMoveX(10,0.5f);
            PlayerPrefs.SetInt("PlayerCharacterNum",2);
            text1.GetComponent<Text>().text = "Kohaku";
            if (kohakuGet == 0)
            {
               startButton.interactable = false;
               rewardPanel.alpha = 0;
               rewardPanel.gameObject.SetActive(true);
               rewardPanel.DOFade(1, 0.4f);
            }
            else
            {
               startButton.interactable = true;
               rewardPanel.gameObject.SetActive(false);
            }
            break;
         case 2:
            mainCamera.transform.DOMoveX(12,0.5f);
            PlayerPrefs.SetInt("PlayerCharacterNum",3);
            text1.GetComponent<Text>().text = "Yuko";
            if (yukoGet == 0)
            {
               startButton.interactable = false;
               rewardPanel.alpha = 0;
               rewardPanel.gameObject.SetActive(true);
               rewardPanel.DOFade(1, 0.4f);
            }
            else
            {
               startButton.interactable = true;
               rewardPanel.gameObject.SetActive(false);
            }
            break;
         case 3:
            mainCamera.transform.DOMoveX(6,0.5f);
            PlayerPrefs.SetInt("PlayerCharacterNum",0);
            text1.GetComponent<Text>().text = "Misaki";
            if (misakiGet == 0)
            {
               startButton.interactable = false;
               rewardPanel.alpha = 0;
               rewardPanel.gameObject.SetActive(true);
               rewardPanel.DOFade(1, 0.4f);
            }
            else
            {
               startButton.interactable = true;
               rewardPanel.gameObject.SetActive(false);
            }
            break;
      }
      yield return null;
   }

   private IEnumerator OnEndlessButtonClickedMove()
   {
      foreach (var button in modeSelectButtons)
      {
         button.DOFade(0f, 0.3f);
      }
      foreach (var button in modeSelectButtons) {button.gameObject.SetActive(false); }
      
      
      endlessModeHeader.SetActive(true);
      endlessModeHeader.transform.localPosition =  new Vector3(2000, 1370, 0);
      endlessModeHeader.transform.DOLocalMove(new Vector3(-200, 1000, 0),0.8f);
      yield return new WaitForSeconds(0.5f);
      
      yourWorldDataPanel.transform.localPosition = new Vector3(2000,0,0);
      yourWorldDataPanel.SetActive(true);
      yourWorldDataPanel.transform.DOLocalMove(new Vector3(0,0,0), 0.8f);
      
      foreach (var button in endlessModeButtons)
      {
         button.gameObject.SetActive(true);
         button.DOFade(1.0f, 0.3f);
         yield return new WaitForSeconds(0.3f);
      }
      //ランキング反映
      
      var playerData = ScoreRanking.GetPlayerData();
      yield return new WaitForSeconds(0.5f);
      if (playerData[0] == 0)
      {
         playerDataText[0].text = playerData[0].ToString();
         yield return new WaitForSeconds(0.4f);
         playerDataText[1].text = "未参加";
      }
      else
      {
         playerDataText[0].text = playerData[0].ToString();
         yield return new WaitForSeconds(0.4f);
         playerDataText[1].text = $"{playerData[1]}位";
      }
   }
   
   private IEnumerator OnStageButtonClickedMove(int stageNum)
   {
      
      //TODO ローディングアニメション入れ

      switch (stageNum)
      {
         case 1:
            SceneManager.LoadScene("Stage1");
            break;
         case 2:
            SceneManager.LoadScene("Stage2");
            break;
         case 3:
            SceneManager.LoadScene("Stage3");
            break;
         case 4:
            SceneManager.LoadScene("Stage4");
            break;
         case 5:
            SceneManager.LoadScene("Stage5");
            break;
         case 6:
            SceneManager.LoadScene("Endless");
            break;
      }

      yield return null;
   }
   
   private IEnumerator OnRegisterButtonClickedMove()
   {
      if (playerNameInputField.text.Length < 3||playerNameInputField.text.Length > 10)
      {
         errorText.text = "3~10文字でで入力してください。";
         yield break;
      }
      
      UpdateUserName(playerNameInputField.text);
   }


   private void SetStageButtonInteractable()
   {
      var clearStage = PlayerPrefs.GetInt("ClearStage");
      if (clearStage == 0) clearStage = 1;
      for (var i = 0; i<stageButtons.Length; i++)
      {
         stageButtons[i].interactable = clearStage > i;
      }
   }

   public void UpdateUserName(string userName)
   {
      //ユーザ名を指定して、UpdateUserTitleDisplayNameRequestのインスタンスを生成
      var request = new UpdateUserTitleDisplayNameRequest
      {
         DisplayName = userName
      };

      //ユーザ名の更新
      Debug.Log($"ユーザ名の更新開始");
      PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnUpdateUserNameSuccess, OnUpdateUserNameFailure);
      PlayerPrefs.SetString("PlayerName",userName);
   }

   //ユーザ名の更新成功
   private void OnUpdateUserNameSuccess(UpdateUserTitleDisplayNameResult result)
   {
      //result.DisplayNameに更新した後のユーザ名が入ってる
      Debug.Log($"ユーザ名の更新が成功しました : {result.DisplayName}");
      playerNameRegisterPanel.DOFade(0, 1).OnComplete(()=>
         playerNameRegisterPanel.gameObject.SetActive(false)
      );
      CharaRewardCheck(playerCharacterNum);
      StartCoroutine(SplashStart());
   }

   //ユーザ名の更新失敗
   private void OnUpdateUserNameFailure(PlayFabError error)
   {
      Debug.LogError($"ユーザ名の更新に失敗しました\n{error.GenerateErrorReport()}");
      errorText.text = "すでにその名前は使用されています。";
      settingErrorText.text = "すでにその名前は使用されています。";
   }

   private IEnumerator SplashStart()
   {
      splashImg.gameObject.SetActive(true);
      yield return new WaitForSeconds(1f);
      splashImg.DOFade(1, 1);
      yield return new WaitForSeconds(2f);
      splashImg.DOFade(0, 1);
      yield return new WaitForSeconds(1.5f);
      splashImg.gameObject.SetActive(false);
      backGroundImg.DOFade(0, 1);
      yield return new WaitForSeconds(1f);
      backGroundImg.gameObject.SetActive(false);
      flickLock = false;
   }

   public void OnSettingButtonClicked()
   {
      flickLock = true;
      settingErrorText.text = "";
      audioManager.SePlay(0);
      settingPanelCanvasGroup.gameObject.SetActive(true);
      settingPanelCanvasGroup.DOFade(1, 0.5f);
   }

   public void OnExitSettingClicked()
   {
      flickLock = false;
      audioManager.SePlay(0);
      settingPanelCanvasGroup.DOFade(0, 0.5f).OnComplete(() => settingPanelCanvasGroup.gameObject.SetActive(false));
   }

   public void RenameButtonClicked()
   {
      if (settingRenameInputField.text.Length < 3||settingRenameInputField.text.Length > 10)
      {
         settingErrorText.text = "3~10文字で入力してください。";
      }
      
      UpdateSettingUserName(settingRenameInputField.text);
   }
   
   public void UpdateSettingUserName(string userName)
   {
      //ユーザ名を指定して、UpdateUserTitleDisplayNameRequestのインスタンスを生成
      var request = new UpdateUserTitleDisplayNameRequest
      {
         DisplayName = userName
      };

      //ユーザ名の更新
      Debug.Log($"ユーザ名の更新開始");
      PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnSettingUpdateUserNameSuccess, OnUpdateUserNameFailure);
      PlayerPrefs.SetString("PlayerName",userName);
   }
   
   private void OnSettingUpdateUserNameSuccess(UpdateUserTitleDisplayNameResult result)
   {
      //result.DisplayNameに更新した後のユーザ名が入ってる
      Debug.Log($"ユーザ名の更新が成功しました : {result.DisplayName}");
      settingErrorText.text = "ユーザ名を変更しました。";
   }

   public void RewardButtonClicked()
   {
      rewardConfirmWindow.alpha = 0;
      rewardConfirmWindow.gameObject.SetActive(true);
      rewardConfirmWindow.DOFade(1, 0.5f);
   }

   public void RewardWindowExit()
   {
      rewardConfirmWindow.DOFade(0, 0.5f).OnComplete(() => rewardConfirmWindow.gameObject.SetActive(false));
      CharaRewardCheck(playerCharacterNum);
   }

   public void CharaRewardCheck(int charNum)
   {
      misakiGet = PlayerPrefs.GetInt("MisakiGet");
      tokoGet = PlayerPrefs.GetInt("TokoGet");
      kohakuGet = PlayerPrefs.GetInt("KohakuGet");
      yukoGet = PlayerPrefs.GetInt("YukoGet");
      switch (charNum)
      {
         case 0:
            if (misakiGet == 0)
            {
               startButton.interactable = false;
               rewardPanel.alpha = 0;
               rewardPanel.gameObject.SetActive(true);
               rewardPanel.DOFade(1, 0.4f);
            }
            else
            {
               startButton.interactable = true;
               rewardPanel.gameObject.SetActive(false);
            }
            break;
         case 1:
            if (tokoGet == 0)
            {
               startButton.interactable = false;
               rewardPanel.alpha = 0;
               rewardPanel.gameObject.SetActive(true);
               rewardPanel.DOFade(1, 0.4f);
            }
            else
            {
               startButton.interactable = true;
               rewardPanel.gameObject.SetActive(false);
            }
            break;
         case 2:
            if (kohakuGet == 0)
            {
               startButton.interactable = false;
               rewardPanel.alpha = 0;
               rewardPanel.gameObject.SetActive(true);
               rewardPanel.DOFade(1, 0.4f);
            }
            else
            {
               startButton.interactable = true;
               rewardPanel.gameObject.SetActive(false);
            }
            break;
         case 3:
            if (yukoGet == 0)
            {
               startButton.interactable = false;
               rewardPanel.alpha = 0;
               rewardPanel.gameObject.SetActive(true);
               rewardPanel.DOFade(1, 0.4f);
            }
            else
            {
               startButton.interactable = true;
               rewardPanel.gameObject.SetActive(false);
            }
            break;
         
      }
   }
}
