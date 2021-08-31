using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultButtonManager : MonoBehaviour
{
    [SerializeField] private LoadingManager loadingManager =default;
    public void OnHomeButtonClicked()
    {
        loadingManager.NextScene("Title");
    }

    public void OnRetryButtonClicked()
    {
        loadingManager.NextScene(SceneManager.GetActiveScene().name);
    }

    public void OnNextButtonClicked()
    {
        var stageName = SceneManager.GetActiveScene().name;
        switch (stageName)
        {
            case "Stage1":
                loadingManager.NextScene("Stage2");
                break;
            case "Stage2":
                loadingManager.NextScene("Stage3");
                break;
            case "Stage3":
                loadingManager.NextScene("Stage4");
                break;
            case "Stage4":
                loadingManager.NextScene("Stage5");
                break;
        }
    }
}
