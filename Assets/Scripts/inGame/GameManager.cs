using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{ 
    private Camera characterCamera;
    [SerializeField] private Camera mainCamera =default;
    [SerializeField] private CanvasGroup uiCanvasGroup = default;
    private Player player;
    [SerializeField] private GameObject[] characterPrefabs = new GameObject[4];
    void Start()
    {
        StartCoroutine(GameStart());
        uiCanvasGroup.alpha = 0;
        GenerateCharacter();
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(3f);
        mainCamera.gameObject.SetActive(true);
        characterCamera.gameObject.SetActive(false);
        uiCanvasGroup.alpha = 1;
        player.PlayerRun();
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
