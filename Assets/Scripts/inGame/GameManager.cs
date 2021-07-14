using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Camera characterCamera =default;
    [SerializeField] private Camera mainCamera =default;
    [SerializeField] private CanvasGroup uiCanvasGroup =default;
    [SerializeField] private Player player =default;
    void Start()
    {
        StartCoroutine(GameStart());
        uiCanvasGroup.alpha = 0;
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(3f);
        mainCamera.gameObject.SetActive(true);
        characterCamera.gameObject.SetActive(false);
        uiCanvasGroup.alpha = 1;
        player.PlayerRun();
    }
    


}
