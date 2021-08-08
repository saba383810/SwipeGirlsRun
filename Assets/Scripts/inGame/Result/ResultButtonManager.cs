using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultButtonManager : MonoBehaviour
{
    public void OnHomeButtonClicked()
    {
        SceneManager.LoadScene("Title");
    }

    public void OnRetryButtonClicked()
    {
        var stageName = SceneManager.GetActiveScene().name;
        switch (stageName)
        {
            case "Stage1":
                SceneManager.LoadScene("Stage1");
                break;
            case "Stage2":
                SceneManager.LoadScene("Stage2");
                break;
            case "Stage3":
                SceneManager.LoadScene("Stage3");
                break;
            case "Stage4":
                SceneManager.LoadScene("Stage4");
                break;
            case "Stage5":
                SceneManager.LoadScene("Stage5");
                break;
            case "Endless":
                SceneManager.LoadScene("Endless");
                break;
        }
    }

    public void OnNextButtonClicked()
    {
        var stageName = SceneManager.GetActiveScene().name;
        switch (stageName)
        {
            case "Stage1":
                SceneManager.LoadScene("Stage2");
                break;
            case "Stage2":
                SceneManager.LoadScene("Stage3");
                break;
            case "Stage3":
                SceneManager.LoadScene("Stage3");
                break;
            case "Stage4":
                SceneManager.LoadScene("Stage3");
                break;
        }
    }
}
