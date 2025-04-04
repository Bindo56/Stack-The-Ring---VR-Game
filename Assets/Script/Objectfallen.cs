using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Objectfallen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMenu()
    {
        LoadSenceBYAddressable.Instance.ReloadAddressableSceneGame();
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.GetComponent<XRGrabInteractable>())
        {
            LoadSenceBYAddressable.Instance.ReloadAddressableSceneGame();
        }
    }
}
