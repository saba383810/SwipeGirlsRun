using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class TItleButtonManager : MonoBehaviour
{
   [SerializeField] private CanvasGroup startButton =default;
   [SerializeField] private GameObject whiteBack=default;
   [SerializeField] private GameObject selectHeader = default;
   public  void OnStartButtonClicked()
   {
      StartCoroutine(nameof(StartButtonClickedMove));
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
      selectHeader.transform.position =  new Vector3(2493, 2694, 0);
      selectHeader.transform.DOMove(new Vector3(447, 2333, 0),0.8f);
   }
}
