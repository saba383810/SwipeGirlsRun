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
        SceneManager.LoadScene("MainGame");
    }
}
