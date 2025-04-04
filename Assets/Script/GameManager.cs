using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager : MonoBehaviour
{
    private bool isDragging = false;


    [SerializeField] Material[] normalMaterials;
    [SerializeField] Material[] outlineMaterials;
    [SerializeField] GameObject fadeInUI;
    [SerializeField] TextMeshProUGUI uiText;
   [SerializeField] Button restartBtn;
    [SerializeField] Transform setPoint1;
    [SerializeField] Transform setPoint2;
    [SerializeField] Transform setPoint3;
    [SerializeField] Transform setPoint4;
    public Transform poleTop; // Reference to the top of the pole
  //  private float ringSpacing = 0.1f; // Space between stacked rings
    private List<GameObject> stackedRings = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
      // restartBtn.gameObject.SetActive(false);
        uiText.text = "Stack the rings from largest to smallest!";
    }

    public void RestartBtn()
    {
        LoadSenceBYAddressable.Instance.ReloadAddressableSceneGame();

      //  StartCoroutine(LoadAsync("SampleScene"));
       // fadeInUI.SetActive(true);
    }
   /* IEnumerator LoadAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            yield return null;
        }
    }*/

    private void OnTriggerStay(Collider other)
    {
      //  if (other.CompareTag("Ring1") || other.CompareTag("Ring2") || other.CompareTag("Ring3") || other.CompareTag("Ring4"))
        {
            if (other.CompareTag("Ring1") || other.CompareTag("Ring2") || other.CompareTag("Ring3") || other.CompareTag("Ring4"))
            {
                Transform targetSetPoint = GetAvailableSetPoint(); // Find an empty set point

                if (targetSetPoint != null)
                {
                    other.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
                    other.transform.SetParent(targetSetPoint);
                    other.transform.position = targetSetPoint.position;
                    other.transform.rotation = targetSetPoint.rotation;
                    other.gameObject.GetComponentInChildren<Canvas>().enabled = false;

                    Debug.Log($"{other.name} placed at {targetSetPoint.name}");
                }
                else
                {
                    Debug.Log("No available set points!");
                }

                isDragging = false; // Stop dragging when placed
                CheckWinConditions();
            }
        }
    }

    Transform GetAvailableSetPoint()
    {
        if (setPoint1.childCount == 0) return setPoint1;
        if (setPoint2.childCount == 0) return setPoint2;
        if (setPoint3.childCount == 0) return setPoint3;
        if (setPoint4.childCount == 0) return setPoint4;

        return null; // No empty set points available
    }

    void CheckWinConditions()
    {
        bool set1 = setPoint1.childCount > 0 && setPoint1.GetChild(0).CompareTag("Ring1");
        bool set2 = setPoint2.childCount > 0 && setPoint2.GetChild(0).CompareTag("Ring2");
        bool set3 = setPoint3.childCount > 0 && setPoint3.GetChild(0).CompareTag("Ring3");
        bool set4 = setPoint4.childCount > 0 && setPoint4.GetChild(0).CompareTag("Ring4");


        if (set1 && set2 && set3 && set4)
        {
            Debug.Log("All rings are in the correct order!");
            uiText.text = " Game Won ";
            restartBtn.gameObject.SetActive(true);
          //  StartCoroutine(CheckPoints());
        }

        else if (setPoint1.childCount == 1 &&
                 setPoint2.childCount == 1 &&
                 setPoint3.childCount == 1 &&
                 setPoint4.childCount == 1)
        {
            Debug.Log("All rings are in the wrong order!");
            uiText.text = " Game Lost ";
            restartBtn.gameObject.SetActive(true);
           // StartCoroutine(CheckPoints());
        }
        else
        {
           // StartCoroutine(CheckPoints());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
