using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Animate_HandsOnInput : MonoBehaviour
{

    public InputActionProperty pinchAnimateAction;
    public InputActionProperty gripAnimateAction;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      float triggerValue =  pinchAnimateAction.action.ReadValue<float>();
      float gripValue =  pinchAnimateAction.action.ReadValue<float>();
        anim.SetFloat("Trigger", triggerValue);
        anim.SetFloat("Grip", gripValue);
       // Debug.Log(triggerValue);
    }
}
