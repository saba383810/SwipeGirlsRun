using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{ 
    private Camera characterCamera;
    [SerializeField] private Camera mainCamera =default;
    [SerializeField] private CanvasGroup resultCanvasGroup = default;
    [SerializeField] private CanvasGroup battleUICanvasGroup =default;
    [SerializeField] private GameObject missionUIObj = default;
    [SerializeField] private Text missionText =default;
    private Player player;
    [SerializeField] private GameObject[] characterPrefabs = new GameObject[4];
    [SerializeField] private AudioManager audioManager = default;
    void Start()
    {
        audioManager.BGMPlay(0);
        StartCoroutine(GameStart());
        StartCoroutine(MissionWindowIn());
        battleUICanvasGroup.alpha = 0;
        GenerateCharacter();
    }

    private IEnumerator GameStart()
    {
        yield return new WaitForSeconds(3f);
        mainCamera.gameObject.SetActive(true);
        characterCamera.gameObject.SetActive(false);
        battleUICanvasGroup.alpha = 1;
        player.PlayerRun();
    }

    private IEnumerator MissionWindowIn()
    {
        var stageName = SceneManager.GetActiveScene().name;
        var mission = stageName switch
        {
            "Stage1" => "Score 2000pt を達成せよ！",
            "Stage2" => "Score 4000pt を達成せよ！",
            "Stage3" => "Score 8000pt を達成せよ！",
            "Stage4" => "Score 16000pt を達成せよ！",
            "Stage5" => "Score 32000pt を達成せよ！",
            _ => "none"
        };

        if (mission != "none")
        {
            missionText.text = mission;
            missionUIObj.transform.DOLocalMove(new Vector3(0, 1110, 0), 1f);
        }

        yield return null;
    }

    private void GenerateCharacter()
    {
        var playerNum = PlayerPrefs.GetInt("PlayerCharacterNum");

        var playerCharacter = Instantiate(characterPrefabs[playerNum]);
        playerCharacter.transform.position = new Vector3(0, 0.5f, 22);
        playerCharacter.transform.rotation = Quaternion.Euler(0,0,0);
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public void SetCharacterCamera(Camera camera)
    {
        characterCamera = camera;
    }

}
