using System.Collections.Generic;
using System.Text;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayFabLogin : MonoBehaviour
{
  //アカウントを作成するか
  private bool _shouldCreateAccount;
  
  //ログイン時に使うID
  private string _customID;
  
  //=================================================================================
  //ログイン処理
  //=================================================================================
  
  public void Start() {
    Login();
  }

  //ログイン実行
  private void Login() {
    _customID = LoadCustomID();
    var request = new LoginWithCustomIDRequest { CustomId = _customID,  CreateAccount = _shouldCreateAccount};
    PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
  }

  //ログイン成功
  private void OnLoginSuccess(LoginResult result){
    //アカウントを作成しようとしたのに、IDが既に使われていて、出来なかった場合
    if (_shouldCreateAccount && !result.NewlyCreated) {
      Debug.LogWarning($"CustomId : {_customID} は既に使われています。");
      Login();//ログインしなおし
      return;
    }
    
    //アカウント作成時にIDを保存
    if (result.NewlyCreated) {
      SaveCustomID();
      ScoreRanking.SendPlayScore(0);
    }
    Debug.Log($"PlayFabのログインに成功\nPlayFabId : {result.PlayFabId}, CustomId : {_customID}\nアカウントを作成したか : {result.NewlyCreated}");
  }

  //ログイン失敗
  private void OnLoginFailure(PlayFabError error){
    Debug.LogError($"PlayFabのログインに失敗\n{error.GenerateErrorReport()}");
  }
  
  //=================================================================================
  //カスタムIDの取得
  //=================================================================================

  //IDを保存する時のKEY
  private static readonly string CUSTOM_ID_SAVE_KEY = "CUSTOM_ID_SAVE_KEY";
  
  //IDを取得
  private string LoadCustomID() {
    //IDを取得
    string id = PlayerPrefs.GetString(CUSTOM_ID_SAVE_KEY);

    //保存されていなければ新規生成
    _shouldCreateAccount = string.IsNullOrEmpty(id);
    return _shouldCreateAccount ? GenerateCustomID() : id;
  }

  //IDの保存
  private void SaveCustomID() {
    PlayerPrefs.SetString(CUSTOM_ID_SAVE_KEY, _customID);
  }
  
  //=================================================================================
  //カスタムIDの生成
  //=================================================================================
 
  //IDに使用する文字
  private static readonly string ID_CHARACTERS = "0123456789abcdefghijklmnopqrstuvwxyz";

  //IDを生成する
  private string GenerateCustomID() {
    var idLength = 32;//IDの長さ
    var stringBuilder = new StringBuilder(idLength);
    var random = new System.Random();

    //ランダムにIDを生成
    for (var i = 0; i < idLength; i++){
      stringBuilder.Append(ID_CHARACTERS[random.Next(ID_CHARACTERS.Length)]);
    }

    return stringBuilder.ToString();
  }
}

